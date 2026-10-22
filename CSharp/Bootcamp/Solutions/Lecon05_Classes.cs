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
        if (soldeInitial < 0)
            throw new ArgumentException("Le solde initial ne peut pas être négatif.");
        Solde = soldeInitial;
        Historique.Add($"Ouverture : {soldeInitial}");
    }

    /// <summary>Ajoute le montant au solde. Montant ≤ 0 → ArgumentException.
    /// Ajoute "Dépôt : {montant}" à l'historique.</summary>
    public void Deposer(decimal montant)
    {
        if (montant <= 0)
            throw new ArgumentException("Le montant doit être positif.");
        Solde += montant;
        Historique.Add($"Dépôt : {montant}");
    }

    /// <summary>Retire le montant. Montant ≤ 0 → ArgumentException.
    /// Si le solde est insuffisant, ne fait rien et renvoie false. Sinon renvoie true
    /// et ajoute "Retrait : {montant}" à l'historique.</summary>
    public bool Retirer(decimal montant)
    {
        if (montant <= 0)
            throw new ArgumentException("Le montant doit être positif.");
        if (montant > Solde)
            return false;
        Solde -= montant;
        Historique.Add($"Retrait : {montant}");
        return true;
    }

    /// <summary>Vire le montant vers un autre compte (retrait ici + dépôt là-bas).
    /// Renvoie false (et ne change rien) si le solde est insuffisant.</summary>
    public bool VirerVers(CompteBancaire destination, decimal montant)
    {
        if (!Retirer(montant))
            return false;
        destination.Deposer(montant);
        return true;
    }

    /// <summary>Renvoie par exemple "Khaled : 150 €".</summary>
    public override string ToString()
    {
        return $"{Titulaire} : {Solde} €";
    }
}
