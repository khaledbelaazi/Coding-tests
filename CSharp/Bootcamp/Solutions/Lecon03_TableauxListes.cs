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
        int max = valeurs[0];
        foreach (int v in valeurs)
            if (v > max) max = v;
        return max;
    }

    /// <summary>Renvoie un NOUVEAU tableau avec les éléments dans l'ordre inverse.</summary>
    public static int[] Inverser(int[] valeurs)
    {
        int[] resultat = new int[valeurs.Length];
        for (int i = 0; i < valeurs.Length; i++)
            resultat[i] = valeurs[valeurs.Length - 1 - i];
        return resultat;
    }

    /// <summary>Renvoie la liste des nombres pairs, dans le même ordre.</summary>
    public static List<int> GarderLesPairs(List<int> valeurs)
    {
        var pairs = new List<int>();
        foreach (int v in valeurs)
            if (v % 2 == 0) pairs.Add(v);
        return pairs;
    }

    /// <summary>Compte le nombre d'apparitions de chaque mot. ["a","b","a"] → { a: 2, b: 1 }</summary>
    public static Dictionary<string, int> CompterMots(string[] mots)
    {
        var compte = new Dictionary<string, int>();
        foreach (string mot in mots)
        {
            compte.TryGetValue(mot, out int n);
            compte[mot] = n + 1;
        }
        return compte;
    }

    /// <summary>Renvoie la liste sans doublons, en gardant l'ordre de première apparition.</summary>
    public static List<int> SansDoublons(List<int> valeurs)
    {
        var vus = new HashSet<int>();
        var resultat = new List<int>();
        foreach (int v in valeurs)
            if (vus.Add(v)) resultat.Add(v);
        return resultat;
    }
}
