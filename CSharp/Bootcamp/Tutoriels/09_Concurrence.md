# 09 - Concurrence bornee et etat partage

[Precedent](08_Async.md) | [Sommaire](README.md) | [Suite : Strategy](10_Strategy.md)

## Situation

Charger mille articles ne doit pas ouvrir mille operations externes simultanees.
Il faut limiter les operations actives, conserver l'ordre des resultats et proteger l'etat partage.
Prerequis : await et annulation du tutoriel 08.

## Exemple guide

```csharp
int[] entrees = [3, 1, 2, 4];
int[] resultats = new int[entrees.Length];
int actifs = 0;
var options = new ParallelOptions { MaxDegreeOfParallelism = 2 };

await Parallel.ForEachAsync(Enumerable.Range(0, entrees.Length), options, async (index, token) =>
{
    int courant = Interlocked.Increment(ref actifs);
    try
    {
        if (courant > 2) throw new InvalidOperationException("Limite depassee");
        token.ThrowIfCancellationRequested();
        await Task.Yield();
        resultats[index] = entrees[index] * 10;
    }
    finally
    {
        Interlocked.Decrement(ref actifs);
    }
});

Console.WriteLine(string.Join(",", resultats));
Console.WriteLine(actifs);
```

Sortie : `30,10,20,40`, puis `0`.
Yield permet une suspension dans cette demonstration ; il ne simule ni debit ni latence reseau.

## Expliquer la conception

1. On distribue les **indices**, pour savoir ou ecrire chaque resultat.
2. Chaque iteration ecrit dans une case distincte. Pas de List partagee modifiee en concurrence.
3. La limite est de deux operations actives, pas une garantie que deux seront toujours simultanees.
4. `Interlocked.Increment` effectue une operation atomique sur le compteur d'observation.
5. `finally` le decremente aussi sur erreur.
6. L'await externe attend la fin de la boucle avant d'afficher le tableau.

`Task.WhenAll` attend des Tasks, mais ne limite pas combien ont deja ete lancees.
Un rate limiter limite une frequence ; une borne de concurrence limite un nombre en cours.
Un Channel borne peut limiter les elements en attente et imposer une backpressure au producteur.

## A vous : prouver la limite sans chronometre fragile

Imaginer un chargeur qui ne termine que quand le test lui donne un signal.
Lancer quatre entrees avec une borne de deux. Que verifier avant d'envoyer le signal ?
Puis calculer un compteur avec Parallel.For : pourquoi `compteur++` n'est-il pas suffisant ?

<details>
<summary>Correction expliquee</summary>

Attendre un signal indiquant que deux appels ont demarre, puis verifier que seuls deux sont entres
pendant qu'ils sont bloques. Liberer ensuite les appels et attendre la fin.
Les tests du bootcamp utilisent TaskCompletionSource avec continuations asynchrones et un delai
maximal de securite ; ce delai ne sert pas a mesurer une performance.

Pour le compteur :

```csharp
int compteur = 0;
Parallel.For(0, 100_000, index => Interlocked.Increment(ref compteur));
Console.WriteLine(compteur);
```

`compteur++` est une lecture, un calcul puis une ecriture ; deux iterations peuvent perdre
un increment. Interlocked fournit ici une seule operation atomique.
Si l'invariant concerne plusieurs champs, un verrou ou une conception a proprietaire unique peut etre necessaire.

</details>

## Erreurs et annulation

Passer le token du ParallelOptions, puis le token recu par l'iteration au chargeur.
La boucle peut combiner l'annulation appelante et son fonctionnement interne : verifier l'effet,
pas l'identite exacte des tokens. Ne pas avaler une exception pour remplir la case avec zero.
La fin d'une iteration n'implique pas que les autres sont deja terminees.

## Verification et TP

- L'ordre de fin impose-t-il l'ordre de sortie ? Non, si les indices sont preserves.
- Interlocked rend-il toute une classe thread-safe ? Non.
- La borne de concurrence limite-t-elle automatiquement les appels par seconde ? Non.

Implementer les deux methodes de [Lecon12_AsyncConcurrence.cs](../Lecons/Lecon12_AsyncConcurrence.cs),
puis lancer `dotnet run -- 12`. Savoir expliquer les tests de borne, ordre, erreur et annulation.