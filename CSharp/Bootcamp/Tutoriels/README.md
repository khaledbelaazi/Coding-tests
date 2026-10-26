# Cours C# : un sujet a la fois

Ce dossier est le support de cours. Il n'est pas necessaire de parcourir toutes les notions
en une seance : terminer un tutoriel signifie comprendre son exemple, faire son exercice
et savoir expliquer sa correction. Aucun rythme rapide n'est impose.

## Comment suivre un tutoriel

1. Lire la situation et le rappel, sans ouvrir la correction.
2. Predire le resultat du programme, puis executer l'exemple.
3. Relire l'explication instruction par instruction et dessiner les objets si necessaire.
4. Faire l'exercice et comparer avec la correction deployee en bas de page.
5. Repondre aux questions de verification. En cas de doute, rester sur ce sujet.
6. Faire le TP du bootcamp indique, puis passer au tutoriel suivant.

## Executer l'exemple d'une page

Depuis `CSharp/Bootcamp`, dans PowerShell :

```powershell
./Tutoriels/Executer-Exemple.ps1 -Tutoriel 1
./Tutoriels/Executer-Exemple.ps1 -Tutoriel 3
```

Le script extrait le premier exemple C# de la page, le compile dans un projet temporaire et l'execute.
Il ne modifie pas vos exercices et supprime son atelier temporaire ensuite.
`-Tous` execute tous les exemples du cours. Cette verification controle leur compilation et leur execution,
pas automatiquement les sorties annoncees ni les reponses aux exercices.

Pour modifier librement un exemple, utiliser un petit projet console separe.
Les exemples ne sont pas a coller dans le point d'entree du bootcamp : celui-ci lance ses tests.
Depuis `CSharp/Bootcamp`, ces commandes creent un atelier voisin, pas imbrique :

```bash
cd ..
dotnet new console -n AtelierCSharp --framework net8.0
cd AtelierCSharp
dotnet run
```

Le premier bloc C# de chaque tutoriel est un programme autonome pour le point d'entree de cet atelier.
Il remplace le programme de l'atelier precedent ; les blocs de correction correspondent a cet exemple,
et ne constituent pas tous des programmes autonomes. Aucun package externe n'est requis.
Les exemples ciblent .NET 8 / C# 12, avec les usings implicites du projet console standard.

## Itineraire

| Ordre | Un sujet principal | TP associe |
|---|---|---|
| 01 | [Valeur, reference et identite](01_ValeurReference.md) | Lecon 09, copie de struct |
| 02 | [Copie, ref et immuabilite](02_CopiesRef.md) | Lecon 09, ref et instantane |
| 03 | [Boxing et unboxing](03_Boxing.md) | Lecon 10, object et nullable |
| 04 | [Generiques et contrats](04_Generiques.md) | Lecon 10, egalite et somme |
| 05 | [GC et retention memoire](05_GC.md) | Observation et diagnostic |
| 06 | [Dispose et propriete des ressources](06_Ressources.md) | Lecon 11, flux emprunte |
| 07 | [Span et buffers loues](07_SpanBuffers.md) | Lecon 11, vues et pool |
| 08 | [Async, await et annulation](08_Async.md) | Preparation de la lecon 12 |
| 09 | [Concurrence bornee et etat partage](09_Concurrence.md) | Lecon 12 |
| 10 | [Strategy](10_Strategy.md) | Lecon 13, politiques |
| 11 | [Decorator](11_Decorator.md) | Lecon 13, journal |
| 12 | [Adapter](12_Adapter.md) | Lecon 14, prix fournisseur |
| 13 | [Simple factory](13_Factory.md) | Lecon 14, exports |
| 14 | [Modele metier et invariants](14_Domaine.md) | Lecon 15 |
| 15 | [Architecture et dependances](15_Architecture.md) | Lecon 16 |
| 16 | [Choisir une architecture avec une ADR](16_DecisionArchitecture.md) | Etude de cas finale |

Les tutoriels sont plus fins que les lecons : il est normal de ne resoudre qu'une methode du TP
apres une seance. Ne pas chercher a faire tous les tests d'une lecon avant d'avoir etudie ses sujets.

## Retrouver les travaux pratiques

Depuis `CSharp/Bootcamp` :

```bash
dotnet run -- 9
dotnet run -p:Corriges=true -- 9
```

La premiere commande teste votre travail dans [Lecons](../Lecons).
La seconde execute les implementations de [Solutions](../Solutions), sans ecraser vos fichiers.
Un exercice non implemente est affiche comme « a faire » et produit un code de sortie 1.
Les tests d'autres methodes de la meme lecon peuvent rester a faire pendant votre progression.

Les explications du cours et leurs exercices de reflexion ne sont pas automatiquement notes.
Les tests du bootcamp sont une verification complementaire, pas un remplacement du raisonnement.

**Commencer : [Tutoriel 01 - Valeur, reference et identite](01_ValeurReference.md).**