namespace Bootcamp.Lecons;

public static class Lecon12
{
    public static async Task<int[]> ChargerAsync(int[] entrees, int parallelisme,
        Func<int, CancellationToken, Task<int>> charger, CancellationToken token = default)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(parallelisme);
        var resultats = new int[entrees.Length];
        var options = new ParallelOptions { MaxDegreeOfParallelism = parallelisme, CancellationToken = token };
        await Parallel.ForEachAsync(Enumerable.Range(0, entrees.Length), options, async (index, annulation) =>
        {
            resultats[index] = await charger(entrees[index], annulation);
        });
        return resultats;
    }

    public static int CompterEnParallele(int iterations)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(iterations);
        int compteur = 0;
        Parallel.For(0, iterations, index => Interlocked.Increment(ref compteur));
        return compteur;
    }
}