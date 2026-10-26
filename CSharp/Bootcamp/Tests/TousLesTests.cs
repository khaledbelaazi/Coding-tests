namespace Bootcamp.Tests;

public static class TousLesTests
{
    public static IEnumerable<Test> Lister() =>
        TestsLecon01.Tests()
            .Concat(TestsLecon02.Tests())
            .Concat(TestsLecon03.Tests())
            .Concat(TestsLecon04.Tests())
            .Concat(TestsLecon05.Tests())
            .Concat(TestsLecon06.Tests())
            .Concat(TestsLecon07.Tests())
            .Concat(TestsLecon08.Tests())
            .Concat(TestsLecon09.Tests())
            .Concat(TestsLecon10.Tests())
            .Concat(TestsLecon11.Tests())
            .Concat(TestsLecon12.Tests())
            .Concat(TestsLecon13.Tests())
            .Concat(TestsLecon14.Tests())
            .Concat(TestsLecon15.Tests())
            .Concat(TestsLecon16.Tests());
}
