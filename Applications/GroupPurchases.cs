namespace ConsoleApp07.Applications;

public class GroupPurchases
{
    public static void Execute() 
    {
        var products = new List<Product> {
            new Product { Id = 101, Name = "Laptop", Price = 1456.85m },
            new Product { Id = 102, Name = "Mouse", Price = 30.54m },
            new Product { Id = 103, Name = "Keyboard", Price = 80.50m },
            new Product { Id = 104, Name = "Monitor", Price = 435.45m },
            new Product { Id = 105, Name = "USB Driver", Price = 10.50m },
            new Product { Id = 106, Name = "Joystick", Price = 86.85m },
        };

        var purchases = new List<Purchase> {
                new Purchase { Id = 1, ProductId = 101, Quantity = 2, Date = DateTime.Today },
                new Purchase { Id = 2, ProductId = 102, Quantity = 5, Date = DateTime.Today.AddDays(-1) },
                new Purchase { Id = 3, ProductId = 103, Quantity = 3, Date = DateTime.Today.AddDays(-2) },
                new Purchase { Id = 4, ProductId = 101, Quantity = 1, Date = DateTime.Today.AddDays(-1) },
                new Purchase { Id = 5, ProductId = 104, Quantity = 2, Date = DateTime.Today },
                new Purchase { Id = 6, ProductId = 105, Quantity = 10, Date = DateTime.Today.AddDays(-3) }
        };

        GetLeftJoinTotalPurshase(products, purchases);

    }

    private static void GetLeftJoinTotalPurshase(List<Product> products, List<Purchase> purchases)
    {
        var joined = products
            .GroupJoin(
                purchases,
                pr => pr.Id,
                pu => pu.ProductId,
                (product, purchase) => new
                {
                    product.Name,
                    product.Price,
                    Purchases = purchase.Sum(p => p.Quantity),
                    Total = purchase.Sum(p => p.Quantity) * product.Price
                }
            ).ToList();

        foreach ( var purchase in joined)    
            Console.WriteLine("Product: {0}, Quantity: {1}, Price: {2}, Total: {3}", purchase.Name, purchase.Purchases, purchase.Price, purchase.Total);
    }



    private class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public decimal Price { get; set; }
    }


    private class Purchase
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
    }
}
