# 08 - Async, await et annulation

[Precedent](07_SpanBuffers.md) | [Sommaire](README.md) | [Suite : concurrence](09_Concurrence.md)

## Situation et objectif

Votre programme attend un resultat lent. Bloquer le thread pendant toute l'attente est-il necessaire ?
`Task<T>` represente une operation et son resultat futur. `await` permet de reprendre le traitement
quand cette operation se termine, sans exiger de bloquer le thread pendant son attente.
Prerequis : methodes, exceptions, delegates simples et proprietaires des ressources.

## Exemple guide

```csharp
int resultat = await ChargerAsync(21, CancellationToken.None);
Console.WriteLine(resultat);

using var source = new CancellationTokenSource();
source.Cancel();
try
{
    await ChargerAsync(21, source.Token);
}
catch (OperationCanceledException)
{
    Console.WriteLine("Chargement annule");
}

static async Task<int> ChargerAsync(int valeur, CancellationToken token)
{
    await Task.Delay(20, token);
    return valeur * 2;
}
```

Sortie : `42`, puis `Chargement annule`. Delay simule seulement une attente : ce n'est pas un appel reseau.

## Ce qui se passe

1. L'appel execute la methode jusqu'a une attente qui n'est pas deja terminee.
2. La methode rend une Task a son appelant ; elle ne cree pas obligatoirement un nouveau thread.
3. Quand Delay est termine, le calcul reprend et produit 42.
4. L'appelant qui attend cette Task recoit le resultat.
5. Au second appel, le token est deja annule ; l'operation observe cette demande et la propage.

L'annulation est cooperative : transmettre un token a une methode qui ne le consulte jamais
n'arrete pas magiquement son travail. Celui qui possede le CancellationTokenSource demande
l'annulation ; les methodes appelees recoivent en general seulement le token.

## Une operation asynchrone n'est pas toujours un travail parallele

Pour une I/O, privilegier l'API async native, avec token si elle le permet.
Pour du calcul CPU sur une interface graphique, deplacer le travail avec Task.Run peut garder
l'interface reactive. Sur un serveur, cela ne cree pas de capacite CPU supplementaire.
Un `await` peut continuer immediatement si la Task est deja terminee.

Eviter `.Result` et `.Wait()` dans le code applicatif : ils bloquent et peuvent provoquer
des deadlocks dans certains contextes. Le moteur de tests console du bootcamp a une frontiere
synchrone particuliere ; ce n'est pas un modele a recopier dans une requete ou une UI.

## A vous : annuler un vrai travail en cours

Modifier ChargerAsync pour effectuer dix etapes avec une attente par etape.
Transmettre le token a chaque attente. Definir si un resultat partiel peut etre retourne.
Pour ce cours : l'annulation doit se propager, pas retourner un succes partiel silencieux.

<details>
<summary>Correction expliquee</summary>

```csharp
static async Task<int> ChargerAsync(int valeur, CancellationToken token)
{
    int total = 0;
    for (int etape = 0; etape < 10; etape++)
    {
        await Task.Delay(20, token);
        total += valeur;
    }
    return total;
}
```

Il s'agit d'un remplacement de la fonction precedente : son resultat nominal change.
Une demande d'annulation pendant une attente interrompt la methode avec OperationCanceledException.
Dans du calcul CPU long, ajouter des points d'observation comme `token.ThrowIfCancellationRequested()`.
Ne pas utiliser un delai de quelques millisecondes comme preuve fiable dans un test :
un signal explicite permet de savoir que le travail a effectivement demarre.

</details>

## Verification

- Async implique-t-il un nouveau thread ? Non.
- await Task.Delay bloque-t-il volontairement le thread comme Thread.Sleep ? Non.
- Un catch general doit-il transformer une annulation en resultat zero ? Non, pas avec ce contrat.

Lire la signature de `ChargerAsync` dans
[Lecon12_AsyncConcurrence.cs](../Lecons/Lecon12_AsyncConcurrence.cs).
Avant de l'implementer, continuer avec le tutoriel suivant : il ajoute la limite de concurrence et l'ordre.