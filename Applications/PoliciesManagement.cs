namespace ConsoleApp07.Applications;

public class PoliciesManagement
{
    public static void Execute() 
    {
        /*
         * Create a method called GetTopTwoPolicyTypes that takes an array of 
         * policy types (represented as integers) 
           and returns the two policy types that are most frequently sold. 
           This method will help an insurance company identify their most popular products.
         */

        // policies collection
        int[] policies = [1, 2, 3, 4, 5, 6, 2, 3, 4, 2, 4, 5, 2, 1, 1, 6];

        GetTopTwoPolicyTypes(policies);
    }

    private static void GetTopTwoPolicyTypes(int[] policies) 
    {
        var filtered = policies
            .GroupBy(p => p)
            .OrderByDescending(g => g.Count())
            .Take(2)
            .Select(g => new { 
                Policy = g.Key, 
                Counting = g.ToList().Count() 
            })
            .ToList();

        foreach (var policy in filtered) 
        {   
            Console.WriteLine("Policy: {0}, Total: {1}", policy.Policy, policy.Counting);
        }
    }
}
