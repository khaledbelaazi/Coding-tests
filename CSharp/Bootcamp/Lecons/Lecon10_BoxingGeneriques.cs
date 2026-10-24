namespace Bootcamp.Lecons;

public readonly record struct IdentifiantClient(int Valeur);

/// <summary>
/// 10 - Boxing, unboxing, nullable et dispatch generique.
/// object boite = 42 alloue une boite contenant une COPIE de l'int.
/// (long)boite echoue : il faut d'abord (int)boite puis convertir vers long.
/// int? sans valeur devient null lors du boxing ; avec valeur, il boite un int.
/// Une contrainte generique permet un appel d'interface sans conversion explicite vers object.
/// </summary>
public static class Lecon10
{
    /// <summary>Accepter uniquement un int boxe (y compris negatif). Sinon InvalidCastException, meme null.</summary>
    public static long DeballerPuisElargir(object? valeur)
        => throw new NotImplementedException();

    /// <summary>Retourner les deux nullable convertis en object : null ou int boxe, jamais Nullable boxe.</summary>
    public static (object? Vide, object? Plein) BoxerNullable(int? vide, int? plein)
        => throw new NotImplementedException();

    /// <summary>Comparer via IEquatable&lt;T&gt;, sans cast object/interface dans le corps.</summary>
    public static bool MemeValeur<T>(T gauche, T droite) where T : struct, IEquatable<T>
        => throw new NotImplementedException();

    /// <summary>Sommer les int sans passer par une collection non generique. Resultat en long.</summary>
    public static long SommeSansBoxing(IEnumerable<int> valeurs)
        => throw new NotImplementedException();
}