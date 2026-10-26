namespace Bootcamp.Lecons;

public sealed record ProfilMemoire(string Nom, List<string> Roles);
public struct CompteurValeur
{
    public int Valeur { get; set; }
}

/// <summary>
/// 09 - Valeur, reference, alias et copies.
/// Une affectation de struct copie ses champs, pas les objets references par ses champs.
/// Un record class reste un objet reference ; with effectue une copie superficielle.
/// Stack/heap ne se deduisent pas du seul mot struct/class : voir le guide avance.
/// </summary>
public static class Lecon09
{
    /// <summary>Copier le struct, incrementer la copie et la retourner sans modifier l'original.</summary>
    public static CompteurValeur IncrementerCopie(CompteurValeur compteur)
        => throw new NotImplementedException();

    /// <summary>Modifier directement la variable appelante via ref, avec un increment.</summary>
    public static void IncrementerSurPlace(ref CompteurValeur compteur)
        => throw new NotImplementedException();

    /// <summary>Changer le nom via with, en partageant volontairement la liste Roles.</summary>
    public static ProfilMemoire Renommer(ProfilMemoire profil, string nom)
        => throw new NotImplementedException();

    /// <summary>Copier aussi la liste pour obtenir un instantane independant. Les string sont immuables.</summary>
    public static ProfilMemoire Instantane(ProfilMemoire profil)
        => throw new NotImplementedException();
}