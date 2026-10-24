using System.Buffers;
using System.Text;

namespace Bootcamp.Lecons;

public static class Lecon11
{
    public static int SommeChiffres(ReadOnlySpan<char> texte)
    {
        int total = 0;
        foreach (char caractere in texte)
        {
            if (caractere is < '0' or > '9') throw new FormatException("Chiffre ASCII attendu.");
            total += caractere - '0';
        }
        return total;
    }

    public static void AvecBuffer(ArrayPool<byte> pool, int taille, Action<byte[], int> traiter)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(taille);
        byte[] buffer = pool.Rent(taille);
        try
        {
            buffer.AsSpan(0, taille).Fill(42);
            traiter(buffer, taille);
        }
        finally
        {
            pool.Return(buffer, clearArray: true);
        }
    }

    public static string LireSansFermer(Stream flux)
    {
        using var lecteur = new StreamReader(flux, Encoding.UTF8, leaveOpen: true);
        return lecteur.ReadToEnd();
    }
}