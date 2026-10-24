using System.Buffers;
using System.Text;
using Bootcamp.Lecons;
using static Bootcamp.Tests.Verifier;

namespace Bootcamp.Tests;

public static class TestsLecon09
{
    public static IEnumerable<Test> Tests() =>
    [
        new(9, "Copie de struct et mutation par ref", () =>
        {
            var original = new CompteurValeur { Valeur = 10 };
            Egal(11, Lecon09.IncrementerCopie(original).Valeur);
            Egal(10, original.Valeur);
            Lecon09.IncrementerSurPlace(ref original);
            Egal(11, original.Valeur);
        }),
        new(9, "with copie superficielle : alias de liste", () =>
        {
            var original = new ProfilMemoire("Sara", ["Lecture"]);
            var copie = Lecon09.Renommer(original, "Lea");
            Egal("Sara", original.Nom);
            Egal("Lea", copie.Nom);
            Vrai(!ReferenceEquals(original, copie), "Le record doit etre copie.");
            Vrai(ReferenceEquals(original.Roles, copie.Roles), "La liste doit rester partagee.");
            copie.Roles.Add("Ecriture");
            Egal(2, original.Roles.Count);
        }),
        new(9, "Instantane sans alias mutable", () =>
        {
            var original = new ProfilMemoire("Sara", ["Lecture"]);
            var copie = Lecon09.Instantane(original);
            original.Roles.Clear();
            Sequence(["Lecture"], copie.Roles);
            Egal("Sara", copie.Nom);
            Vrai(!ReferenceEquals(original.Roles, copie.Roles), "Listes independantes requises.");
        }),
    ];
}

public static class TestsLecon10
{
    public static IEnumerable<Test> Tests() =>
    [
        new(10, "Unboxing exact avant conversion numerique", () =>
        {
            Egal(42L, Lecon10.DeballerPuisElargir(42));
            Egal(-2L, Lecon10.DeballerPuisElargir(-2));
            Leve<InvalidCastException>(() => Lecon10.DeballerPuisElargir(42L));
            Leve<InvalidCastException>(() => Lecon10.DeballerPuisElargir("42"));
            Leve<InvalidCastException>(() => Lecon10.DeballerPuisElargir(null));
        }),
        new(10, "Nullable : null ou type sous-jacent boxe", () =>
        {
            var (vide, plein) = Lecon10.BoxerNullable(null, 7);
            Egal<object?>(null, vide);
            Egal(typeof(int), plein!.GetType());
            Egal(7, (int)plein);
            var inverse = Lecon10.BoxerNullable(0, null);
            Egal(0, (int)inverse.Vide!);
            Egal<object?>(null, inverse.Plein);
        }),
        new(10, "Egalite generique de value objects", () =>
        {
            Egal(true, Lecon10.MemeValeur(new IdentifiantClient(5), new IdentifiantClient(5)));
            Egal(false, Lecon10.MemeValeur(new IdentifiantClient(5), new IdentifiantClient(6)));
            Egal(true, Lecon10.MemeValeur(42, 42));
        }),
        new(10, "Somme typee : vide et depassement d'int", () =>
        {
            Egal(0L, Lecon10.SommeSansBoxing([]));
            Egal(4294967294L, Lecon10.SommeSansBoxing([int.MaxValue, int.MaxValue]));
            Egal(-1L, Lecon10.SommeSansBoxing([2, -3]));
        }),
    ];
}

public static class TestsLecon11
{
    private sealed class PoolObserve : ArrayPool<byte>
    {
        public byte[] Buffer { get; } = new byte[32];
        public int Locations { get; private set; }
        public int Retours { get; private set; }
        public bool Nettoyage { get; private set; }
        public override byte[] Rent(int minimumLength)
        {
            Locations++;
            Vrai(minimumLength <= Buffer.Length, "Buffer de test trop petit.");
            Array.Fill(Buffer, (byte)99);
            return Buffer;
        }
        public override void Return(byte[] array, bool clearArray = false)
        {
            Vrai(ReferenceEquals(Buffer, array), "Restituer le buffer loue.");
            Retours++;
            Nettoyage = clearArray;
            if (clearArray) Array.Clear(array);
        }
    }

