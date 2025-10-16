namespace ConsoleApp07.Applications;

public class Anagrams
{
    public static void Execute() 
    {
        Console.WriteLine(GetAnagram("listen", "silent")); // True
        Console.WriteLine(GetAnagram("aabbcc", "baccab")); // True
        Console.WriteLine(GetAnagram("hello", "holle"));   // False
        Console.WriteLine(GetAnagram("aabbc", "aaabbc"));

    }

    private static bool GetAnagram(string word1, string word2) 
    {
        var results = new List<bool>();

        var x = word1
            .GroupBy(c => c.ToString().ToLower())
            .OrderBy(g => g.Key)
            .Select(g => new
            {
                Letter = g.Key,
                Repeats = g.Count()
            }).ToList();

        var y = word2
            .GroupBy(c => c.ToString().ToLower())
            .OrderBy(g => g.Key)
            .Select(g => new
            {
                Letter = g.Key,
                Repeats = g.Count()
            }).ToList();       

        return x.SequenceEqual(y);
    }

    private static bool GetAnagram2(string word1, string word2)
    {
        if (string.IsNullOrWhiteSpace(word1) || string.IsNullOrWhiteSpace(word2))
            return false;

        word1 = new string(word1.ToLower().Where(char.IsLetter).ToArray());
        word2 = new string(word2.ToLower().Where(char.IsLetter).ToArray());

        if (word1.Length != word2.Length)
            return false;

        var freq1 = word1.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());
        var freq2 = word2.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());

        return freq1.Count == freq2.Count && !freq1.Except(freq2).Any();
    }
}
