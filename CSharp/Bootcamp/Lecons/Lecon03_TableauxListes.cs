namespace Bootcamp.Lecons;

/// <summary>
/// LEÇON 03 – Tableaux, listes et dictionnaires
///
/// À retenir :
///  - Tableau (taille fixe) : int[] t = { 1, 2, 3 }; t.Length; t[0]
///  - Liste (taille variable) : var l = new List&lt;int&gt;(); l.Add(4); l.Count
///  - Dictionnaire (clé → valeur) : var d = new Dictionary&lt;string, int&gt;(); d["a"] = 1;
///    d.TryGetValue(cle, out var valeur), d.ContainsKey(cle)
///  - Dans cette leçon, n'utilisez PAS LINQ (Max(), Sum()...) : écrivez les boucles vous-même !
/// </summary>
public static class Lecon03
{
    /// <summary>Renvoie le plus grand élément du tableau (non vide).</summary>
    public static int Maximum(int[] valeurs)
    {
        throw new NotImplementedException(); // TODO : à vous de jouer !
    }

    /// <summary>Renvoie un NOUVEAU tableau avec les éléments dans l'ordre inverse.</summary>
    public static int[] Inverser(int[] valeurs)
    {
        throw new NotImplementedException(); // TODO : à vous de jouer !
    }

    /// <summary>Renvoie la liste des nombres pairs, dans le même ordre.</summary>
    public static List<int> GarderLesPairs(List<int> valeurs)
    {
        throw new NotImplementedException(); // TODO : à vous de jouer !
    }

    /// <summary>Compte le nombre d'apparitions de chaque mot. ["a","b","a"] → { a: 2, b: 1 }</summary>
    public static Dictionary<string, int> CompterMots(string[] mots)
    {
        throw new NotImplementedException(); // TODO : à vous de jouer !
    }

    /// <summary>Renvoie la liste sans doublons, en gardant l'ordre de première apparition.</summary>
    public static List<int> SansDoublons(List<int> valeurs)
    {
        throw new NotImplementedException(); // TODO : à vous de jouer !
    }
}
