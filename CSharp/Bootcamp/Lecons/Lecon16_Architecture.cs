namespace Bootcamp.Lecons;

public sealed record DemandeDevis(string Reference, int Quantite);
public abstract record ResultatDevis
{
    public sealed record Accepte(decimal Brut, decimal Net) : ResultatDevis;
    public sealed record Refuse(string Motif) : ResultatDevis;
}

/// <summary>
/// Cas d'usage applicatif, sans reference a une base, HTTP ou une console.
/// IPrixArticle est son port de lecture ; IPolitiqueRemise porte une regle interchangeable.
/// Il orchestre CommandeAchat (domaine), sans recopier ses calculs de total.
/// Les implementations sont assemblees a l'exterieur : composition root.
/// </summary>
public sealed class CalculateurDevis
{
    public CalculateurDevis(IPrixArticle catalogue, IPolitiqueRemise remise) => throw new NotImplementedException();
    /// <summary>
    /// Vide -> Refuse("commande-vide"). Valider TOUTES les entrees avant de consulter le catalogue :
    /// reference blanche ou quantite &lt;= 0 -> Refuse("ligne-invalide").
    /// Prix null -> Refuse("article-inconnu:" + reference).
    /// Prix negatif -> InvalidOperationException (contrat technique du catalogue viole).
    /// Sinon construire/valider une CommandeAchat et retourner Accepte(brut, remise.Appliquer(brut)).
    /// Propager une panne du catalogue : ce n'est pas un refus metier ni un prix zero.
    /// Aucun enregistrement, reservation de stock ou paiement dans ce cas d'usage de lecture.
    /// </summary>
    public ResultatDevis Calculer(IReadOnlyList<DemandeDevis> lignes)
        => throw new NotImplementedException();
}

/// <summary>Adaptateur en memoire, remplacable par un adaptateur SQL/HTTP sans changer le cas d'usage.</summary>
public sealed class CatalogueMemoire : IPrixArticle
{
    public CatalogueMemoire(IReadOnlyDictionary<string, decimal> prix) => throw new NotImplementedException();
    public decimal? PrixUnitaire(string reference) => throw new NotImplementedException();
}

public static class CompositionDevis
{
    public static CalculateurDevis Creer() => new(
        new CatalogueMemoire(new Dictionary<string, decimal> { ["CLAVIER"] = 80m, ["SOURIS"] = 20m }),
        new RemiseFidelite(0.10m));
}