    public static IEnumerable<Test> Tests() =>
    [
        new(11, "Span : slice, vide et format invalide", () =>
        {
            Egal(6, Lecon11.SommeChiffres("xx123yy".AsSpan(2, 3)));
            Egal(0, Lecon11.SommeChiffres(ReadOnlySpan<char>.Empty));
            Leve<FormatException>(() => Lecon11.SommeChiffres("12x"));
        }),
        new(11, "Pool : taille utile et nettoyage", () =>
        {
            var pool = new PoolObserve();
            int appels = 0;
            Lecon11.AvecBuffer(pool, 4, (buffer, taille) =>
            {
                appels++;
                Egal(4, taille);
                Sequence<byte>([42, 42, 42, 42], buffer.Take(taille));
                Egal((byte)99, buffer[taille], "ne pas ecrire au-dela de la taille utile");
            });
            Egal(1, appels); Egal(1, pool.Retours); Egal(true, pool.Nettoyage);
        }),
        new(11, "Pool : restitution meme sur exception", () =>
        {
            var pool = new PoolObserve();
            Leve<InvalidOperationException>(() => Lecon11.AvecBuffer(pool, 4,
                (buffer, taille) => throw new InvalidOperationException("panne")));
            Egal(1, pool.Retours); Egal(true, pool.Nettoyage);
        }),
        new(11, "Pool : taille invalide avant location", () =>
        {
            var pool = new PoolObserve();
            Leve<ArgumentOutOfRangeException>(() => Lecon11.AvecBuffer(pool, 0, (buffer, taille) => { }));
            Leve<ArgumentOutOfRangeException>(() => Lecon11.AvecBuffer(pool, -1, (buffer, taille) => { }));
            Egal(0, pool.Locations);
        }),
        new(11, "Dispose du lecteur mais pas du flux emprunte", () =>
        {
            using var flux = new MemoryStream(Encoding.UTF8.GetBytes("Bonjour C#"));
            Egal("Bonjour C#", Lecon11.LireSansFermer(flux));
            Egal(true, flux.CanRead);
            flux.Position = 0;
            Egal((int)'B', flux.ReadByte());
        }),
    ];
}

public static class TestsLecon12
{
    private static async Task VerifierBorneAsync()
    {
        var deuxDemarres = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var liberer = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        int actifs = 0, demarres = 0, termines = 0;
        Task<int[]> travail = Lecon12.ChargerAsync([3, 1, 2, 4], 2, async (valeur, token) =>
        {
            int courant = Interlocked.Increment(ref actifs);
            try
            {
                Vrai(courant <= 2, "Plus de deux operations actives.");
                if (Interlocked.Increment(ref demarres) == 2) deuxDemarres.TrySetResult();
                await liberer.Task.WaitAsync(token);
                Interlocked.Increment(ref termines);
                return valeur * 10;
            }
            finally { Interlocked.Decrement(ref actifs); }
        });
        try
        {
            await deuxDemarres.Task.WaitAsync(TimeSpan.FromSeconds(5));
            Egal(2, Volatile.Read(ref demarres));
        }
        finally
        {
            liberer.TrySetResult();
            await travail.WaitAsync(TimeSpan.FromSeconds(5));
        }
        Sequence([30, 10, 20, 40], await travail);
        Egal(4, termines);
    }

    public static IEnumerable<Test> Tests() =>
    [
        new(12, "Concurrence bornee sans attente arbitraire", () => VerifierBorneAsync().GetAwaiter().GetResult()),
        new(12, "Ordre conserve avec fin dans le desordre", () =>
        {
            var secondFini = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
            var resultat = Lecon12.ChargerAsync([1, 2], 2, async (valeur, token) =>
            {
                if (valeur == 1) await secondFini.Task.WaitAsync(TimeSpan.FromSeconds(5), token);
                else secondFini.TrySetResult();
                return valeur * 10;
            }).GetAwaiter().GetResult();
            Sequence([10, 20], resultat);
        }),
        new(12, "Batch vide et parallelisme invalide", () =>
        {
            int appels = 0;
            Sequence<int>([], Lecon12.ChargerAsync([], 2, (valeur, token) =>
            { appels++; return Task.FromResult(valeur); }).GetAwaiter().GetResult());
            Egal(0, appels);
            Leve<ArgumentOutOfRangeException>(() => Lecon12.ChargerAsync([1], 0,
                (valeur, token) => Task.FromResult(valeur)).GetAwaiter().GetResult());
        }),
        new(12, "Annulation transmise aux operations en cours", () =>
        {
            using var source = new CancellationTokenSource();
            Leve<OperationCanceledException>(() => Lecon12.ChargerAsync([1], 1, async (valeur, token) =>
            {
                Vrai(token.CanBeCanceled, "Transmettre le token de la boucle.");
                source.Cancel();
                await Task.Delay(Timeout.Infinite, token);
                return valeur;
            }, source.Token).GetAwaiter().GetResult());
        }),
        new(12, "Erreur propagee", () => Leve<InvalidOperationException>(() =>
            Lecon12.ChargerAsync([1], 1, (valeur, token) =>
                Task.FromException<int>(new InvalidOperationException("service indisponible")))
                .GetAwaiter().GetResult())),
        new(12, "Increment atomique", () =>
        {
            Egal(100_000, Lecon12.CompterEnParallele(100_000));
            Egal(0, Lecon12.CompterEnParallele(0));
            Leve<ArgumentOutOfRangeException>(() => Lecon12.CompterEnParallele(-1));
        }),
    ];
}