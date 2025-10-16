
namespace ConsoleApp07.Applications;

public class GroupAndCount
{
    public static void Execute() 
    {
        /* 
        * You have a sales list. It groups sales by product and shows how many times each one was sold.
        */

        var sales = new List<Sales>
        {
            new Sales { Id = 1, Product = "Laptop", Quantity = 1 },
            new Sales { Id = 2, Product = "Mouse",  Quantity = 3 },
            new Sales { Id = 3, Product = "Laptop", Quantity = 2 },
            new Sales { Id = 4, Product = "Teclado",Quantity = 1 },
            new Sales { Id = 5, Product = "Mouse",  Quantity = 1 }
        };

        GetOrderedSales(sales);
    }

    private static void GetOrderedSales(List<Sales> sales)
    {
        var ordered = sales
            .GroupBy(s => new
            {
                Name = s.Product                
            })
            .OrderByDescending(g => g.Sum(g => g.Quantity))
            .Select(g => new {
                Name = g.Key.Name,
                Quantity = g.Sum(g => g.Quantity)
            })
            .ToList();

        foreach (var sale in ordered) 
        {
            Console.WriteLine("Name: {0}, Quantity: {1}", sale.Name, sale.Quantity);
        }
    }

    private class Sales
    {
        public int Id { get; set; }
        public string Product { get; set; } = default!;
        public int Quantity { get; set; }
    }
}
