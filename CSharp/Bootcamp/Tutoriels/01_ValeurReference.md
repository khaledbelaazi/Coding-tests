# 01 - Valeur, reference et identite

[Sommaire](README.md) | [Suite : copies et ref](02_CopiesRef.md)

## Avant de commencer

Il suffit de savoir declarer une variable et appeler une methode. Une classe decrit des objets ;
`new` cree ici une instance. Une propriete permet de lire ou modifier une information de cette instance.

Objectif : distinguer **copier une valeur**, **copier une reference** et **creer un objet**.
Ne cherchez pas encore a placer chaque variable sur la stack ou le heap : c'est une autre question.

## Situation

Deux variables designent un compteur. Quand l'une le modifie, l'autre voit-elle le changement ?
La reponse depend du type et de ce qui est copie lors de l'affectation.

## Exemple a executer

Avant de lancer le programme, predire les quatre lignes affichees.

```csharp
var premier = new CompteurObjet { Valeur = 10 };
var second = premier;
second.Valeur++;
Console.WriteLine(premier.Valeur);
Console.WriteLine(ReferenceEquals(premier, second));

var valeurInitiale = new CompteurNombre { Valeur = 10 };
var valeurCopiee = valeurInitiale;
valeurCopiee.Valeur++;
Console.WriteLine(valeurInitiale.Valeur);
Console.WriteLine(valeurCopiee.Valeur);

public sealed class CompteurObjet
{
    public int Valeur { get; set; }
}

public struct CompteurNombre
{
    public int Valeur { get; set; }
}
```

Sortie :

```text
11
True
10
11
```

## Comprendre chaque etape

1. `new CompteurObjet` cree un objet avec une propriete egale a 10.
2. `premier` contient une reference permettant d'atteindre cet objet.
3. `second = premier` copie cette reference. Il n'y a toujours qu'un seul objet compteur.
4. `second.Valeur++` atteint cet objet et le modifie. `premier` atteint le meme objet.
5. Pour le struct, l'affectation copie la valeur, ici son champ entier.
6. L'increment de `valeurCopiee` ne change donc pas `valeurInitiale`.

```text
premier ----+
            +----> CompteurObjet { Valeur = 11 }
second -----+

valeurInitiale : CompteurNombre { Valeur = 10 }
valeurCopiee   : CompteurNombre { Valeur = 11 }
```

Une reference n'est pas l'objet : c'est ce qui permet de le retrouver.
`ReferenceEquals` verifie l'identite de deux objets, pas simplement l'egalite de leurs proprietes.
Deux objets differents peuvent avoir les memes donnees.

## A vous : reaffectation ou mutation ?

Apres `second.Valeur++`, ajouter `second = new CompteurObjet { Valeur = 99 };`.
Afficher les valeurs de `premier` et `second`, puis leur identite.
Pourquoi `premier` ne devient-il pas egal a 99 ?

Indice : dessiner une nouvelle boite objet et deplacer uniquement la fleche `second`.

<details>
<summary>Correction expliquee</summary>

```csharp
second = new CompteurObjet { Valeur = 99 };
Console.WriteLine(premier.Valeur);
Console.WriteLine(second.Valeur);
Console.WriteLine(ReferenceEquals(premier, second));
```

Resultats : `11`, `99`, `False`. L'affectation remplace la reference stockee dans `second`.
Elle ne modifie ni la variable `premier`, ni l'objet que celle-ci designe.
Cela distingue une **mutation de l'objet partage** d'une **reaffectation de variable**.

</details>

## Verification avant de continuer

- `var` choisit-il la semantique valeur/reference ? Non : il laisse le compilateur deduire le type.
- `class` signifie-t-il « nouvelle copie a chaque affectation » ? Non : la reference est copiee.
- `struct` implique-t-il « toujours sur la stack » ? Non : un struct peut notamment etre un champ d'un objet.

TP : implementer uniquement `IncrementerCopie` dans [Lecon09_Memoire.cs](../Lecons/Lecon09_Memoire.cs).
Lancer `dotnet run -- 9` ; les autres exercices de cette lecon peuvent rester a faire.

Vous etes pret pour la suite quand vous pouvez dessiner les deux situations sans executer le programme.