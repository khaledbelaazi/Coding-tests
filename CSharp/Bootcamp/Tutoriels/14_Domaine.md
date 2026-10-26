# 14 - Modele metier : proteger les invariants

[Precedent](13_Factory.md) | [Sommaire](README.md) | [Suite : architecture](15_Architecture.md)

## Situation et objectif

Une commande ne doit plus accepter de lignes apres validation. Un simple objet avec des setters
publics laisse chaque appelant contourner cette regle. Le domaine doit proteger sa coherence.
Prerequis : encapsulation, listes, exceptions et immuabilite des tutoriels precedents.

Un invariant est une regle qui doit rester vraie apres chaque operation publique reussie.
Ici : lignes valides, et ajout uniquement lorsque la commande est en brouillon.

## Exemple guide

```csharp
var commande = new Commande();
commande.Ajouter("A", 2, 12.50m);
commande.Valider();
Console.WriteLine(commande.Etat);
Console.WriteLine(commande.Total.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
try
{
    commande.Ajouter("B", 1, 5m);
}
catch (InvalidOperationException)
{
    Console.WriteLine("Modification refusee");
}

public enum EtatCommande { Brouillon, Validee }
public sealed record Ligne(string Reference, int Quantite, decimal Prix);

public sealed class Commande
{
    private readonly List<Ligne> _lignes = new();
    public EtatCommande Etat { get; private set; } = EtatCommande.Brouillon;
    public IReadOnlyList<Ligne> Lignes => _lignes.ToArray();
    public decimal Total => _lignes.Sum(ligne => ligne.Quantite * ligne.Prix);

    public void Ajouter(string reference, int quantite, decimal prix)
    {
        if (Etat != EtatCommande.Brouillon) throw new InvalidOperationException();
        ArgumentException.ThrowIfNullOrWhiteSpace(reference);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantite);
        ArgumentOutOfRangeException.ThrowIfNegative(prix);
        _lignes.Add(new Ligne(reference, quantite, prix));
    }

    public void Valider()
    {
        if (Etat != EtatCommande.Brouillon || _lignes.Count == 0)
            throw new InvalidOperationException();
        Etat = EtatCommande.Validee;
    }
}
```

Sortie : `Validee`, `25.00`, `Modification refusee`.
Cette version de demonstration n'a que deux etats ; le TP ajoute l'annulation.

## Comprendre les choix

1. La liste mutable est privee : aucun appelant ne peut contourner Ajouter avec un Add direct.
2. Les lignes sont des records immuables avec des membres eux-memes immuables.
3. Le tableau retourne est un instantane : le modifier ne change pas la liste interne.
4. Les validations arrivent avant l'insertion ; une entree invalide ne laisse pas une demi-modification.
5. Etat ne possede pas de setter public : on demande une operation metier, pas une valeur arbitraire.
6. Le total est calcule par le domaine, pas recopie dans tous les consommateurs.

Cet objet joue le role d'agregat : un point d'entree protege un ensemble coherent de donnees.
Un DTO transporte des donnees mais n'est pas oblige de porter ces comportements.
Les distinguer ne signifie pas qu'il faut un modele riche pour toute table CRUD.

## A vous : ajouter une annulation terminale

Autoriser Brouillon -> Annulee et Validee -> Annulee.
Interdire tout nouvel ajout, nouvelle validation et double annulation une fois annulee.
Conserver les lignes pour l'historique. Dessiner les transitions avant de coder.

<details>
<summary>Correction expliquee</summary>

Ajouter Annulee a l'enum, puis cette methode dans Commande :

```csharp
public void Annuler()
{
    if (Etat == EtatCommande.Annulee)
        throw new InvalidOperationException("Deja annulee");
    Etat = EtatCommande.Annulee;
}
```

Les gardes existantes d'Ajouter et Valider n'acceptent que Brouillon : elles rejettent donc deja Annulee.
La methode ne vide pas la liste, conformement au contrat.
Une enum avec gardes suffit ici ; le pattern State devient interessant quand les comportements
par etat sont assez complexes pour justifier des objets distincts.

</details>

## Ce que ce modele ne garantit pas

L'instantane alloue un tableau a chaque lecture : c'est un compromis de simplicite.
Le modele n'est pas thread-safe. Deux processus peuvent aussi charger la meme commande et
decider des modifications contradictoires. Une transaction ou version de concurrence en base
est alors necessaire ; une propriete privee en C# ne remplace pas cette protection.
Ce modele ne garantit aucune reservation de stock externe ni aucun paiement.

## Verification et TP

- Peut-on contourner Ajouter par la collection publique ? Pas avec cet instantane.
- Un setter prive suffit-il pour deux serveurs concurrents ? Non.
- Un agregat doit-il contenir tout le systeme ? Non, sa frontiere correspond a des invariants coherents.

Implementer [Lecon15_ModeleMetier.cs](../Lecons/Lecon15_ModeleMetier.cs), puis `dotnet run -- 15`.
Justifier les tests de transitions et d'absence de mutation partielle.