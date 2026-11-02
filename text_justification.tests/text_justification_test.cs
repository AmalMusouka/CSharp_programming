using System.IO;

public class JustifierTests
{
    private static string Run(string input, int width)
    {
        StringWriter output = new StringWriter();
        Justifier justifier = new Justifier(width, output);
        justifier.Format(new StringReader(input));
        return output.ToString();
    }

    [Fact]
    public void AssignmentExample()
    {
        string input = "If a train station is where the train stops, what is a work station?";
        string expected =
            "If     a    train\n" +
            "station  is where\n" +
            "the  train stops,\n" +
            "what  is  a  work\n" +
            "station?\n";
        Assert.Equal(expected, Run(input, 17));
    }

    [Fact]
    public void LastLineIsLeftAligned()
    {
        Assert.Equal("aa  bb  cc\ndd\n", Run("aa bb cc dd", 10));
    }

    [Fact]
    public void WiderGapsAreOnTheLeft()
    {
        Assert.Equal("a  b c d\ne f\n", Run("a b c d e f", 8));
    }

    [Fact]
    public void SingleWordLineIsLeftAligned()
    {
        Assert.Equal("ab\nabc\n", Run("ab abc", 3));
    }

    [Fact]
    public void WordLongerThanWidthIsAlone()
    {
        Assert.Equal("ab\nabcdefgh\ncd\n", Run("ab abcdefgh cd", 5));
    }

    [Fact]
    public void LongWordAtEndOfFile()
    {
        Assert.Equal("abcdefgh\n", Run("abcdefgh", 5));
    }

    [Fact]
    public void ParagraphsAreSeparatedByOneEmptyLine()
    {
        Assert.Equal("aa bb\n\ncc dd\n", Run("aa bb\n\ncc dd", 10));
    }

    [Fact]
    public void ManyEmptyAndWhitespaceOnlyLinesAreOneSeparator()
    {
        Assert.Equal("aa bb\n\ncc dd\n", Run("aa bb\n\n\n   \n\t\ncc dd", 10));
    }

    [Fact]
    public void LeadingAndTrailingEmptyLinesAreIgnored()
    {
        Assert.Equal("ab\n", Run("\n\n  \nab\n\n\n", 10));
    }

    [Fact]
    public void TabsAndNewlinesInsideParagraphActAsSpaces()
    {
        Assert.Equal("aa bb cc\n", Run("aa\tbb\ncc", 10));
    }

    [Fact]
    public void EmptyInputGivesEmptyOutput()
    {
        Assert.Equal("", Run("", 10));
    }

    [Fact]
    public void WhitespaceOnlyInputGivesEmptyOutput()
    {
        Assert.Equal("", Run(" \n\t\n  ", 10));
    }

    [Fact]
    public void NoTrailingSpacesOnAnyLine()
    {
        string result = Run("aa bb cc dd ee ff gg hh", 7);
        foreach (string line in result.Split('\n'))
        {
            Assert.Equal(line.TrimEnd(), line);
        }
    }
}