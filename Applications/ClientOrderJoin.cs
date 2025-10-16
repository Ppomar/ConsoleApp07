namespace ConsoleApp07.Applications;

public class ClientOrderJoin
{
    public static void Execute() 
    {
        /* 
          You have a list of customers and a list of orders. 
          It displays the customer's name along with the order total.
        */

        var clients = new List<Client>
        {
            new Client { Id = 1, Name = "Juan" },
            new Client { Id = 2, Name = "Cari" },
            new Client { Id = 3, Name = "Pedro" },
            new Client { Id = 4, Name = "Omar" }
        };

        var orders = new List<Order>
        {
            new Order { Id = 1, ClientId = 1, Total = 250 },
            new Order { Id = 2, ClientId = 1, Total = 120 },
            new Order { Id = 3, ClientId = 2, Total = 300 },
            new Order { Id = 4, ClientId = 3, Total = 50 },
            new Order { Id = 5, ClientId = 3, Total = 50 },
        };

        LeftJoin(clients, orders);
    }

    private static void InnerJoinClient(List<Client> clients, List<Order> orders) 
    {
        var sales = orders
            .Join(
                clients,
                o => o.ClientId,
                c => c.Id,
                (order, client) => new 
                {
                    Id = client.Id,
                    Name = client.Name,
                    Total = order.Total
                }
            )
            .GroupBy(g => new { g.Id, g.Name })
            .Select(g => new { 
                    Id = g.Key.Id,
                    Name = g.Key.Name,
                    Total = g.Sum(g => g.Total)
            })
            .ToList();

        foreach (var sale in sales) 
        {
            Console.WriteLine("Client: {0}, Total: {1}", sale.Name, sale.Total);
        }

    }

    private static void LeftJoin(List<Client> clients, List<Order> orders) 
    {
        var filtered = clients
            .GroupJoin(
                orders,
                c => c.Id,
                o => o.ClientId,
                (client, order) => new
                {
                    Id = client.Id,
                    Name = client.Name,
                    Total = order.Sum(o => o.Total)
                })
            .ToList();

        foreach (var client in filtered) 
        {
            Console.WriteLine("Client: {0}, Total: {1}", client.Name, client.Total);
        }
    }

    private class Client
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
    }

    private class Order
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public decimal Total { get; set; }
    }
}


