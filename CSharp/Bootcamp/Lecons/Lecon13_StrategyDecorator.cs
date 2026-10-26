namespace Bootcamp.Lecons;

public interface IPolitiqueRemise
{
    decimal Appliquer(decimal brut);
}

/// <summary>Strategy : echanger une regle sans modifier son consommateur. Brut negatif interdit.</summary>
public sealed class SansRemise : IPolitiqueRemise
{
    public decimal Appliquer(decimal brut) => throw new NotImplementedException();
}

/// <summary>
/// Taux dans [0,1], verifie lors d'Appliquer ; sinon ArgumentOutOfRangeException.
/// Arrondir le montant final a deux decimales, MidpointRounding.AwayFromZero.
/// Convention pedagogique en euros, pas une regle universelle pour toutes les devises.
/// </summary>
public sealed class RemiseFidelite : IPolitiqueRemise
{
    public RemiseFidelite(decimal taux) => throw new NotImplementedException();
    public decimal Appliquer(decimal brut) => throw new NotImplementedException();
}

/// <summary>
/// Decorator : meme contrat, comportement ajoute par composition.
/// Deleguer UNE fois, puis journaliser seulement en cas de succes : total=90.00 (culture invariante).
/// Ne pas avaler les erreurs de la politique ou du journal.
/// </summary>
public sealed class RemiseJournalisee : IPolitiqueRemise
{
    public RemiseJournalisee(IPolitiqueRemise interne, Action<string> journal) => throw new NotImplementedException();
    public decimal Appliquer(decimal brut) => throw new NotImplementedException();
}

/// <summary>Injection par constructeur, sans new RemiseFidelite dans le service ni service locator.</summary>
public sealed class ServiceTarification
{
    public ServiceTarification(IPolitiqueRemise remise) => throw new NotImplementedException();
    /// <summary>Refuser tout prix negatif, sommer puis appliquer la politique une seule fois. Vide donne un brut de 0.</summary>
    public decimal Total(IEnumerable<decimal> prix) => throw new NotImplementedException();
}