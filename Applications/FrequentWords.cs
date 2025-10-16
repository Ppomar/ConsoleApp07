using System.Security.AccessControl;

namespace ConsoleApp07.Applications;

public class FrequentWords
{
    public static void Execute()
    {
        //Find the most frequent word 
        string phrase = "Hello world! Hello, hello. How are you, world?";
        GetMostFrequentWord(phrase);
    }

    private static void GetMostFrequentWord(string phrase)
    {
        var commonMarks = new List<string> { ".", ",", "?", "!", "'" };

        commonMarks.ForEach(mark => 
            phrase = phrase.Replace(mark, "")
        );

        var wordsList = phrase.Split(' ').ToList();

        var wordsGrouped = wordsList
            .GroupBy(w => w.ToLower())            
            .OrderByDescending(g => g.Count())
            .ThenBy(g => g.Key)
            .Take(1)
            .Select(g => new {
                Word = g.Key,
                Repetitions = g.Count()
            })
            .ToList();

        Console.WriteLine($"---------------- {phrase} -----------------");
        wordsGrouped.ForEach(w => Console.WriteLine("Word: {0}, Repetitions: {1}", w.Word, w.Repetitions));
    }
    
}
