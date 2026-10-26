namespace Bootcamp.Lecons;

public sealed record DemandeDevis(string Reference, int Quantite);
public abstract record ResultatDevis
{
    public sealed record Accepte(decimal Brut, decimal Net) : ResultatDevis;
    public sealed record Refuse(string Motif) : ResultatDevis;
}

public sealed class CalculateurDevis(IPrixArticle catalogue, IPolitiqueRemise remise)
{
    public ResultatDevis Calculer(IReadOnlyList<DemandeDevis> lignes)
    {
        if (lignes.Count == 0) return new ResultatDevis.Refuse("commande-vide");
        if (lignes.Any(ligne => string.IsNullOrWhiteSpace(ligne.Reference) || ligne.Quantite <= 0))
            return new ResultatDevis.Refuse("ligne-invalide");

        var commande = new CommandeAchat();
        foreach (var ligne in lignes)
        {
            decimal? prix = catalogue.PrixUnitaire(ligne.Reference);
            if (prix is null) return new ResultatDevis.Refuse("article-inconnu:" + ligne.Reference);
            if (prix < 0) throw new InvalidOperationException("Contrat du catalogue viole.");
            commande.Ajouter(ligne.Reference, ligne.Quantite, prix.Value);
        }
        commande.Valider();
        decimal brut = commande.Total;
        return new ResultatDevis.Accepte(brut, remise.Appliquer(brut));
    }
}

public sealed class CatalogueMemoire(IReadOnlyDictionary<string, decimal> prix) : IPrixArticle
{
    public decimal? PrixUnitaire(string reference) => prix.TryGetValue(reference, out decimal montant) ? montant : null;
}

public static class CompositionDevis
{
    public static CalculateurDevis Creer() => new(
        new CatalogueMemoire(new Dictionary<string, decimal> { ["CLAVIER"] = 80m, ["SOURIS"] = 20m }),
        new RemiseFidelite(0.10m));
}