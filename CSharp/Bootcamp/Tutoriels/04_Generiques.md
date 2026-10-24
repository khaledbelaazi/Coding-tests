# 04 - Generiques et contrats

[Precedent](03_Boxing.md) | [Sommaire](README.md) | [Suite : GC](05_GC.md)

## Situation et objectif

Vous voulez reutiliser une comparaison pour plusieurs types sans passer par `object`.
Un generique conserve l'information de type. Une contrainte exprime ce que le code peut demander a ce type.
Rappel : une interface decrit un contrat ; elle n'impose pas une classe de base commune.

## Exemple guide

```csharp
var premier = new Identifiant(5);
var second = new Identifiant(5);
Console.WriteLine(MemeValeur(premier, second));
Console.WriteLine(MemeValeur(42, 43));

var valeurs = new List<int> { int.MaxValue, int.MaxValue };
long total = 0;
foreach (int valeur in valeurs) total += valeur;
Console.WriteLine(total);

static bool MemeValeur<T>(T gauche, T droite) where T : struct, IEquatable<T>
    => gauche.Equals(droite);

public readonly record struct Identifiant(int Valeur);
```

Sortie : `True`, `False`, `4294967294`.

## Comprendre la signature

1. `<T>` introduit un parametre de type, pas une variable contenant un type a l'execution.
2. Les arguments `Identifiant` font deduire `T = Identifiant` au compilateur.
3. `where T : struct` restreint ici T aux types valeur non nullable.
4. `IEquatable<T>` garantit la presence d'une methode `Equals(T)`.
5. `gauche.Equals(droite)` peut donc appeler ce contrat sans convertir explicitement gauche en object/interface.

Le `record struct` genere notamment une egalite typee par membres.
Il convient ici a une petite valeur identifiee par son entier.
Une entite metier mutable dont l'identite survit aux changements de proprietes ne se modele
pas automatiquement avec une egalite portant sur toutes ses donnees.

## Comparer deux approches

`IEquatable<Identifiant> contrat = premier` convertit le struct vers une interface : boxing.
L'appel generique contraint ne requiert pas cette conversion dans son corps.
Il ne garantit pas que toute implementation imaginable d'Equals soit sans allocation interne.

De meme, `List<int>` conserve des int sans boxer chaque element.
L'enumeration via une interface peut cependant boxer l'enumerateur struct de certaines collections.
Toujours distinguer « pas de boxing par element » et « aucune allocation du tout ».

## A vous : somme reutilisable sans perdre le type

Ecrire `static long Somme(IEnumerable<int> valeurs)` avec une boucle.
Verifier une sequence vide, `[2, -3]`, et deux `int.MaxValue`.
Pourquoi un accumulateur int est-il insuffisant ?

Indice : convertir a la fin ne repare pas un depassement survenu pendant le calcul.

<details>
<summary>Correction expliquee</summary>

```csharp
static long Somme(IEnumerable<int> valeurs)
{
    long total = 0;
    foreach (int valeur in valeurs) total += valeur;
    return total;
}
```

L'accumulateur long fait effectuer chaque addition dans un domaine plus large.
Le resultat vide est naturellement zero. Le contrat `IEnumerable<int>` permet plusieurs sources
sans imposer une List, et sans perdre le type des elements.
Un long peut lui aussi deborder si la sequence est assez grande : « plus large » ne signifie pas « infini ».

</details>

## Choisir la bonne abstraction

Utiliser un generique lorsque l'algorithme est reellement commun a plusieurs types et qu'un
contrat clair decrit les operations necessaires. Ne pas ajouter cinq parametres de type
pour generaliser un seul cas d'utilisation sans besoin concret.

## Verification et TP

- Une contrainte permet-elle d'appeler une methode inconnue de T ? Elle rend certaines operations garanties.
- Un generique oblige-t-il a utiliser object ? Non, son interet est justement de conserver les types.
- L'egalite d'un identifiant est-elle l'identite d'un objet en memoire ? Non.

Terminer `MemeValeur` et `SommeSansBoxing` dans
[Lecon10_BoxingGeneriques.cs](../Lecons/Lecon10_BoxingGeneriques.cs).
Puis relancer `dotnet run -- 10` et expliquer chaque test, pas seulement sa couleur.