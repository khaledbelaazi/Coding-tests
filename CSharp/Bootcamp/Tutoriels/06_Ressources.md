# 06 - Dispose et propriete des ressources

[Precedent](05_GC.md) | [Sommaire](README.md) | [Suite : Span et buffers](07_SpanBuffers.md)

## Situation et objectif

Une methode recoit un flux et utilise un lecteur pour le lire. Doit-elle fermer le flux ?
Reponse : il faut un contrat de **propriete**. Celui qui cree une ressource est souvent son proprietaire,
mais une API peut explicitement transferer cette propriete.
Prerequis : exceptions et bloc `try/finally` ; relire la lecon 08 si necessaire.

## Exemple guide

```csharp
using System.Text;

using var flux = new MemoryStream(Encoding.UTF8.GetBytes("Bonjour"));
string texte = Lire(flux);
Console.WriteLine(texte);
Console.WriteLine(flux.CanRead);

static string Lire(Stream fluxEmprunte)
{
    using var lecteur = new StreamReader(fluxEmprunte, Encoding.UTF8, leaveOpen: true);
    return lecteur.ReadToEnd();
}
```

Sortie : `Bonjour`, puis `True`.

## Pourquoi ce code ferme le bon objet

1. Le programme appelant cree le MemoryStream et le possede.
2. La methode recoit une reference empruntee vers ce flux.
3. Elle cree un StreamReader : ce lecteur lui appartient.
4. `using` appelle Dispose sur le lecteur a la sortie de la methode, meme sur exception.
5. `leaveOpen: true` demande au lecteur de ne pas fermer le flux sous-jacent.
6. Le `using` de l'appelant fermera finalement le flux a la fin du programme.

Un `using var` agit a la fin de sa portee, pas immediatement a la fin de la ligne.
Un `using (...) { ... }` permet de choisir une portee plus courte.
Disposer un objet ne l'efface pas de la memoire managee : cela termine son usage de ressources.

## Le piege de la double responsabilite

Sans `leaveOpen`, l'appelant perdrait la possibilite de relire son flux apres l'appel.
Ce n'est pas toujours un bug : certaines API prennent volontairement possession de la ressource.
Le probleme est de le faire sans contrat clair.
Ne pas ajouter `using` a tous les parametres IDisposable « par prudence ».

## A vous : montrer l'interet de finally

Remplacer `return lecteur.ReadToEnd()` par une exception de test.
Entourer l'appel a Lire d'un `try/catch` dans le programme.
Verifier ensuite que le flux reste lisible. Pourquoi ce test ne prouve-t-il pas a lui seul
que Dispose a bien ete appele sur le lecteur ?

<details>
<summary>Correction expliquee</summary>

La transformation `using` garantit la tentative de Dispose lors de la sortie, y compris sur erreur.
`CanRead` confirme uniquement que le flux emprunte n'a pas ete ferme.
Pour observer directement la liberation d'une ressource possedee dans un test unitaire,
utiliser un double IDisposable enregistrant les appels a Dispose, puis verifier le compteur.
Ne pas deduire la liberation d'un objet simplement de la survie d'un autre.

</details>

## Et les ressources asynchrones ?

Une API implementant `IAsyncDisposable` peut necessiter une liberation asynchrone.
Dans une methode async, `await using` attend cette liberation. Il ne suffit pas qu'une API soit
utilisee par une methode async pour qu'elle exige automatiquement IAsyncDisposable.

Un finalizer est non deterministe et couteux. Il ne remplace pas IDisposable.
Pour posseder directement une ressource native, utiliser en priorite une abstraction existante
ou SafeHandle, plutot que bricoler un finalizer dans chaque classe.

## Verification et TP

- Le GC et Dispose font-ils le meme travail ? Non : accessibilite memoire et liberation de ressource sont distinctes.
- Une ressource empruntee doit-elle toujours etre fermee ? Non, cela depend de son contrat.
- `return` empeche-t-il le using de disposer le lecteur ? Non.

Implementer `LireSansFermer` dans [Lecon11_AllocationsRessources.cs](../Lecons/Lecon11_AllocationsRessources.cs).
Lancer `dotnet run -- 11`. Les tests Span et pool seront traites au prochain tutoriel.