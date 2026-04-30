
namespace PlaywrightTests.Helpers;

public static class StringHelper 
{
    public static string KeepFirstTwoWords(string text) 
    {
        return string.Join(" ", text.Split(' ').Take(2));
    }
}