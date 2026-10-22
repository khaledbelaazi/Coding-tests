# Bootcamp C#

8 leçons progressives pour apprendre le C# **en faisant passer des tests**, du plus simple aux notions objet.

## Démarrer

Prérequis : le [SDK .NET 8](https://dotnet.microsoft.com/download) (ou Visual Studio 2022).

```bash
cd CSharp/Bootcamp
dotnet run          # lance les tests de toutes les leçons
dotnet run -- 3     # lance seulement les tests de la leçon 3
```

Au début, tous les tests affichent `… (à faire)`.

## Comment travailler

1. Ouvrez la leçon dans `Lecons/` et lisez le bloc « À retenir » en haut du fichier.
2. Remplacez chaque `throw new NotImplementedException();` par votre code.
3. Relancez `dotnet run -- <numéro>` jusqu'à ce que tout soit vert ✔.
4. Bloqué ? Le corrigé est dans `Solutions/` (même nom de fichier). Essayez d'abord seul !

| Symbole | Signification |
|---|---|
| ✔ | test réussi |
| ✘ | votre code renvoie un mauvais résultat (le message indique l'attendu et l'obtenu) |
| … | exercice pas encore commencé |

## Programme

| # | Leçon | Notions |
|---|---|---|
| 01 | Bases | variables, types, opérateurs, conversions |
| 02 | Conditions et boucles | if, switch, for, while, FizzBuzz, nombres premiers |
| 03 | Tableaux et listes | tableaux, `List<T>`, `Dictionary<K,V>`, `HashSet<T>` |
| 04 | Chaînes | interpolation, méthodes de `string`, `StringBuilder` |
| 05 | Classes | constructeur, propriétés, encapsulation : un **compte bancaire** |
| 06 | Interfaces | interface, héritage, polymorphisme : des **formes géométriques** |
| 07 | LINQ | Where, Select, OrderBy, GroupBy, Sum… sur une liste d'employés |
| 08 | Exceptions | throw, try/catch/finally, exception personnalisée, TryParse |

## Structure

```
Bootcamp/
├── Lecons/      ← vos exercices (à compléter)
├── Solutions/   ← les corrigés (non compilés)
├── Tests/       ← les tests et le mini-moteur de test
└── Program.cs   ← point d'entrée : lance les tests
```

> Astuce : faites un commit après chaque leçon terminée pour suivre votre progression.
