# 03 - Boxing et unboxing

[Precedent](02_CopiesRef.md) | [Sommaire](README.md) | [Suite : generiques](04_Generiques.md)

## Le probleme

Une API accepte `object`, mais vous lui fournissez un `int`. Une reference vers quoi est creee ?
Objectif : expliquer cette conversion et recuperer la valeur sans confondre unboxing et conversion numerique.
Prerequis : difference entre valeur et reference, puis casts simples comme `(long)42`.

## Exemple guide

```csharp
int nombre = 42;
object boite = nombre;
nombre = 99;
Console.WriteLine((int)boite);

try
{
    long incorrect = (long)boite;
    Console.WriteLine(incorrect);
}
catch (InvalidCastException)
{
    Console.WriteLine("La boite contient un int, pas un long");
}

long correct = (long)(int)boite;
Console.WriteLine(correct);

int? absent = null;
int? present = 7;
object? boiteVide = absent;
object? boitePleine = present;
Console.WriteLine(boiteVide is null);
Console.WriteLine(boitePleine is int);
```

Sortie : `42`, le message de l'exception, `42`, `True`, `True`.

## Etape par etape

1. `object boite = nombre` effectue un boxing : une copie de l'int est placee dans un objet.
2. Le changement de `nombre` n'affecte pas la copie contenue dans la boite.
3. `(int)boite` demande de deballer un int. Le type contenu correspond : cela fonctionne.
4. `(long)boite` demande de deballer directement un long. Le type contenu ne correspond pas : echec.
5. `(long)(int)boite` deballe d'abord l'int, puis effectue une conversion numerique vers long.

Le fait qu'un long puisse representer tous les int ne change pas la regle de l'unboxing.
Une boite contenant `42L` contient un long : `(int)` ne peut pas la deballer directement non plus.

Pour `int?`, le boxing d'une absence donne `null`, celui d'une presence donne un int boxe.
Ne pas chercher une instance boxee de `Nullable<int>` dans ce cas.

## A vous : accepter uniquement un int boxe

Ecrire `static long LireEntier(object? valeur)` :

- accepter un int positif, nul ou negatif ;
- renvoyer cette valeur en long ;
- lever `InvalidCastException` pour long, string et null.

Indice : le pattern `valeur is int entier` verifie le type et extrait la valeur.
Ne pas utiliser `Convert.ToInt64` : cela accepterait d'autres types que le contrat demande.

<details>
<summary>Correction expliquee</summary>

```csharp
static long LireEntier(object? valeur)
{
    if (valeur is int entier) return (long)entier;
    throw new InvalidCastException("Un int boxe est requis.");
}
```

Le pattern extrait un int seulement si le type convient. L'elargissement en long arrive ensuite.
L'exception sur null est ici une decision du contrat ; ce n'est pas le comportement de tout cast
non nullable sur null, qui peut produire `NullReferenceException`.

</details>

## Pourquoi c'est utile en pratique

Un passage massif par `object`, une collection non generique ou une conversion de struct en interface
peut multiplier les boxings. Le JIT peut eliminer certaines allocations : il faut mesurer
le chemin reel, pas annoncer un cout fixe garanti dans chaque version du runtime.
Il ne faut pas non plus remplacer une interface utile uniquement sur la base d'une intuition.

## Verification et TP

- Pourquoi la boite reste-t-elle a 42 ? Elle contient une copie de la valeur initiale.
- Pourquoi `(long)boite` echoue-t-il ? L'unboxing exige ici le type valeur contenu exact.
- Que devient `int?` sans valeur dans object ? `null`.

Implementer `DeballerPuisElargir` et `BoxerNullable` dans
[Lecon10_BoxingGeneriques.cs](../Lecons/Lecon10_BoxingGeneriques.cs), puis `dotnet run -- 10`.
Les exercices de generiques seront faits au prochain tutoriel.