namespace Bootcamp.Lecons;

public interface IPrixArticle
{
    decimal? PrixUnitaire(string reference);
}

public interface IFournisseurCentimes
{
    long? LireCentimes(string reference);
}

/// <summary>
/// Adapter : traduire le contrat externe (centimes) vers le port de l'application (euros).
/// null signifie article inconnu. Centimes negatifs : InvalidOperationException (donnee externe invalide).
/// Diviser en decimal, pas en entier ; ne pas transformer une panne technique en article inconnu.
/// </summary>
public sealed class AdaptateurPrix : IPrixArticle
{
    public AdaptateurPrix(IFournisseurCentimes fournisseur) => throw new NotImplementedException();
    public decimal? PrixUnitaire(string reference) => throw new NotImplementedException();
}

public sealed record LigneExport(string Reference, decimal Montant);
public interface IExporteur
{
    string Exporter(IReadOnlyList<LigneExport> lignes);
}

/// <summary>Serialiser via System.Text.Json.JsonSerializer, sans fabriquer du JSON a la main.</summary>
public sealed class ExportJson : IExporteur
{
    public string Exporter(IReadOnlyList<LigneExport> lignes) => throw new NotImplementedException();
}

/// <summary>
/// Via System.Xml.Linq : racine lignes, enfants ligne avec attribut reference et enfant montant.
/// Laisser XElement echapper les caracteres speciaux et formater les decimal.
/// </summary>
public sealed class ExportXml : IExporteur
{
    public string Exporter(IReadOnlyList<LigneExport> lignes) => throw new NotImplementedException();
}

/// <summary>Simple factory, pas le GoF Factory Method : centraliser un choix de construction.</summary>
public static class FabriqueExport
{
    /// <summary>json/xml, casse et espaces ignores. Format inconnu : ArgumentException.</summary>
    public static IExporteur Creer(string format) => throw new NotImplementedException();
}