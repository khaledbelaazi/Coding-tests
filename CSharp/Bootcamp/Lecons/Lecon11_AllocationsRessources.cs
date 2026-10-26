using System.Buffers;

namespace Bootcamp.Lecons;

/// <summary>
/// 11 - Span, location de buffers et propriete des ressources.
/// ReadOnlySpan est une vue : Slice n'est pas une copie. Ne pas capturer cette vue dans une lambda.
/// Rent garantit une taille AU MOINS demandee : ne pas traiter les octets supplementaires.
/// Return doit s'executer dans finally, meme si le traitement echoue.
/// Dispose libere une ressource deterministe ; le GC ne remplace pas using.
/// </summary>
public static class Lecon11
{
    /// <summary>Somme des chiffres ASCII d'une vue, sans ToString/Substring. Vide donne 0 ; autre caractere : FormatException.</summary>
    public static int SommeChiffres(ReadOnlySpan<char> texte)
        => throw new NotImplementedException();

    /// <summary>
    /// Louer taille octets (strictement positive), copier seulement taille octets de valeur 42,
    /// appeler traiter une fois avec le buffer et taille, puis Return avec clearArray:true.
    /// Le callback ne doit pas conserver le tableau au-dela de cet appel.
    /// </summary>
    public static void AvecBuffer(ArrayPool<byte> pool, int taille, Action<byte[], int> traiter)
        => throw new NotImplementedException();

    /// <summary>Lire tout en UTF-8 avec un StreamReader dispose, sans fermer le stream appartenant a l'appelant.</summary>
    public static string LireSansFermer(Stream flux)
        => throw new NotImplementedException();
}