using System.Globalization;
using System.Text.Json;
using System.Xml.Linq;
using Bootcamp.Lecons;
using static Bootcamp.Tests.Verifier;

namespace Bootcamp.Tests;

public static class TestsLecon13
{
    private sealed class PolitiqueObservee : IPolitiqueRemise
    {
        public int Appels { get; private set; }
        public decimal Appliquer(decimal brut) { Appels++; return brut / 2; }
    }

    public static IEnumerable<Test> Tests() =>
    [
        new(13, "Strategy interchangeable sans modifier le service", () =>
        {
            Egal(100m, new ServiceTarification(new SansRemise()).Total([40m, 60m]));
            Egal(90m, new ServiceTarification(new RemiseFidelite(.10m)).Total([40m, 60m]));
            Egal(0m, new ServiceTarification(new SansRemise()).Total([]));
        }),
        new(13, "Validation et arrondi monetaires explicites", () =>
        {
            Egal(1.01m, new RemiseFidelite(.5m).Appliquer(2.01m));
            Egal(0m, new RemiseFidelite(1m).Appliquer(100m));
            Leve<ArgumentOutOfRangeException>(() => new RemiseFidelite(-.1m).Appliquer(100m));
            Leve<ArgumentOutOfRangeException>(() => new RemiseFidelite(1.1m).Appliquer(100m));
            Leve<ArgumentOutOfRangeException>(() => new SansRemise().Appliquer(-1m));
            Leve<ArgumentOutOfRangeException>(() => new RemiseFidelite(.1m).Appliquer(-1m));
            Leve<ArgumentOutOfRangeException>(() => new ServiceTarification(new SansRemise()).Total([20m, -1m]));
        }),
        new(13, "Decorator : delegation unique, journal invariant", () =>
        {
            var politique = new PolitiqueObservee();
            var messages = new List<string>();
            var culture = CultureInfo.CurrentCulture;
            try
            {
                CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("fr-FR");
                var service = new ServiceTarification(new RemiseJournalisee(politique, messages.Add));
                Egal(50m, service.Total([40m, 60m]));
                Egal(1, politique.Appels);
                Sequence(["total=50.00"], messages);
            }
            finally { CultureInfo.CurrentCulture = culture; }
        }),
        new(13, "Decorator : aucun faux succes sur erreur", () =>
        {
            var messages = new List<string>();
            var politique = new RemiseJournalisee(new SansRemise(), messages.Add);
            Leve<ArgumentOutOfRangeException>(() => politique.Appliquer(-1m));
            Egal(0, messages.Count);
            var journalEnPanne = new RemiseJournalisee(new SansRemise(),
                message => throw new InvalidOperationException("journal en panne"));
            Leve<InvalidOperationException>(() => journalEnPanne.Appliquer(10m));
        }),
    ];
}

public static class TestsLecon14
{
    private sealed class Fournisseur(long? valeur) : IFournisseurCentimes
    {
        public long? LireCentimes(string reference) => valeur;
    }
    private sealed class FournisseurEnPanne : IFournisseurCentimes
    {
        public long? LireCentimes(string reference) => throw new IOException("reseau indisponible");
    }

    public static IEnumerable<Test> Tests() =>
    [
        new(14, "Adapter : centimes, zero et absence", () =>
        {
            Egal<decimal?>(12.99m, new AdaptateurPrix(new Fournisseur(1299)).PrixUnitaire("A"));
            Egal<decimal?>(0m, new AdaptateurPrix(new Fournisseur(0)).PrixUnitaire("A"));
            Egal<decimal?>(null, new AdaptateurPrix(new Fournisseur(null)).PrixUnitaire("A"));
            Egal<decimal?>(long.MaxValue / 100m, new AdaptateurPrix(new Fournisseur(long.MaxValue)).PrixUnitaire("A"));
        }),
        new(14, "Adapter : ne pas masquer les erreurs techniques", () =>
        {
            Leve<InvalidOperationException>(() => new AdaptateurPrix(new Fournisseur(-1)).PrixUnitaire("A"));
            Leve<IOException>(() => new AdaptateurPrix(new FournisseurEnPanne()).PrixUnitaire("A"));
        }),
        new(14, "Factory et JSON avec echappement", () =>
        {
            LigneExport[] lignes = [new("A\"<&", 12.99m)];
            IExporteur exporteur = FabriqueExport.Creer(" JSON ");
            Sequence(lignes, JsonSerializer.Deserialize<LigneExport[]>(exporteur.Exporter(lignes)));
            Sequence<LigneExport>([], JsonSerializer.Deserialize<LigneExport[]>(exporteur.Exporter([])));
        }),
        new(14, "XML : structure et caracteres speciaux", () =>
        {
            var document = XDocument.Parse(FabriqueExport.Creer("Xml").Exporter([new("A<&", 12.99m)]));
            Egal("lignes", document.Root!.Name.LocalName);
            var ligne = document.Root.Element("ligne")!;
            Egal("A<&", (string?)ligne.Attribute("reference"));
            Egal(12.99m, (decimal)ligne.Element("montant")!);
            Egal(0, XDocument.Parse(FabriqueExport.Creer("xml").Exporter([])).Root!.Elements().Count());
        }),
        new(14, "Format non supporte explicite", () => Leve<ArgumentException>(() => FabriqueExport.Creer("yaml"))),
    ];
}

