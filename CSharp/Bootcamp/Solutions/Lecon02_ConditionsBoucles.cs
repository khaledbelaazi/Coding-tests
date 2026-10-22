namespace Bootcamp.Lecons;

/// <summary>
/// LEÇON 02 – Conditions et boucles
///
/// À retenir :
///  - if / else if / else, et l'opérateur ternaire : condition ? siVrai : siFaux
///  - switch (expression) : n switch { 1 => "un", _ => "autre" }
///  - Boucles : for (int i = 0; i &lt; n; i++), while (condition), foreach (var x in liste)
/// </summary>
public static class Lecon02
{
    /// <summary>Renvoie "négatif", "nul" ou "positif".</summary>
    public static string Signe(int n)
    {
        if (n < 0) return "négatif";
        if (n == 0) return "nul";
        return "positif";
    }

    /// <summary>Renvoie la mention d'une note sur 20 :
    /// &lt; 10 "Insuffisant", &lt; 12 "Passable", &lt; 14 "Assez bien", &lt; 16 "Bien", sinon "Très bien".</summary>
    public static string Mention(double note)
    {
        return note switch
        {
            < 10 => "Insuffisant",
            < 12 => "Passable",
            < 14 => "Assez bien",
            < 16 => "Bien",
            _ => "Très bien"
        };
    }

    /// <summary>Renvoie la somme des entiers de 1 à n (0 si n ≤ 0).</summary>
    public static int SommeJusqua(int n)
    {
        int somme = 0;
        for (int i = 1; i <= n; i++)
            somme += i;
        return somme;
    }

    /// <summary>Renvoie n! (factorielle). Factorielle(0) = 1, Factorielle(5) = 120.</summary>
    public static long Factorielle(int n)
    {
        long resultat = 1;
        for (int i = 2; i <= n; i++)
            resultat *= i;
        return resultat;
    }

    /// <summary>FizzBuzz : "Fizz" si n divisible par 3, "Buzz" par 5, "FizzBuzz" par les deux, sinon le nombre.</summary>
    public static string FizzBuzz(int n)
    {
        if (n % 15 == 0) return "FizzBuzz";
        if (n % 3 == 0) return "Fizz";
        if (n % 5 == 0) return "Buzz";
        return n.ToString();
    }

    /// <summary>Renvoie true si n est un nombre premier (divisible seulement par 1 et lui-même, n ≥ 2).</summary>
    public static bool EstPremier(int n)
    {
        if (n < 2) return false;
        for (int d = 2; d * d <= n; d++)
            if (n % d == 0) return false;
        return true;
    }
}
