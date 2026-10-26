# 10 - Strategy : changer une regle sans changer son consommateur

[Precedent](09_Concurrence.md) | [Sommaire](README.md) | [Suite : Decorator](11_Decorator.md)

## Situation et prerequis

Un commerce applique soit aucun avantage, soit une remise de fidelite.
Le service qui additionne les prix doit-il connaitre chaque formule commerciale ?
Prerequis : interface, implementation, constructeur et delegation d'un appel.
Rappel : recevoir une interface permet de travailler avec plusieurs implementations du meme contrat.

## Exemple guide

```csharp
var standard = new Tarification(new SansRemise());
var fidele = new Tarification(new RemisePourcentage(0.10m));
Console.WriteLine(standard.Total(new[] { 80m, 20m }).ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
Console.WriteLine(fidele.Total(new[] { 80m, 20m }).ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));

public interface IRemise
{
    decimal Appliquer(decimal brut);
}

public sealed class SansRemise : IRemise
{
    public decimal Appliquer(decimal brut) => brut;
}

public sealed class RemisePourcentage(decimal taux) : IRemise
{
    public decimal Appliquer(decimal brut)
        => decimal.Round(brut * (1 - taux), 2, MidpointRounding.AwayFromZero);
}

public sealed class Tarification(IRemise remise)
{
    public decimal Total(IEnumerable<decimal> prix) => remise.Appliquer(prix.Sum());
}
```

Sortie : `100.00`, puis `90.00`. Les validations sont ajoutees dans l'exercice ci-dessous.
Les parametres apres le nom d'une classe sont des constructeurs primaires C# 12.
Un constructeur classique stockant la dependance dans un champ readonly convient aussi.

## Repartir les responsabilites

1. Tarification orchestre un calcul : sommer, puis appliquer une politique.
2. IRemise definit le point de variation : transformer un brut en net.
3. Chaque implementation porte sa formule.
4. Le programme appelant choisit et assemble les objets.
5. Tarification n'instancie ni RemisePourcentage ni un conteneur global.

C'est Strategy : une famille d'algorithmes partage un contrat et peut etre substituee.
L'injection par constructeur est le moyen de fournir la strategie. Aucun conteneur DI n'est requis.
Un champ interface n'est pas utile par magie : c'est le besoin de variation qui le justifie.

## A vous : rendre le contrat robuste

Refuser un brut negatif et un taux hors de [0,1] avec ArgumentOutOfRangeException.
Dans le service, refuser une ligne negative avant de sommer, meme si le total resterait positif.
Pour le TP, valider le taux lors d'Appliquer afin de correspondre au contrat des tests.
Pourquoi arrondir 2.01 euros avec une remise de 50 % donne-t-il 1.01 et pas 1.00 ?

<details>
<summary>Correction expliquee</summary>

```csharp
public decimal Appliquer(decimal brut)
{
    ArgumentOutOfRangeException.ThrowIfNegative(brut);
    if (taux is < 0 or > 1) throw new ArgumentOutOfRangeException(nameof(taux));
    return decimal.Round(brut * (1 - taux), 2, MidpointRounding.AwayFromZero);
}
```

Cette methode remplace celle de RemisePourcentage. SansRemise doit aussi refuser un brut negatif.
Dans Tarification, une boucle valide chaque montant avant de l'ajouter a l'accumulateur.
2.01 / 2 donne 1.005 ; AwayFromZero a deux decimales donne 1.01.
Cette convention doit etre explicite : toutes les devises et tous les systemes comptables n'ont pas
les memes regles d'arrondi. decimal ne decide pas de la politique a votre place.

</details>

## Quand ne pas utiliser Strategy ?

Pour deux formules locales, stables et sans autre dependance, un switch ou une fonction peut suffire.
Strategy devient utile quand les regles evoluent independamment ou portent leurs propres dependances.
Ne pas confondre remplacement de regle et accumulation de comportements : sujet du Decorator.

## Verification et TP

- Qui choisit la politique ? Le code de composition, pas Tarification.
- Faut-il modifier Tarification pour une nouvelle remise ? Pas si le contrat reste adapte.
- Une interface elimine-t-elle tous les changements futurs ? Non, un contrat peut devenir insuffisant.

Dans [Lecon13_StrategyDecorator.cs](../Lecons/Lecon13_StrategyDecorator.cs), implementer SansRemise,
RemiseFidelite et ServiceTarification. Lancer `dotnet run -- 13` ; le journal reste pour la suite.