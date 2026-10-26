namespace Bootcamp.Lecons;

public enum EtatCommande { Brouillon, Validee, Annulee }
public sealed record LigneCommande(string Reference, int Quantite, decimal PrixUnitaire);

public sealed class CommandeAchat
{
    private readonly List<LigneCommande> _lignes = [];
    public EtatCommande Etat { get; private set; } = EtatCommande.Brouillon;
    public IReadOnlyList<LigneCommande> Lignes => _lignes.ToArray();
    public decimal Total => _lignes.Sum(ligne => ligne.Quantite * ligne.PrixUnitaire);

    public void Ajouter(string reference, int quantite, decimal prixUnitaire)
    {
        if (Etat != EtatCommande.Brouillon) throw new InvalidOperationException("Commande figee.");
        ArgumentException.ThrowIfNullOrWhiteSpace(reference);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantite);
        ArgumentOutOfRangeException.ThrowIfNegative(prixUnitaire);
        _lignes.Add(new(reference, quantite, prixUnitaire));
    }

    public void Valider()
    {
        if (Etat != EtatCommande.Brouillon || _lignes.Count == 0)
            throw new InvalidOperationException("Un brouillon non vide est requis.");
        Etat = EtatCommande.Validee;
    }

    public void Annuler()
    {
        if (Etat == EtatCommande.Annulee) throw new InvalidOperationException("Deja annulee.");
        Etat = EtatCommande.Annulee;
    }
}