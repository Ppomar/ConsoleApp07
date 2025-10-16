namespace ConsoleApp07.Applications;

public class GroupLetters
{
    public static void Execute()
    {
        string input = "Hello world, this is an exercise for a Mock Interview";
        GetGroupedLetters(input);
    }

    private static void GetGroupedLetters(string input)
    {
        var letters = input
            .Replace(" ", "")
            .ToUpper()
            .Where(c => char.IsLetter(c))
            .GroupBy(c => c)
            .OrderByDescending(c => c.Count())
            .Take(1)
            .Select(c => new
            {
                Letter = c.Key,
                Count = c.Count()
            })
            .ToList();

        foreach (var l in letters)
        {
            Console.WriteLine("Letter: {0}, Total: {1}", l.Letter, l.Count);
        }
    }

}
