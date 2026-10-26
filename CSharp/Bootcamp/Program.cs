using Bootcamp.Tests;

// Bootcamp C# : lance les tests de toutes les leçons (ou d'une seule : dotnet run -- 3)
int? lecon = args.Length > 0 && int.TryParse(args[0], out var n) ? n : null;

Console.OutputEncoding = System.Text.Encoding.UTF8;
var resultat = Testeur.Executer(TousLesTests.Lister(), lecon);
return resultat ? 0 : 1;
