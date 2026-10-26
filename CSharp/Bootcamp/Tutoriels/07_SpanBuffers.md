# 07 - Span et buffers loues

[Precedent](06_Ressources.md) | [Sommaire](README.md) | [Suite : async](08_Async.md)

## Situation et objectif

Un traitement examine une portion de texte et prepare un buffer temporaire.
Faut-il allouer une nouvelle chaine et un nouveau tableau a chaque appel ?
Deux outils repondent a des besoins differents : une **vue** evite une copie, un **pool** reutilise du stockage.
Prerequis : tableaux, exceptions et regles de propriete du tutoriel 06.

## Exemple guide

```csharp
using System.Buffers;

string texte = "xx123yy";
ReadOnlySpan<char> portion = texte.AsSpan(2, 3);
int somme = 0;
foreach (char chiffre in portion) somme += chiffre - '0';
Console.WriteLine(somme);

ArrayPool<byte> pool = ArrayPool<byte>.Shared;
byte[] buffer = pool.Rent(4);
try
{
    buffer.AsSpan(0, 4).Fill(42);
    int total = 0;
    foreach (byte valeur in buffer.AsSpan(0, 4)) total += valeur;
    Console.WriteLine(total);
}
finally
{
    pool.Return(buffer, clearArray: true);
}
```

Sortie : `6`, puis `168`. La longueur physique du tableau n'est pas affichee car elle peut varier.

## Premiere moitie : une vue

`AsSpan(2, 3)` designe trois caracteres existants, a partir de l'indice 2.
Il ne construit pas une chaine contenant "123" comme le ferait Substring.
Le support doit rester valide pendant l'utilisation de la vue.
`ReadOnlySpan` interdit de modifier les elements **a travers cette vue** ; il ne rend pas
necessairement immuable le stockage sous-jacent quand celui-ci est un tableau mutable.

En C# 12, un Span est un ref struct : pas de capture par lambda, pas de champ d'une classe,
pas de locale Span dans une methode async. `Memory<T>` peut representer un segment a conserver
au-dela d'une attente, mais sa presence ne resout pas automatiquement la propriete du buffer.

## Deuxieme moitie : une location

1. `Rent(4)` promet un tableau d'au moins quatre elements, pas exactement quatre.
2. Le tableau peut deja contenir des donnees d'un precedent utilisateur.
3. Seuls les quatre elements utiles sont remplis et lus.
4. `finally` garantit la tentative de restitution, meme si le traitement echoue.
5. Apres Return, ce code n'est plus autorise a utiliser le tableau : un autre locataire peut l'obtenir.

`clearArray: true` demande un nettoyage lors de la restitution au pool.
Ce n'est pas une garantie universelle d'effacement de toutes les copies d'un secret.
Le pooling peut retenir de la memoire et compliquer la duree de vie : pas d'optimisation sans mesure.

## A vous : gerer les erreurs et les tailles

Ecrire une fonction de somme des chiffres ASCII sur ReadOnlySpan : vide donne 0,
mais `12x` leve FormatException. Ensuite, dans le TP buffer, faire lever une exception au callback.
Le buffer est-il toujours rendu ? Comment le prouver sans inspecter le pool global ?

<details>
<summary>Correction expliquee</summary>

```csharp
static int SommeChiffres(ReadOnlySpan<char> texte)
{
    int total = 0;
    foreach (char caractere in texte)
    {
        if (caractere is < '0' or > '9') throw new FormatException();
        total += caractere - '0';
    }
    return total;
}
```

Le controle est fait avant la conversion du caractere en chiffre.
Pour le buffer, injecter un ArrayPool de test observant Rent et Return.
La lecon fournit deja ce test : il distingue la taille utile de la taille physique et observe
la restitution sur exception. Un test sur Shared serait moins precis et couplerait le test a son etat global.

</details>

## Verification et TP

- Une slice possede-t-elle son stockage ? Non.
- Peut-on traiter buffer.Length si seulement quatre octets sont utiles ? Pas sans changer le contrat.
- Peut-on garder le tableau dans le callback apres Return ? Non.

Terminer `SommeChiffres` et `AvecBuffer` dans
[Lecon11_AllocationsRessources.cs](../Lecons/Lecon11_AllocationsRessources.cs), puis `dotnet run -- 11`.