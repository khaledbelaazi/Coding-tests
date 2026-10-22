namespace Bootcamp.Tests;

/// <summary>Un test : la leçon, son nom et le code à exécuter.</summary>
public record Test(int Lecon, string Nom, Action Code);

/// <summary>Échec d'une vérification.</summary>
public class EchecException(string message) : Exception(message);

/// <summary>Petites fonctions de vérification (équivalent simplifié de xUnit / NUnit).</summary>
public static class Verifier
{
    public static void Egal<T>(T attendu, T obtenu, string? contexte = null)
    {
        if (!EqualityComparer<T>.Default.Equals(attendu, obtenu))
            throw new EchecException($"attendu « {Afficher(attendu)} » mais obtenu « {Afficher(obtenu)} »{Ctx(contexte)}");
    }

    public static void Sequence<T>(IEnumerable<T> attendu, IEnumerable<T>? obtenu, string? contexte = null)
    {
        if (obtenu is null || !attendu.SequenceEqual(obtenu))
            throw new EchecException($"attendu [{string.Join(", ", attendu)}] mais obtenu [{(obtenu is null ? "null" : string.Join(", ", obtenu))}]{Ctx(contexte)}");
    }

    public static void Proche(double attendu, double obtenu, double tolerance = 1e-9, string? contexte = null)
    {
        if (Math.Abs(attendu - obtenu) > tolerance)
            throw new EchecException($"attendu ≈ {attendu} mais obtenu {obtenu}{Ctx(contexte)}");
    }

    public static void Vrai(bool condition, string message)
    {
        if (!condition) throw new EchecException(message);
    }

    public static void Leve<TException>(Action code, string? contexte = null) where TException : Exception
    {
        try
        {
            code();
        }
        catch (TException)
        {
            return;
        }
        catch (NotImplementedException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new EchecException($"attendu une exception {typeof(TException).Name} mais obtenu {ex.GetType().Name}{Ctx(contexte)}");
        }
        throw new EchecException($"attendu une exception {typeof(TException).Name}, aucune levée{Ctx(contexte)}");
    }

    private static string Afficher<T>(T valeur) => valeur is null ? "null" : valeur.ToString() ?? "";
    private static string Ctx(string? c) => c is null ? "" : $" ({c})";
}

/// <summary>Exécute les tests et affiche la progression.</summary>
public static class Testeur
{
    public static bool Executer(IEnumerable<Test> tests, int? seulementLecon)
    {
        int reussis = 0, aFaire = 0, echecs = 0;
        int? leconCourante = null;

        foreach (var test in tests.Where(t => seulementLecon is null || t.Lecon == seulementLecon))
        {
            if (test.Lecon != leconCourante)
            {
                leconCourante = test.Lecon;
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"── Leçon {test.Lecon:00} ──────────────────────────────");
                Console.ResetColor();
            }

            try
            {
                test.Code();
                Ecrire(ConsoleColor.Green, "  ✔ ", test.Nom);
                reussis++;
            }
            catch (NotImplementedException)
            {
                Ecrire(ConsoleColor.DarkGray, "  … ", test.Nom + " (à faire)");
                aFaire++;
            }
            catch (EchecException ex)
            {
                Ecrire(ConsoleColor.Red, "  ✘ ", $"{test.Nom} : {ex.Message}");
                echecs++;
            }
            catch (Exception ex)
            {
                Ecrire(ConsoleColor.Red, "  ✘ ", $"{test.Nom} : exception inattendue {ex.GetType().Name} – {ex.Message}");
                echecs++;
            }
        }

        int total = reussis + aFaire + echecs;
        Console.WriteLine();
        Console.WriteLine($"Résultat : {reussis}/{total} réussis, {echecs} en échec, {aFaire} à faire.");
        if (total > 0 && reussis == total)
            Ecrire(ConsoleColor.Green, "", "Bravo, tout est vert !");
        return echecs == 0 && aFaire == 0;
    }

    private static void Ecrire(ConsoleColor couleur, string prefixe, string texte)
    {
        Console.ForegroundColor = couleur;
        Console.Write(prefixe);
        Console.ResetColor();
        Console.WriteLine(texte);
    }
}
