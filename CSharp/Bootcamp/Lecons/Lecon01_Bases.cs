namespace Bootcamp.Lecons;

/// <summary>
/// LEÇON 01 – Variables, types et opérateurs
///
/// À retenir :
///  - Types de base : int (entier), double (décimal), bool (vrai/faux), char, string.
///  - Opérateurs : + - * / et % (reste de la division entière).
///  - Attention : 7 / 2 vaut 3 avec des int ! Utilisez 7 / 2.0 pour obtenir 3.5.
///  - Conversion : (double)x, int.Parse("42"), x.ToString().
///
/// Consigne : remplacez chaque « throw new NotImplementedException(); » par votre code,
/// puis lancez « dotnet run -- 1 » pour vérifier.
/// </summary>
public static class Lecon01
{
    /// <summary>Renvoie la somme de a et b.</summary>
    public static int Somme(int a, int b)
    {
        throw new NotImplementedException(); // TODO : à vous de jouer !
    }

    /// <summary>Renvoie la moyenne de a et b (résultat décimal : Moyenne(3, 4) = 3.5).</summary>
    public static double Moyenne(int a, int b)
    {
        throw new NotImplementedException(); // TODO : à vous de jouer !
    }

    /// <summary>Renvoie true si n est pair (astuce : opérateur %).</summary>
    public static bool EstPair(int n)
    {
        throw new NotImplementedException(); // TODO : à vous de jouer !
    }

    /// <summary>Convertit une température en degrés Celsius vers Fahrenheit : F = C × 9 / 5 + 32.</summary>
    public static double CelsiusVersFahrenheit(double celsius)
    {
        throw new NotImplementedException(); // TODO : à vous de jouer !
    }

    /// <summary>Convertit un texte en entier puis le double ("21" → 42).</summary>
    public static int DoublerTexte(string nombre)
    {
        throw new NotImplementedException(); // TODO : à vous de jouer !
    }
}
