namespace Bootcamp.Lecons;

public enum EtatCommande { Brouillon, Validee, Annulee }
public sealed record LigneCommande(string Reference, int Quantite, decimal PrixUnitaire);

/// <summary>
/// Agregat : garder les invariants dans l'objet qui possede les donnees.
/// Pas de setters publics sur Etat ni de List mutable exposee.
/// Transitions : Brouillon -> Validee ou Annulee ; Validee -> Annulee ; Annulee est terminal.
/// Ce modele ne gere ni paiement, ni stock, ni persistance. Il n'est pas thread-safe.
/// </summary>
public sealed class CommandeAchat
{
    private readonly List<LigneCommande> _lignes = [];
    public EtatCommande Etat { get; private set; } = EtatCommande.Brouillon;

    /// <summary>Renvoyer un instantane : changer le tableau retourne ne doit pas modifier l'agregat.</summary>
    public IReadOnlyList<LigneCommande> Lignes => throw new NotImplementedException();
    public decimal Total => throw new NotImplementedException();

    /// <summary>
    /// Uniquement en brouillon, sinon InvalidOperationException.
    /// Reference vide/blanche : ArgumentException ; quantite &lt;= 0 ou prix &lt; 0 : ArgumentOutOfRangeException.
    /// Une entree invalide ne doit jamais ajouter de ligne. Prix zero accepte ; conserver les lignes separees.
    /// </summary>
    public void Ajouter(string reference, int quantite, decimal prixUnitaire)
        => throw new NotImplementedException();

    /// <summary>Seulement un brouillon non vide ; sinon InvalidOperationException sans changer l'etat.</summary>
    public void Valider() => throw new NotImplementedException();

    /// <summary>Annuler un brouillon ou une commande validee ; double annulation : InvalidOperationException.</summary>
    public void Annuler() => throw new NotImplementedException();
}