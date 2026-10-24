namespace Bootcamp.Lecons;

/// <summary>
/// 12 - Async ne signifie pas automatiquement thread supplementaire.
/// await libere l'attente d'I/O ; Task.Run sert surtout a deplacer du travail CPU.
/// Utiliser Parallel.ForEachAsync avec MaxDegreeOfParallelism, pas un Task.WhenAll sans borne.
/// Transmettre le token fourni par la boucle a chaque operation et ne pas avaler l'annulation.
/// Ne pas utiliser Result/Wait dans le code applicatif ; Interlocked protege un increment partage.
/// </summary>
public static class Lecon12
{
    /// <summary>
    /// Appliquer charger a chaque entree avec au plus parallelisme operations actives.
    /// Preserver l'ordre d'entree dans le tableau retourne, meme si les appels terminent dans le desordre.
    /// parallelisme &lt;= 0 : ArgumentOutOfRangeException. Propager erreurs et annulation.
    /// </summary>
    public static Task<int[]> ChargerAsync(int[] entrees, int parallelisme,
        Func<int, CancellationToken, Task<int>> charger, CancellationToken token = default)
        => throw new NotImplementedException();

    /// <summary>
    /// Parallel.For sur [0, iterations), avec Interlocked.Increment sur un compteur local partage.
    /// Refuser les iterations negatives. Ne pas remplacer la boucle par return iterations.
    /// </summary>
    public static int CompterEnParallele(int iterations)
        => throw new NotImplementedException();
}