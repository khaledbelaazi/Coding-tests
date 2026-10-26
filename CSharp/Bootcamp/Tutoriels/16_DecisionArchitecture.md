# 16 - Choisir une architecture et ecrire une ADR

[Precedent](15_Architecture.md) | [Sommaire](README.md)

## Objectif

Nous avons appris a separer un cas d'usage et ses details. Cela ne signifie pas que cette organisation
soit le choix optimal pour chaque produit. Ce tutoriel est une etude de conception : plusieurs reponses
peuvent etre valables, mais chacune doit etre defendue avec les contraintes et ses consequences.

Une ADR, Architecture Decision Record, conserve le contexte d'une decision, ses alternatives,
ses raisons et ses couts. Ce n'est ni un catalogue de technologies ni une preuve que le choix est eternel.

## Etape 1 - Lire le probleme avant de nommer la solution

Sujet : une application de devis pour un commerce. Une equipe de cinq personnes, deux fournisseurs
externes, une base relationnelle et deploiement quotidien. Les promotions changent souvent.
Les devis ne reservent pas de stock. La future reservation devra traiter le dernier article disponible.
La charge mesuree est moderee et l'equipe n'exploite actuellement aucun broker.

Avant de proposer une architecture, repondre :

- Quelles operations doivent etre atomiques ?
- Quelles regles changent et quels details doivent etre remplacables ?
- Quelles pannes sont acceptables, et combien de temps peut durer une incoherence ?
- Quels volumes et objectifs de latence sont mesures, et lesquels restent inconnus ?
- Qui livre, qui exploite, et qui intervient sur incident ?

Ne pas inventer une exigence de millions de requetes pour justifier une architecture preferee.

## Etape 2 - Comparer des options realistes

| Option | Benefice principal | Cout ou risque |
|---|---|---|
| Monolithe simple en couches | Deploiement et transactions simples | Regles dispersees si les responsabilites sont mal tenues |
| Monolithe modulaire avec ports utiles | Frontieres metier et fournisseurs isolables | Discipline sur les acces intermodules et davantage de contrats |
| Services independants | Autonomie de livraison et de charge par capacite | Reseau, coherence distribuee, observabilite, versionnement et exploitation |

Ces options ne sont pas des niveaux de prestige. Un monolithe peut etre modulaire et utiliser
ports/adaptateurs. CQRS separe lectures et commandes ; il n'impose ni services independants
ni event sourcing. Commencer par un modele de lecture optimise peut suffire pour un tableau de bord.

## Etape 3 - Consigner la decision

Le programme suivant structure une decision deja prise par raisonnement humain.
Il ne calcule pas automatiquement la « bonne architecture » a partir de quelques nombres.

```csharp
var decision = new Adr(
    "Devis pour un commerce, une equipe, deux fournisseurs remplacables",
    "Monolithe modulaire avec ports aux frontieres fournisseurs",
    "Deploiement simple ; discipline necessaire entre les modules",
    "Reexaminer si une capacite exige une autonomie de livraison ou de charge mesuree");

Console.WriteLine($"Contexte : {decision.Contexte}");
Console.WriteLine($"Decision : {decision.Decision}");
Console.WriteLine($"Consequences : {decision.Consequences}");
Console.WriteLine($"Reevaluation : {decision.Reevaluation}");

public sealed record Adr(string Contexte, string Decision, string Consequences, string Reevaluation);
```

Sortie : quatre lignes, une par rubrique. Dans une vraie ADR, ajouter statut, date et alternatives.
Le document doit expliquer pourquoi une option moins complexe ne suffit pas ou pourquoi elle est retenue.

## Etape 4 - Mettre le choix a l'epreuve d'une panne

Deux clients reservent le dernier article. Lire le stock, verifier `stock > 0`, puis enregistrer plus tard
sans protection permet aux deux clients de croire qu'ils ont reussi.

Si le stock est local a la meme base, envisager une mise a jour conditionnelle atomique dans une transaction,
ou une version de concurrence avec gestion de conflit. Tester avec plusieurs transactions reelles.
Un lock C# dans une instance ne protege pas contre une autre instance du serveur.

Si le stock appartient a un fournisseur externe, une transaction SQL locale ne protege pas ce fournisseur.
Il faut son protocole de reservation, une cle d'idempotence si disponible, une confirmation et une reprise.
Apres timeout, l'issue peut etre **inconnue** : le fournisseur a peut-etre accepte avant la perte de reponse.
Une nouvelle tentative sans identite stable peut reserver deux fois.

Si un evenement doit etre publie avec une modification SQL, l'outbox permet de les enregistrer
dans la meme transaction locale. Le relais peut republier : le consommateur doit tolerer les doublons.
L'outbox ne rend pas magiquement tous les effets externes « exactement une fois ».

## A vous : ecrire votre ADR

Produire une page avec contexte, alternatives, decision, consequences et criteres de reevaluation.
Dessiner les dependances de code et un scenario de reservation, y compris timeout et doublon.
Ne pas implementer de microservices pour cet exercice : la preuve attendue est le raisonnement.

Indice : separer le besoin actuel (devis sans reservation) du besoin futur (reservation avec coherence).

<details>
<summary>Proposition de correction, pas reponse unique</summary>

**Decision** : monolithe modulaire, avec module commande et ports pour les fournisseurs.
Les politiques commerciales restent dans le domaine. Une seule application est deployee.

**Justification** : une equipe et une charge moderee ne justifient pas encore le cout de services
independants. La variation reelle concerne les fournisseurs et les regles commerciales ; les ports
et les modules isolent ces points sans introduire de pannes reseau internes.

**Alternative plus simple** : un monolithe en couches reste defensible si les regles sont finalement
peu nombreuses. Il faut alors montrer comment eviter leur dispersion dans les controllers ou l'acces aux donnees.

**Limites** : pas de deploiement independant des modules ; dependances intermodules a surveiller.
La reservation externe reste un protocole metier a concevoir, pas une garantie du calcul de devis.

**Reevaluation** : besoin mesure de charge ou d'isolation de panne par capacite, equipes autonomes,
et capacite d'exploitation suffisante. Pas simplement « le code a grossi ».

</details>

## Comment evaluer votre reponse

- Deux options au moins sont comparees avec des couts concrets.
- La frontiere de transaction est explicite et ne pretend pas englober un fournisseur externe.
- Les effets d'un timeout et d'un doublon sont traites.
- La decision peut etre revue sur des criteres observables.
- Le diagramme distingue dependances de code, appels a l'execution et unites de deploiement.

Les tests ne notent pas cette ADR. Pour d'autres situations, consulter les
[quatre etudes de cas du guide](../PARCOURS_AVANCE.md), puis reprendre la meme demarche.