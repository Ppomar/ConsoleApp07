namespace ConsoleApp07.Applications;

public class GreaterPrice
{
    public static void Execute()
    {
        /*
            Given a list of products, return all products with a price greater than 50, 
            sorted by descending price. Display only the name and price. 
       */

        var products = new Product[] {
            new Product{ Id = 1, Name = "Laptop", Price = 1200.54m },
            new Product{ Id = 2, Name = "Mouse", Price = 25.43m},
            new Product{ Id = 3, Name = "Teclado", Price = 75.65m },
            new Product{ Id = 4, Name = "Monitor", Price = 250.84m },
            new Product{ Id = 5, Name = "Cargador", Price = 45.62m }
        };

        GetGreaterPrice(50, products);
    }

    private static void GetGreaterPrice(int price, Product[] products)
    {
        var productsGreater = products
            .Where(p => p.Price > price)
            .ToList();

        foreach (var p in productsGreater) 
        {
            Console.WriteLine("Product: {0}, Price: {1}", p.Name, p.Price);
        }
    }

    private class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public decimal Price { get; set; }
    }
}
