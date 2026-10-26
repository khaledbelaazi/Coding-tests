namespace Bootcamp.Lecons;

public interface IPolitiqueRemise
{
    decimal Appliquer(decimal brut);
}

public sealed class SansRemise : IPolitiqueRemise
{
    public decimal Appliquer(decimal brut)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(brut);
        return brut;
    }
}

public sealed class RemiseFidelite(decimal taux) : IPolitiqueRemise
{
    public decimal Appliquer(decimal brut)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(brut);
        if (taux is < 0 or > 1) throw new ArgumentOutOfRangeException(nameof(taux));
        return decimal.Round(brut * (1 - taux), 2, MidpointRounding.AwayFromZero);
    }
}

public sealed class RemiseJournalisee(IPolitiqueRemise interne, Action<string> journal) : IPolitiqueRemise
{
    public decimal Appliquer(decimal brut)
    {
        decimal net = interne.Appliquer(brut);
        journal(FormattableString.Invariant($"total={net:0.00}"));
        return net;
    }
}

public sealed class ServiceTarification(IPolitiqueRemise remise)
{
    public decimal Total(IEnumerable<decimal> prix)
    {
        decimal brut = 0;
        foreach (decimal montant in prix)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(montant);
            brut += montant;
        }
        return remise.Appliquer(brut);
    }
}