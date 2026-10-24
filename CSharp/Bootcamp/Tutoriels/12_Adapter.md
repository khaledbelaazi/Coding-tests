# 12 - Adapter : traduire un contrat externe

[Precedent](11_Decorator.md) | [Sommaire](README.md) | [Suite : factory](13_Factory.md)

## Situation et objectif

L'application attend des euros en decimal. Un fournisseur renvoie des centimes en long.
Son API ne doit pas imposer son vocabulaire et ses unites a tout le domaine.
Prerequis : interfaces, types nullable et difference entre refus attendu et panne technique.

## Exemple guide

```csharp
IPrix catalogue = new Adaptateur(new AncienFournisseur());
decimal? prix = catalogue.LireEuros("A");
Console.WriteLine(prix?.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
Console.WriteLine(catalogue.LireEuros("INCONNU") is null);

public interface IPrix
{
    decimal? LireEuros(string reference);
}

public interface IPrixHistorique
{
    long? LireCentimes(string reference);
}

public sealed class AncienFournisseur : IPrixHistorique
{
    public long? LireCentimes(string reference) => reference == "A" ? 1299L : null;
}

public sealed class Adaptateur(IPrixHistorique fournisseur) : IPrix
{
    public decimal? LireEuros(string reference)
    {
        long? centimes = fournisseur.LireCentimes(reference);
        if (centimes < 0) throw new InvalidOperationException("Prix externe invalide");
        return centimes / 100m;
    }
}
```

Sortie : `12.99`, puis `True`.

## Comprendre la frontiere

1. L'application consomme IPrix, son propre besoin de lecture.
2. L'adaptateur utilise le contrat historique, pas le consommateur.
3. Il traduit les unites avec une division decimale : `100m`, pas `100`.
4. Il conserve l'absence : null n'est pas gratuit.
5. Il rejette un prix negatif : une donnee invalide ne doit pas devenir une donnee acceptee.

Les operations sur nullable propagent ici null lors de la division.
Pour une valeur nullable absente, le test `centimes < 0` vaut false :
aucune exception de prix negatif n'est levee, puis la division retourne null.

Un Adapter change le contrat expose. Un Decorator conserve le contrat et ajoute un comportement.
Tous deux peuvent contenir un autre objet, mais ils ne resolvent pas le meme probleme.

## A vous : ne pas mentir sur les pannes

Faire lever IOException par LireCentimes. Faut-il retourner null, zero, ou laisser l'erreur remonter ?
Puis ajouter un article a zero centime. Comment le distinguer d'un article inconnu ?

<details>
<summary>Correction raisonnee</summary>

Le contrat de cette lecon reserve null a une absence connue d'article.
Une IOException signifie que la lecture n'a pas abouti : l'absence n'est pas prouvee.
La panne remonte donc a une frontiere capable de decider quoi afficher ou retenter.
Un prix externe de 0 retourne `0m`, un prix absent retourne null.
Un catch general renvoyant null masquerait un incident et pourrait provoquer une mauvaise decision metier.

</details>

## Limites de cet adaptateur

Il suppose une meme devise. Passer des centimes de dollars a des euros demande une decision de change,
pas seulement une division par 100. Dans un produit multidevise, representer montant et devise.
L'adaptateur reel devra aussi traiter timeout, authentification et evolution du schema externe.
Un faux fournisseur prouve notre traduction, pas la compatibilite effective avec une API distante.

## Verification et TP

- Une absence est-elle une erreur reseau ? Non.
- Le contrat applicatif doit-il recopier le schema du fournisseur ? Pas necessairement.
- Peut-on remplacer l'adaptateur sans changer le cas d'usage ? Oui, si le nouveau respecte le port.

Implementer AdaptateurPrix dans [Lecon14_AdapterFactory.cs](../Lecons/Lecon14_AdapterFactory.cs),
puis lancer `dotnet run -- 14`. Les exports seront traites au prochain tutoriel.