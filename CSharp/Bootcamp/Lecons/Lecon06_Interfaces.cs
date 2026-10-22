namespace Bootcamp.Lecons;

/// <summary>
/// LEÇON 06 – Interfaces et polymorphisme : des formes géométriques
///
/// À retenir :
///  - Une interface est un contrat : toute classe qui l'implémente DOIT fournir ses membres.
///  - Polymorphisme : on manipule des IForme sans savoir si ce sont des cercles ou des rectangles,
///    et chaque objet exécute SA propre version de Aire().
///  - Math.PI, Math.Sqrt(x), Math.Pow(x, 2)
/// </summary>
public interface IForme
{
    string Nom { get; }
    double Aire();
    double Perimetre();
}

public class Cercle(double rayon) : IForme
{
    public double Rayon { get; } = rayon;
    public string Nom => "Cercle";

    public double Aire()
    {
        throw new NotImplementedException(); // TODO : à vous de jouer !
    }

    public double Perimetre()
    {
        throw new NotImplementedException(); // TODO : à vous de jouer !
    }
}

public class Rectangle(double largeur, double hauteur) : IForme
{
    public double Largeur { get; } = largeur;
    public double Hauteur { get; } = hauteur;
    public virtual string Nom => "Rectangle";

    public double Aire()
    {
        throw new NotImplementedException(); // TODO : à vous de jouer !
    }

    public double Perimetre()
    {
        throw new NotImplementedException(); // TODO : à vous de jouer !
    }
}

/// <summary>Un carré EST un rectangle (héritage) dont les côtés sont égaux.</summary>
public class Carre(double cote) : Rectangle(cote, cote)
{
    public override string Nom => "Carré";
}

/// <summary>Triangle défini par ses 3 côtés. Aire : formule de Héron, avec s = périmètre / 2 :
/// aire = √(s × (s − a) × (s − b) × (s − c)).</summary>
public class Triangle(double a, double b, double c) : IForme
{
    public double A { get; } = a;
    public double B { get; } = b;
    public double C { get; } = c;
    public string Nom => "Triangle";

    public double Aire()
    {
        throw new NotImplementedException(); // TODO : à vous de jouer !
    }

    public double Perimetre()
    {
        throw new NotImplementedException(); // TODO : à vous de jouer !
    }
}

public static class Lecon06
{
    /// <summary>Renvoie la somme des aires de toutes les formes.</summary>
    public static double AireTotale(IEnumerable<IForme> formes)
    {
        throw new NotImplementedException(); // TODO : à vous de jouer !
    }

    /// <summary>Renvoie le nom de la forme ayant la plus grande aire (liste non vide).</summary>
    public static string PlusGrandeForme(IEnumerable<IForme> formes)
    {
        throw new NotImplementedException(); // TODO : à vous de jouer !
    }
}
