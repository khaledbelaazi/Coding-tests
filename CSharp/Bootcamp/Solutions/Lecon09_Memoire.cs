namespace Bootcamp.Lecons;

public sealed record ProfilMemoire(string Nom, List<string> Roles);
public struct CompteurValeur
{
    public int Valeur { get; set; }
}

public static class Lecon09
{
    public static CompteurValeur IncrementerCopie(CompteurValeur compteur)
    {
        compteur.Valeur++;
        return compteur;
    }

    public static void IncrementerSurPlace(ref CompteurValeur compteur) => compteur.Valeur++;

    public static ProfilMemoire Renommer(ProfilMemoire profil, string nom) => profil with { Nom = nom };

    public static ProfilMemoire Instantane(ProfilMemoire profil) => profil with { Roles = new(profil.Roles) };
}