# Bootcamp C#

16 leçons pour pratiquer le C# **en faisant passer des tests** : 8 rappels fondamentaux,
puis un parcours avancé sur la mémoire, la concurrence, les design patterns et l'architecture.

**Pour apprendre sujet par sujet, commencez par les [tutoriels guidés](Tutoriels/README.md).**
Chaque tutoriel explique une notion, déroule un exemple et propose un exercice avec correction commentée.
Le [guide avancé](PARCOURS_AVANCE.md) sert de mémo, et les leçons servent de travaux pratiques.
Les leçons 1 à 8 restent disponibles comme rappels, sans obligation de les terminer d'abord.

## Démarrer

Prérequis : le [SDK .NET 8](https://dotnet.microsoft.com/download) (ou Visual Studio 2022).

```bash
cd CSharp/Bootcamp
dotnet run          # lance les tests de toutes les leçons
dotnet run -- 3     # lance seulement les tests de la leçon 3
dotnet run -- 9     # commence directement le parcours avancé
```

Le séparateur `--` est nécessaire : `dotnet run -- 1`, pas `dotnet run --1`.
Un exercice non commencé affiche `… (à faire)` ; tant qu'il en reste, le code de sortie vaut 1.

Pour exécuter les corrigés, sans écraser votre travail :

```bash
dotnet run -p:Corriges=true -- 9
dotnet run -p:Corriges=true        # vérifie les 78 tests des 16 leçons
dotnet run -- 9                   # revient aux exercices
```

Ne mettez pas `--no-build` en changeant de mode : exercices et corrigés utilisent le même binaire de sortie.

## Comment travailler

1. Ouvrez la leçon dans `Lecons/` et lisez le bloc « À retenir » en haut du fichier.
2. Remplacez chaque `throw new NotImplementedException();` par votre code.
3. Relancez `dotnet run -- <numéro>` jusqu'à ce que tout soit vert ✔.
4. Bloqué ? Le corrigé est dans `Solutions/` (même nom de fichier). Essayez d'abord seul !

Pour les leçons avancées, lisez aussi la section correspondante du guide : prédisez le résultat
des exemples avant de les exécuter, puis justifiez les compromis de votre solution.
Les constructeurs à compléter doivent conserver les dépendances nécessaires aux méthodes.

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
| 09 | Mémoire et copies | struct, class, ref, record, alias, copie superficielle et instantané |
| 10 | Boxing et génériques | unboxing exact, nullable, IEquatable, collections typées |
| 11 | Allocations et ressources | Span, ArrayPool, finally, IDisposable, propriété des flux |
| 12 | Async et concurrence | annulation, parallélisme borné, ordre des résultats, Interlocked |
| 13 | Strategy et Decorator | composition, injection, arrondi monétaire, journalisation |
| 14 | Adapter et simple factory | contrat externe, centimes/euros, exports JSON/XML |
| 15 | Modèle métier | agrégat, invariants, encapsulation, transitions d'état |
| 16 | Architecture | cas d'usage, ports/adaptateurs, composition root, refus métier et pannes |

Les leçons 9 à 12 sont indépendantes. Faites ensuite 13 → 14 → 15 → 16 : le dernier cas d'usage
réutilise leurs types. Le guide ajoute des études de cas et un exercice d'ADR pour apprendre à
**choisir** une architecture, pas seulement reproduire un découpage.

## Structure

```
Bootcamp/
├── Lecons/      ← vos exercices (à compléter)
├── Solutions/   ← les corrigés (compilés uniquement avec -p:Corriges=true)
├── Tests/       ← les tests et le mini-moteur de test
├── Tutoriels/   ← le cours pas à pas, sujet par sujet
├── PARCOURS_AVANCE.md ← explications, exemples et études de cas
└── Program.cs   ← point d'entrée : lance les tests
```

> Astuce : faites un commit après chaque leçon terminée pour suivre votre progression.
