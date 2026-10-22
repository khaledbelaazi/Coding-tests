namespace Bootcamp.Lecons;

/// <summary>
/// LEÇON 08 – Exceptions et gestion des erreurs
///
/// À retenir :
///  - throw new ArgumentException("message") : signaler une entrée invalide
///  - try { ... } catch (FormatException ex) { ... } finally { ... toujours exécuté ... }
///  - Préférez int.TryParse(texte, out int n) quand l'erreur est « normale » (saisie utilisateur).
///  - Créez vos propres exceptions en héritant de Exception.
/// </summary>
public class AgeInvalideException(int age) : Exception($"Âge invalide : {age}")
{
    public int Age { get; } = age;
}

public static class Lecon08
{
    /// <summary>Convertit le texte en âge. Texte non numérique → FormatException (laissez int.Parse la lever).
    /// Âge &lt; 0 ou &gt; 150 → AgeInvalideException.</summary>
    public static int LireAge(string texte)
    {
        throw new NotImplementedException(); // TODO : à vous de jouer !
    }

    /// <summary>Comme LireAge, mais ne lève jamais d'exception : renvoie null si le texte est invalide
    /// (non numérique OU âge hors limites). Utilisez try/catch ou TryParse.</summary>
    public static int? LireAgeOuNull(string texte)
    {
        throw new NotImplementedException(); // TODO : à vous de jouer !
    }

    /// <summary>Division sûre : renvoie false (et resultat = 0) si diviseur vaut 0, sinon true et le quotient.</summary>
    public static bool TryDiviser(double dividende, double diviseur, out double resultat)
    {
        throw new NotImplementedException(); // TODO : à vous de jouer !
    }

    /// <summary>Exécute l'action et renvoie le journal des étapes :
    /// "début", puis "succès" OU "erreur : {message}", puis TOUJOURS "fin" (bloc finally).</summary>
    public static List<string> ExecuterAvecJournal(Action action)
    {
        throw new NotImplementedException(); // TODO : à vous de jouer !
    }
}