public static class TestsLecon15
{
    public static IEnumerable<Test> Tests() =>
    [
        new(15, "Agregat : total et lignes separees", () =>
        {
            var commande = new CommandeAchat();
            Egal(EtatCommande.Brouillon, commande.Etat); Egal(0m, commande.Total);
            commande.Ajouter("A", 2, 12.50m); commande.Ajouter("A", 1, 0m);
            Egal(25m, commande.Total); Egal(2, commande.Lignes.Count);
        }),
        new(15, "Invariants proteges sans mutation partielle", () =>
        {
            var commande = new CommandeAchat();
            Leve<ArgumentException>(() => commande.Ajouter(" ", 1, 1m));
            Leve<ArgumentOutOfRangeException>(() => commande.Ajouter("A", 0, 1m));
            Leve<ArgumentOutOfRangeException>(() => commande.Ajouter("A", -1, 1m));
            Leve<ArgumentOutOfRangeException>(() => commande.Ajouter("A", 1, -1m));
            Egal(0, commande.Lignes.Count);
            Leve<InvalidOperationException>(() => commande.Valider());
            Egal(EtatCommande.Brouillon, commande.Etat);
        }),
        new(15, "Pas d'alias mutable vers les lignes du domaine", () =>
        {
            var commande = new CommandeAchat();
            commande.Ajouter("A", 1, 10m);
            var vue = commande.Lignes;
            commande.Ajouter("B", 2, 20m);
            Egal(1, vue.Count, "un instantane, pas une vue vivante");
            if (vue is LigneCommande[] tableau) tableau[0] = new("FAUX", 99, 99m);
            Egal("A", commande.Lignes[0].Reference); Egal(50m, commande.Total);
        }),
        new(15, "Transitions et gel apres validation", () =>
        {
            var commande = new CommandeAchat();
            commande.Ajouter("A", 1, 10m); commande.Valider();
            Egal(EtatCommande.Validee, commande.Etat);
            Leve<InvalidOperationException>(() => commande.Ajouter("B", 1, 1m));
            Leve<InvalidOperationException>(() => commande.Valider());
            commande.Annuler(); Egal(EtatCommande.Annulee, commande.Etat);
            Leve<InvalidOperationException>(() => commande.Annuler());
            Leve<InvalidOperationException>(() => commande.Valider());
            Leve<InvalidOperationException>(() => commande.Ajouter("B", 1, 1m));
            Egal(10m, commande.Total);
        }),
        new(15, "Annulation du brouillon", () =>
        {
            var commande = new CommandeAchat();
            commande.Annuler(); Egal(EtatCommande.Annulee, commande.Etat);
            Leve<InvalidOperationException>(() => commande.Ajouter("A", 1, 1m));
        }),
    ];
}

public static class TestsLecon16
{
    private sealed class CatalogueObserve(decimal? montant) : IPrixArticle
    {
        public int Appels { get; private set; }
        public decimal? PrixUnitaire(string reference) { Appels++; return montant; }
    }
    private sealed class CatalogueEnPanne : IPrixArticle
    {
        public decimal? PrixUnitaire(string reference) => throw new IOException("stockage indisponible");
    }
    private sealed class SourceCentimes : IFournisseurCentimes
    {
        public long? LireCentimes(string reference) => reference == "A" ? 1250 : null;
    }

    public static IEnumerable<Test> Tests() =>
    [
        new(16, "Composition root et scenario nominal", () =>
        {
            Egal<ResultatDevis>(new ResultatDevis.Accepte(120m, 108m),
                CompositionDevis.Creer().Calculer([new("CLAVIER", 1), new("SOURIS", 2)]));
        }),
        new(16, "Adaptateurs substituables pour le meme cas d'usage", () =>
        {
            IPrixArticle[] catalogues =
            [
                new CatalogueMemoire(new Dictionary<string, decimal> { ["A"] = 12.50m }),
                new AdaptateurPrix(new SourceCentimes()),
            ];
            foreach (var catalogue in catalogues)
            {
                var service = new CalculateurDevis(catalogue, new SansRemise());
                Egal<ResultatDevis>(new ResultatDevis.Accepte(25m, 25m), service.Calculer([new("A", 2)]));
                Egal<ResultatDevis>(new ResultatDevis.Refuse("article-inconnu:X"), service.Calculer([new("X", 1)]));
            }
        }),
        new(16, "Refus avant tout appel externe", () =>
        {
            var catalogue = new CatalogueObserve(10m);
            var service = new CalculateurDevis(catalogue, new SansRemise());
            Egal<ResultatDevis>(new ResultatDevis.Refuse("commande-vide"), service.Calculer([]));
            Egal<ResultatDevis>(new ResultatDevis.Refuse("ligne-invalide"), service.Calculer([new("A", 1), new("B", 0)]));
            Egal<ResultatDevis>(new ResultatDevis.Refuse("ligne-invalide"), service.Calculer([new(" ", 1)]));
            Egal<ResultatDevis>(new ResultatDevis.Refuse("ligne-invalide"), service.Calculer([new("A", -1)]));
            Egal(0, catalogue.Appels);
        }),
        new(16, "Prix zero valide, absence distincte", () =>
        {
            Egal<ResultatDevis>(new ResultatDevis.Accepte(0m, 0m),
                new CalculateurDevis(new CatalogueObserve(0m), new SansRemise()).Calculer([new("A", 1)]));
            Egal<ResultatDevis>(new ResultatDevis.Refuse("article-inconnu:A"),
                new CalculateurDevis(new CatalogueObserve(null), new SansRemise()).Calculer([new("A", 1)]));
        }),
        new(16, "Pannes techniques non deguisees en refus metier", () =>
        {
            Leve<IOException>(() => new CalculateurDevis(new CatalogueEnPanne(), new SansRemise()).Calculer([new("A", 1)]));
            Leve<InvalidOperationException>(() => new CalculateurDevis(new CatalogueObserve(-1m), new SansRemise()).Calculer([new("A", 1)]));
        }),
    ];
}