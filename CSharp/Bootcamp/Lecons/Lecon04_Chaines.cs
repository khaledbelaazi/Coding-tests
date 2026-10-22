using System.Text;

namespace Bootcamp.Lecons;

/// <summary>
/// LEÇON 04 – Chaînes de caractères
///
/// À retenir :
///  - Interpolation : $"Bonjour {prenom} !"
///  - Méthodes utiles : ToUpper(), ToLower(), Trim(), Split(' '), Replace(a, b), Contains(x), Substring(debut, longueur)
///  - Une string est immuable : chaque modification crée une nouvelle chaîne.
///    Pour beaucoup de concaténations, utilisez StringBuilder.
///  - char.IsLetter(c), char.IsDigit(c), char.ToLower(c)
/// </summary>
public static class Lecon04
{
    /// <summary>Renvoie "Bonjour Prénom !" (ex. Saluer("Khaled") → "Bonjour Khaled !").</summary>
    public static string Saluer(string prenom)
    {
        throw new NotImplementedException(); // TODO : à vous de jouer !
    }

    /// <summary>Compte les voyelles (a, e, i, o, u, y), majuscules comprises.</summary>
    public static int CompterVoyelles(string texte)
    {
        throw new NotImplementedException(); // TODO : à vous de jouer !
    }

    /// <summary>Renvoie true si le mot se lit pareil dans les deux sens, sans tenir compte de la casse ("Kayak").</summary>
    public static bool EstPalindrome(string mot)
    {
        throw new NotImplementedException(); // TODO : à vous de jouer !
    }

    /// <summary>Met la première lettre de chaque mot en majuscule : "bonjour le monde" → "Bonjour Le Monde".</summary>
    public static string MajusculeChaqueMot(string phrase)
    {
        throw new NotImplementedException(); // TODO : à vous de jouer !
    }

    /// <summary>Compression simple : "aaabcc" → "a3b1c2" (lettre suivie du nombre de répétitions). "" → "".</summary>
    public static string Compresser(string texte)
    {
        throw new NotImplementedException(); // TODO : à vous de jouer !
    }
}
