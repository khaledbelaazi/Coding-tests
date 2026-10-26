# 11 - Decorator : ajouter un comportement par composition

[Precedent](10_Strategy.md) | [Sommaire](README.md) | [Suite : Adapter](12_Adapter.md)

## Situation

Vous voulez journaliser les totaux sans ajouter Console.WriteLine dans toutes les remises.
Une Strategy change la regle ; un Decorator enveloppe une regle en gardant son contrat.
Prerequis : interface et injection par constructeur du tutoriel 10.

## Exemple guide

```csharp
var messages = new List<string>();
IRemise politique = new RemiseFixe();
IRemise auditee = new RemiseJournalisee(politique, messages.Add);
decimal net = auditee.Appliquer(100m);
Console.WriteLine(net.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
Console.WriteLine(messages.Single());

public interface IRemise
{
    decimal Appliquer(decimal brut);
}

public sealed class RemiseFixe : IRemise
{
    public decimal Appliquer(decimal brut) => brut * 0.90m;
}

public sealed class RemiseJournalisee(IRemise interne, Action<string> journal) : IRemise
{
    public decimal Appliquer(decimal brut)
    {
        decimal net = interne.Appliquer(brut);
        journal(FormattableString.Invariant($"total={net:0.00}"));
        return net;
    }
}
```

Sortie : `90.00`, puis `total=90.00`.
Action<string> est une fonction recevant une chaine et ne retournant rien.
Le delegate messages.Add permet ici d'observer le journal sans dependance a une console.

## Suivre un appel

1. L'appel arrive sur auditee, qui expose la meme interface que politique.
2. Le Decorator delegue une seule fois le calcul a interne.
3. Il garde le resultat, puis journalise le succes.
4. Il retourne exactement ce resultat, sans recalcul ni deuxieme appel.

Journaliser avant l'appel ne permettrait pas d'annoncer honnetement un succes.
Recalculer le total pour le journal pourrait doubler un effet de bord ou obtenir une autre valeur.
Conserver le resultat localement rend le comportement explicite.

## A vous : que faire en cas d'erreur ?

Faire lever InvalidOperationException par la politique interne.
Verifier qu'aucun message de succes n'est ajoute. Faire ensuite lever une exception par le journal.
Le consommateur doit-il recevoir le total ou l'erreur ? Choisir avant de coder.

<details>
<summary>Correction raisonnee</summary>

Une exception interne interrompt l'execution avant le journal : aucun faux succes.
Une exception du journal se propage aussi, car aucun catch ne la masque.
C'est le contrat du bootcamp ; dans un produit, un incident de telemetrie pourrait au contraire
devoir etre isole. Dans ce cas, traiter cette erreur explicitement et definir comment l'observer.
Un journal d'audit legal peut avoir des exigences differentes d'une trace de diagnostic.

Pour prouver la delegation unique, remplacer la politique par un double qui incremente un compteur
dans Appliquer. Apres un appel au Decorator, attendre exactement un appel interne.

</details>

## L'ordre des enveloppes compte

Avec un cache et un journal, Journal(Cache(Service)) observe aussi les succes venant du cache.
Cache(Journal(Service)) peut eviter le journal lors d'un hit.
Ce n'est pas simplement une pile de classes equivalente quelle que soit sa construction.

Ne pas ajouter de retry comme Decorator universel : une operation ayant des effets peut ne pas
etre idempotente. Retenter un paiement sans protocole peut debiter deux fois.

## Verification et TP

- Le Decorator doit-il heriter de la classe concrete ? Non, partager son contrat et composer suffit.
- Ajoute-t-il obligatoirement une nouvelle regle metier ? Non, ici il ajoute une observation.
- Peut-on masquer toute exception pour securiser ? Non : cela change le contrat.

Implementer RemiseJournalisee dans [Lecon13_StrategyDecorator.cs](../Lecons/Lecon13_StrategyDecorator.cs),
puis lancer `dotnet run -- 13`. Expliquer pourquoi la culture du journal est fixee explicitement.