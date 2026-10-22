using Bootcamp.Lecons;
using static Bootcamp.Tests.Verifier;

namespace Bootcamp.Tests;

public static class TestsLecon05
{
    public static IEnumerable<Test> Tests() =>
    [
        new(5, "Constructeur", () =>
        {
            var c = new CompteBancaire("Khaled", 100m);
            Egal(100m, c.Solde); Egal("Khaled", c.Titulaire); Sequence(["Ouverture : 100"], c.Historique);
            Leve<ArgumentException>(() => new CompteBancaire("X", -1m), "solde initial négatif");
        }),
        new(5, "Deposer", () =>
        {
            var c = new CompteBancaire("Khaled", 100m);
            c.Deposer(50m);
            Egal(150m, c.Solde); Egal("Dépôt : 50", c.Historique.Last());
            Leve<ArgumentException>(() => c.Deposer(0m), "dépôt de 0");
        }),
        new(5, "Retirer", () =>
        {
            var c = new CompteBancaire("Khaled", 100m);
            Egal(true, c.Retirer(30m)); Egal(70m, c.Solde);
            Egal(false, c.Retirer(500m), "solde insuffisant"); Egal(70m, c.Solde, "le solde ne doit pas changer");
            Leve<ArgumentException>(() => c.Retirer(-5m), "retrait négatif");
        }),
        new(5, "VirerVers", () =>
        {
            var a = new CompteBancaire("A", 100m);
            var b = new CompteBancaire("B");
            Egal(true, a.VirerVers(b, 40m)); Egal(60m, a.Solde); Egal(40m, b.Solde);
            Egal(false, a.VirerVers(b, 1000m)); Egal(60m, a.Solde); Egal(40m, b.Solde);
        }),
        new(5, "ToString", () => Egal("Khaled : 150 €", new CompteBancaire("Khaled", 150m).ToString())),
    ];
}

public static class TestsLecon06
{
    public static IEnumerable<Test> Tests() =>
    [
        new(6, "Cercle", () => { var c = new Cercle(2); Proche(Math.PI * 4, c.Aire()); Proche(Math.PI * 4, c.Perimetre()); }),
        new(6, "Rectangle", () => { var r = new Rectangle(3, 4); Proche(12, r.Aire()); Proche(14, r.Perimetre()); }),
        new(6, "Carré (héritage)", () => { IForme c = new Carre(5); Proche(25, c.Aire()); Egal("Carré", c.Nom); }),
        new(6, "Triangle (Héron)", () => { var t = new Triangle(3, 4, 5); Proche(6, t.Aire()); Proche(12, t.Perimetre()); }),
        new(6, "AireTotale (polymorphisme)", () =>
            Proche(12 + 25 + 6, Lecon06.AireTotale([new Rectangle(3, 4), new Carre(5), new Triangle(3, 4, 5)]))),
        new(6, "PlusGrandeForme", () =>
            Egal("Cercle", Lecon06.PlusGrandeForme([new Rectangle(3, 4), new Cercle(3), new Carre(5)]))),
    ];
}

public static class TestsLecon07
{
    static readonly List<Employe> Equipe =
    [
        new("Sara", "IT", 4500m, 3),
        new("Karim", "IT", 3900m, 12),
        new("Léa", "RH", 3200m, 5),
        new("Yanis", "RH", 3000m, 1),
        new("Amine", "IT", 5200m, 8),
        new("Inès", "Ventes", 3600m, 2),
    ];

    public static IEnumerable<Test> Tests() =>
    [
        new(7, "NomsDuService", () => Sequence(["Amine", "Karim", "Sara"], Lecon07.NomsDuService(Equipe, "IT"))),
        new(7, "MasseSalariale", () => Egal(23400m, Lecon07.MasseSalariale(Equipe))),
        new(7, "SalaireMoyenParService", () =>
        {
            var moy = Lecon07.SalaireMoyenParService(Equipe);
            Egal(4533.33m, Math.Round(moy["IT"], 2)); Egal(3100m, moy["RH"]); Egal(3, moy.Count);
        }),
        new(7, "MieuxPayes", () => Sequence(["Amine", "Sara"], Lecon07.MieuxPayes(Equipe, 2))),
        new(7, "AuMoinsUnVeteran", () =>
        {
            Egal(true, Lecon07.AuMoinsUnVeteran(Equipe));
            Egal(false, Lecon07.AuMoinsUnVeteran(Equipe.Where(e => e.Service == "RH")));
        }),
    ];
}

public static class TestsLecon08
{
    public static IEnumerable<Test> Tests() =>
    [
        new(8, "LireAge", () =>
        {
            Egal(42, Lecon08.LireAge("42"));
            Leve<FormatException>(() => Lecon08.LireAge("abc"), "texte non numérique");
            Leve<AgeInvalideException>(() => Lecon08.LireAge("200"), "âge trop grand");
            Leve<AgeInvalideException>(() => Lecon08.LireAge("-1"), "âge négatif");
        }),
        new(8, "LireAgeOuNull", () =>
        {
            Egal<int?>(30, Lecon08.LireAgeOuNull("30"));
            Egal<int?>(null, Lecon08.LireAgeOuNull("trente"));
            Egal<int?>(null, Lecon08.LireAgeOuNull("999"));
        }),
        new(8, "TryDiviser", () =>
        {
            Egal(true, Lecon08.TryDiviser(10, 4, out double r)); Proche(2.5, r);
            Egal(false, Lecon08.TryDiviser(1, 0, out double r0)); Proche(0, r0);
        }),
        new(8, "ExecuterAvecJournal", () =>
        {
            Sequence(["début", "succès", "fin"], Lecon08.ExecuterAvecJournal(() => { }));
            Sequence(["début", "erreur : boum", "fin"], Lecon08.ExecuterAvecJournal(() => throw new InvalidOperationException("boum")));
        }),
    ];
}
