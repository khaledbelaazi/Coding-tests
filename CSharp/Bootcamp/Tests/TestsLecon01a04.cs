using Bootcamp.Lecons;
using static Bootcamp.Tests.Verifier;

namespace Bootcamp.Tests;

public static class TestsLecon01
{
    public static IEnumerable<Test> Tests() =>
    [
        new(1, "Somme", () => { Egal(5, Lecon01.Somme(2, 3)); Egal(-1, Lecon01.Somme(2, -3)); }),
        new(1, "Moyenne", () => { Proche(3.5, Lecon01.Moyenne(3, 4)); Proche(10, Lecon01.Moyenne(10, 10)); }),
        new(1, "EstPair", () => { Egal(true, Lecon01.EstPair(4)); Egal(false, Lecon01.EstPair(7)); Egal(true, Lecon01.EstPair(0)); }),
        new(1, "CelsiusVersFahrenheit", () => { Proche(32, Lecon01.CelsiusVersFahrenheit(0)); Proche(212, Lecon01.CelsiusVersFahrenheit(100)); Proche(-40, Lecon01.CelsiusVersFahrenheit(-40)); }),
        new(1, "DoublerTexte", () => { Egal(42, Lecon01.DoublerTexte("21")); Egal(0, Lecon01.DoublerTexte("0")); }),
    ];
}

public static class TestsLecon02
{
    public static IEnumerable<Test> Tests() =>
    [
        new(2, "Signe", () => { Egal("négatif", Lecon02.Signe(-3)); Egal("nul", Lecon02.Signe(0)); Egal("positif", Lecon02.Signe(8)); }),
        new(2, "Mention", () =>
        {
            Egal("Insuffisant", Lecon02.Mention(9.5)); Egal("Passable", Lecon02.Mention(10));
            Egal("Assez bien", Lecon02.Mention(13.9)); Egal("Bien", Lecon02.Mention(14)); Egal("Très bien", Lecon02.Mention(18));
        }),
        new(2, "SommeJusqua", () => { Egal(15, Lecon02.SommeJusqua(5)); Egal(0, Lecon02.SommeJusqua(0)); Egal(5050, Lecon02.SommeJusqua(100)); }),
        new(2, "Factorielle", () => { Egal(1L, Lecon02.Factorielle(0)); Egal(120L, Lecon02.Factorielle(5)); Egal(2432902008176640000L, Lecon02.Factorielle(20)); }),
        new(2, "FizzBuzz", () =>
        {
            Egal("1", Lecon02.FizzBuzz(1)); Egal("Fizz", Lecon02.FizzBuzz(9));
            Egal("Buzz", Lecon02.FizzBuzz(10)); Egal("FizzBuzz", Lecon02.FizzBuzz(30));
        }),
        new(2, "EstPremier", () =>
        {
            Sequence(new[] { 2, 3, 5, 7, 11, 13, 17, 19 }, Enumerable.Range(0, 20).Where(Lecon02.EstPremier));
        }),
    ];
}

public static class TestsLecon03
{
    public static IEnumerable<Test> Tests() =>
    [
        new(3, "Maximum", () => { Egal(9, Lecon03.Maximum([3, 9, 2])); Egal(-1, Lecon03.Maximum([-5, -1, -3])); }),
        new(3, "Inverser", () =>
        {
            int[] source = [1, 2, 3];
            Sequence([3, 2, 1], Lecon03.Inverser(source));
            Sequence([1, 2, 3], source, "le tableau d'origine ne doit pas être modifié");
        }),
        new(3, "GarderLesPairs", () => Sequence([2, 4, 6], Lecon03.GarderLesPairs([1, 2, 3, 4, 5, 6]))),
        new(3, "CompterMots", () =>
        {
            var compte = Lecon03.CompterMots(["chat", "chien", "chat", "poisson", "chat"]);
            Egal(3, compte["chat"]); Egal(1, compte["chien"]); Egal(3, compte.Count, "nombre de mots différents");
        }),
        new(3, "SansDoublons", () => Sequence([3, 1, 2], Lecon03.SansDoublons([3, 1, 3, 2, 1]))),
    ];
}

public static class TestsLecon04
{
    public static IEnumerable<Test> Tests() =>
    [
        new(4, "Saluer", () => Egal("Bonjour Khaled !", Lecon04.Saluer("Khaled"))),
        new(4, "CompterVoyelles", () => { Egal(3, Lecon04.CompterVoyelles("Bonjour")); Egal(1, Lecon04.CompterVoyelles("rythm")); Egal(5, Lecon04.CompterVoyelles("AEIOU")); }),
        new(4, "EstPalindrome", () => { Egal(true, Lecon04.EstPalindrome("Kayak")); Egal(false, Lecon04.EstPalindrome("Bateau")); Egal(true, Lecon04.EstPalindrome("")); }),
        new(4, "MajusculeChaqueMot", () => Egal("Bonjour Le Monde", Lecon04.MajusculeChaqueMot("bonjour le monde"))),
        new(4, "Compresser", () => { Egal("a3b1c2", Lecon04.Compresser("aaabcc")); Egal("", Lecon04.Compresser("")); }),
    ];
}
