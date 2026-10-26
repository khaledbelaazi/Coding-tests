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
        return $"Bonjour {prenom} !";
    }

    /// <summary>Compte les voyelles (a, e, i, o, u, y), majuscules comprises.</summary>
    public static int CompterVoyelles(string texte)
    {
        int n = 0;
        foreach (char c in texte.ToLower())
            if ("aeiouy".Contains(c)) n++;
        return n;
    }

    /// <summary>Renvoie true si le mot se lit pareil dans les deux sens, sans tenir compte de la casse ("Kayak").</summary>
    public static bool EstPalindrome(string mot)
    {
        string m = mot.ToLower();
        for (int i = 0, j = m.Length - 1; i < j; i++, j--)
            if (m[i] != m[j]) return false;
        return true;
    }

    /// <summary>Met la première lettre de chaque mot en majuscule : "bonjour le monde" → "Bonjour Le Monde".</summary>
    public static string MajusculeChaqueMot(string phrase)
    {
        string[] mots = phrase.Split(' ');
        for (int i = 0; i < mots.Length; i++)
            if (mots[i].Length > 0)
                mots[i] = char.ToUpper(mots[i][0]) + mots[i].Substring(1);
        return string.Join(' ', mots);
    }

    /// <summary>Compression simple : "aaabcc" → "a3b1c2" (lettre suivie du nombre de répétitions). "" → "".</summary>
    public static string Compresser(string texte)
    {
        var sb = new StringBuilder();
        int i = 0;
        while (i < texte.Length)
        {
            char c = texte[i];
            int n = 0;
            while (i < texte.Length && texte[i] == c) { n++; i++; }
            sb.Append(c).Append(n);
        }
        return sb.ToString();
    }
}
