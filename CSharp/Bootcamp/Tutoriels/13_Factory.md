# 13 - Simple factory : choisir une implementation

[Precedent](12_Adapter.md) | [Sommaire](README.md) | [Suite : domaine](14_Domaine.md)

## Situation et objectif

Un utilisateur choisit un export JSON ou XML. Le consommateur ne devrait pas repeter
partout les memes conditions de construction.
Prerequis : interface, switch et collections. Utiliser les bibliotheques de serialization standard,
pas une concatenation manuelle fragile face aux caracteres speciaux.

## Exemple guide

```csharp
using System.Text.Json;
using System.Xml.Linq;

Ligne[] lignes = [new("A<&", 12.99m)];
IExport exporteur = Fabrique.Creer(" JSON ");
string contenu = exporteur.Exporter(lignes);
Ligne[] relues = JsonSerializer.Deserialize<Ligne[]>(contenu)!;
Console.WriteLine(relues[0].Reference);
Console.WriteLine(relues[0].Montant.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));

public sealed record Ligne(string Reference, decimal Montant);
public interface IExport
{
    string Exporter(IReadOnlyList<Ligne> lignes);
}

public sealed class JsonExport : IExport
{
    public string Exporter(IReadOnlyList<Ligne> lignes) => JsonSerializer.Serialize(lignes);
}

public sealed class XmlExport : IExport
{
    public string Exporter(IReadOnlyList<Ligne> lignes) => new XElement("lignes",
        lignes.Select(ligne => new XElement("ligne", new XAttribute("reference", ligne.Reference),
            new XElement("montant", ligne.Montant)))).ToString();
}

public static class Fabrique
{
    public static IExport Creer(string format) => format.Trim().ToLowerInvariant() switch
    {
        "json" => new JsonExport(),
        "xml" => new XmlExport(),
        _ => throw new ArgumentException("Format non supporte", nameof(format)),
    };
}
```

Sortie : `A<&`, puis `12.99`. Le test relit le JSON au lieu d'imposer une mise en forme arbitraire.

## Etapes de conception

1. IExport exprime l'operation commune, pas le format.
2. Chaque exporteur connait sa bibliotheque de serialization.
3. La factory normalise le choix et cree le bon objet.
4. Un format inconnu est rejete, pas remplace silencieusement par JSON.
5. Le consommateur travaille ensuite avec IExport.

Il s'agit d'une simple factory. Le GoF Factory Method repose sur un point de creation redefini
par heritage ; Abstract Factory cree des familles coherentes d'objets. Ces noms ne sont pas synonymes.
Avec deux formats fixes, un switch lisible est une solution valable.

## A vous : verifier le XML et les erreurs

Changer la demande en `xml`. Relire avec XDocument et verifier reference et montant.
Tester une liste vide et le format `yaml`. Pourquoi comparer les documents parses plutot
que leur chaine complete quand l'indentation n'est pas dans le contrat ?

<details>
<summary>Correction expliquee</summary>

```csharp
var document = XDocument.Parse(Fabrique.Creer("xml").Exporter(lignes));
var ligne = document.Root!.Element("ligne")!;
Console.WriteLine((string?)ligne.Attribute("reference"));
Console.WriteLine((decimal)ligne.Element("montant")!);
```

Ce fragment remplace le traitement JSON du debut, en gardant les types.
La reference retrouvee vaut `A<&` : XElement a gere l'echappement lors de l'ecriture.
Le decimal relu vaut 12.99, independamment du format d'affichage de la console.
Une liste vide donne une racine sans enfants ligne ; yaml leve ArgumentException.
Comparer la structure evite de coupler le test aux espaces et retours a la ligne non contractuels.

</details>

## Verification et TP

- La factory convertit-elle du JSON en XML ? Non, elle choisit qui exporte.
- Faut-il un registre de plugins pour deux formats ? Pas sans besoin d'extension dynamique.
- Ajouter un exporteur exige-t-il de modifier la factory ? Oui ici, c'est un choix local explicite.

Terminer ExportJson, ExportXml et FabriqueExport dans
[Lecon14_AdapterFactory.cs](../Lecons/Lecon14_AdapterFactory.cs), puis `dotnet run -- 14`.