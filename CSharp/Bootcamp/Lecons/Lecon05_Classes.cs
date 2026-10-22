namespace Bootcamp.Lecons;

/// <summary>
/// LEÇON 05 – Classes et objets : un compte bancaire
///
/// À retenir :
///  - Une classe regroupe des données (champs / propriétés) et des comportements (méthodes).
///  - Le constructeur initialise l'objet : new CompteBancaire("Khaled", 100m)
///  - Encapsulation : « private set » empêche de modifier le solde depuis l'extérieur,
///    on passe obligatoirement par Deposer / Retirer qui vérifient les règles.
///  - decimal (suffixe m : 10.5m) est le type à utiliser pour l'argent.
///  - Lever une erreur : throw new ArgumentException("message");
/// </summary>
public class CompteBancaire
{
    public string Titulaire { get; }
    public decimal Solde { get; private set; }
    public List<string> Historique { get; } = new();

    /// <summary>Crée le compte. Le solde initial ne peut pas être négatif (ArgumentException).
    /// Ajoute "Ouverture : {soldeInitial}" à l'historique.</summary>
    public CompteBancaire(string titulaire, decimal soldeInitial = 0m)
    {
        Titulaire = titulaire;
        throw new NotImplementedException(); // TODO : à vous de jouer !
    }

    /// <summary>Ajoute le montant au solde. Montant ≤ 0 → ArgumentException.
    /// Ajoute "Dépôt : {montant}" à l'historique.</summary>
    public void Deposer(decimal montant)
    {
        throw new NotImplementedException(); // TODO : à vous de jouer !
    }

    /// <summary>Retire le montant. Montant ≤ 0 → ArgumentException.
    /// Si le solde est insuffisant, ne fait rien et renvoie false. Sinon renvoie true
    /// et ajoute "Retrait : {montant}" à l'historique.</summary>
    public bool Retirer(decimal montant)
    {
        throw new NotImplementedException(); // TODO : à vous de jouer !
    }

    /// <summary>Vire le montant vers un autre compte (retrait ici + dépôt là-bas).
    /// Renvoie false (et ne change rien) si le solde est insuffisant.</summary>
    public bool VirerVers(CompteBancaire destination, decimal montant)
    {
        throw new NotImplementedException(); // TODO : à vous de jouer !
    }

    /// <summary>Renvoie par exemple "Khaled : 150 €".</summary>
    public override string ToString()
    {
        throw new NotImplementedException(); // TODO : à vous de jouer !
    }
}
