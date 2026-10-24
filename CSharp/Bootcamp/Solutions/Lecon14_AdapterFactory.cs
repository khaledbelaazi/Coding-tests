using System.Text.Json;
using System.Xml.Linq;

namespace Bootcamp.Lecons;

public interface IPrixArticle
{
    decimal? PrixUnitaire(string reference);
}

public interface IFournisseurCentimes
{
    long? LireCentimes(string reference);
}

public sealed class AdaptateurPrix(IFournisseurCentimes fournisseur) : IPrixArticle
{
    public decimal? PrixUnitaire(string reference)
    {
        long? centimes = fournisseur.LireCentimes(reference);
        if (centimes < 0) throw new InvalidOperationException("Prix fournisseur negatif.");
        return centimes / 100m;
    }
}

public sealed record LigneExport(string Reference, decimal Montant);
public interface IExporteur
{
    string Exporter(IReadOnlyList<LigneExport> lignes);
}

public sealed class ExportJson : IExporteur
{
    public string Exporter(IReadOnlyList<LigneExport> lignes) => JsonSerializer.Serialize(lignes);
}

public sealed class ExportXml : IExporteur
{
    public string Exporter(IReadOnlyList<LigneExport> lignes) =>
        new XElement("lignes", lignes.Select(ligne =>
            new XElement("ligne", new XAttribute("reference", ligne.Reference),
                new XElement("montant", ligne.Montant)))).ToString();
}

public static class FabriqueExport
{
    public static IExporteur Creer(string format) => format.Trim().ToLowerInvariant() switch
    {
        "json" => new ExportJson(),
        "xml" => new ExportXml(),
        _ => throw new ArgumentException("Format non supporte.", nameof(format)),
    };
}