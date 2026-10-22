namespace Bootcamp.Lecons;

/// <summary>
/// LEÇON 07 – LINQ : interroger des collections
///
/// À retenir (chaque méthode renvoie une nouvelle séquence, la source n'est pas modifiée) :
///  - Where(e => condition)        : filtrer
///  - Select(e => valeur)          : transformer
///  - OrderBy / OrderByDescending  : trier ; ThenBy pour un 2e critère
///  - Sum, Average, Count, Max, Min, Any, All, First, FirstOrDefault
///  - GroupBy(e => cle)            : regrouper
///  - ToList(), ToDictionary(...)  : matérialiser le résultat
/// Objectif : chaque exercice tient en UNE instruction LINQ.
/// </summary>
public record Employe(string Nom, string Service, decimal Salaire, int Anciennete);

public static class Lecon07
{
    /// <summary>Noms des employés du service donné, triés par ordre alphabétique.</summary>
    public static List<string> NomsDuService(IEnumerable<Employe> employes, string service)
    {
        throw new NotImplementedException(); // TODO : à vous de jouer !
    }

    /// <summary>Masse salariale (somme des salaires) de tous les employés.</summary>
    public static decimal MasseSalariale(IEnumerable<Employe> employes)
    {
        throw new NotImplementedException(); // TODO : à vous de jouer !
    }

    /// <summary>Salaire moyen par service : { "IT": 4200, "RH": 3100, ... }.</summary>
    public static Dictionary<string, decimal> SalaireMoyenParService(IEnumerable<Employe> employes)
    {
        throw new NotImplementedException(); // TODO : à vous de jouer !
    }

    /// <summary>Les n employés les mieux payés (noms), du plus payé au moins payé.</summary>
    public static List<string> MieuxPayes(IEnumerable<Employe> employes, int n)
    {
        throw new NotImplementedException(); // TODO : à vous de jouer !
    }

    /// <summary>true si au moins un employé a 10 ans d'ancienneté ou plus.</summary>
    public static bool AuMoinsUnVeteran(IEnumerable<Employe> employes)
    {
        throw new NotImplementedException(); // TODO : à vous de jouer !
    }
}
