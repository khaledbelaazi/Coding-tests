namespace Bootcamp.Lecons;

public readonly record struct IdentifiantClient(int Valeur);

public static class Lecon10
{
    public static long DeballerPuisElargir(object? valeur) => valeur is int entier
        ? (long)entier
        : throw new InvalidCastException("Un int boxe est requis.");

    public static (object? Vide, object? Plein) BoxerNullable(int? vide, int? plein) => (vide, plein);

    public static bool MemeValeur<T>(T gauche, T droite) where T : struct, IEquatable<T>
        => gauche.Equals(droite);

    public static long SommeSansBoxing(IEnumerable<int> valeurs)
    {
        long total = 0;
        foreach (int valeur in valeurs) total += valeur;
        return total;
    }
}