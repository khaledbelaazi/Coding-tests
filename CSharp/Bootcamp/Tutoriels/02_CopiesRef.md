# 02 - Copie, ref et immuabilite

[Precedent](01_ValeurReference.md) | [Sommaire](README.md) | [Suite : boxing](03_Boxing.md)

## Objectif et prerequis

Reprendre la distinction valeur/reference du tutoriel 01. Ici, nous allons voir ce qui arrive
quand une valeur contient elle-meme une reference, puis quand une methode recoit une variable.

## Situation

Vous voulez conserver l'etat d'un profil avant une modification.
Une copie de l'objet suffit-elle si le profil contient une liste modifiable ?

## Exemple guide

```csharp
var original = new Profil("Sara", new List<string> { "Lecture" });
var copie = original with { Nom = "Lea" };
copie.Roles.Add("Ecriture");
Console.WriteLine(original.Nom);
Console.WriteLine(original.Roles.Count);
Console.WriteLine(ReferenceEquals(original.Roles, copie.Roles));

int nombre = 10;
Incrementer(nombre);
Console.WriteLine(nombre);
IncrementerVariable(ref nombre);
Console.WriteLine(nombre);

static void Incrementer(int valeur) => valeur++;
static void IncrementerVariable(ref int valeur) => valeur++;

public sealed record Profil(string Nom, List<string> Roles);
```

Sortie : `Sara`, `2`, `True`, `10`, `11`, chacun sur sa ligne.

## Explication : deux problemes differents

### 1. Une copie superficielle

`with` cree un autre record et copie ses membres. Le nom est remplace ; la reference de `Roles`
est recopiee. La liste n'est pas reconstruite. Les deux profils referencent donc la meme liste.

Les chaines sont immuables : aucune operation ne modifie une instance de `string` en place.
Les listes sont mutables : `Add` change bien la liste existante.
Un record n'est pas automatiquement un graphe d'objets immuables.

### 2. Passage par valeur ou par reference

Sans `ref`, le parametre `valeur` est une variable locale initialisee avec une copie de `nombre`.
Son increment ne change pas la variable appelante.
Avec `ref`, le parametre donne acces a la variable appelante elle-meme ; son increment est visible.

Pour un objet passe sans `ref`, la reference est copiee : on peut modifier l'objet partage,
mais pas remplacer la reference stockee dans la variable appelante.
`ref` n'est donc pas requis pour appeler `Ajouter` sur une liste recue en parametre.

## A vous : conserver un instantane

Creer une methode `Instantane(Profil profil)` renvoyant un autre profil dont la liste est independante.
Vider ensuite la liste du profil original et verifier que la copie conserve ses roles.

Indice : `new List<string>(profil.Roles)` cree une autre liste contenant les memes elements.

<details>
<summary>Correction expliquee</summary>

Ajouter cette fonction locale avant la declaration du type :

```csharp
static Profil Instantane(Profil profil)
    => profil with { Roles = new List<string>(profil.Roles) };
```

Le record est copie par `with`, puis le membre `Roles` recoit une **nouvelle reference de liste**.
La copie des elements suffit ici car ce sont des string immuables.
Avec une `List<AdresseMutable>`, les adresses seraient encore partagees : il faudrait aussi
choisir comment copier les adresses, leurs sous-objets et leurs eventuels cycles.

</details>

## Quand choisir quoi ?

| Besoin | Option raisonnable |
|---|---|
| Calculer un resultat sans modifier l'entree | Retourner une nouvelle valeur |
| Modifier intentionnellement une variable appelante | `ref`, avec contrat clair |
| Historiser un petit etat mutable | Copie defensive adaptee au graphe |
| Partager durablement des donnees sans mutation | Types et collections reellement immuables |

`IReadOnlyList<T>` restreint les operations offertes par une reference ; il ne supprime pas
les autres references capables de modifier la liste. `readonly` ne rend pas non plus les objets
references recursivement immuables.

## Verification

- Une liste partagee devient-elle independante parce que le record est nouveau ? Non.
- `ref` copie-t-il profondement un objet ? Non : il permet d'atteindre une variable.
- Une copie defensive est-elle gratuite ? Non : elle alloue et copie des donnees.

TP : terminer `IncrementerSurPlace`, `Renommer` et `Instantane` dans
[Lecon09_Memoire.cs](../Lecons/Lecon09_Memoire.cs), puis lancer `dotnet run -- 9`.
Savoir expliquer pourquoi `Renommer` partage volontairement ce que `Instantane` isole.