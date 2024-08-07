using ConsoleApp1.Data;
using ConsoleApp1.Models;
using Microsoft.Identity.Client;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var dbContext = new ApplicationDbContext();

            // Add data to products and orders tables
            List<Product> myProducts = new List<Product>
            {
                    new Product { Name = "Product1", price = 10.99 },
                    new Product { Name = "Product2", price = 20.99 },
                    new Product { Name = "Product3", price = 30.99 }
            };
           
            List<Order> myOrders = new List<Order>
            {
                    new Order { CreatedAt = DateTime.Now },
                    new Order { CreatedAt = DateTime.Now },
                    new Order { CreatedAt = DateTime.Now }
            };
            dbContext.products.AddRange(myProducts);
            dbContext.orders.AddRange(myOrders);
            dbContext.SaveChanges();

            // Get all data from products and orders tables
            var productsInDatabase = dbContext.products.ToList();
            foreach(var product in productsInDatabase)
            {
                Console.WriteLine(product.Name);
            }
            var ordersInDatabase = dbContext.orders.ToList();
            foreach (var order in ordersInDatabase)
            {
                Console.WriteLine(order.CreatedAt);
            }

           // Update data in products and orders tables
            var productToUpdate = dbContext.products.First(pr => pr.Id == 1);
            productToUpdate.Name = $"{productToUpdate.Name}--Updated";
            dbContext.SaveChanges();
            Console.WriteLine(productToUpdate.Name);

            var orderToUpdate = dbContext.orders.First(or => or.Id == 1);
            orderToUpdate.CreatedAt = new DateTime(2024, 07, 01);
            dbContext.SaveChanges();
            Console.WriteLine(orderToUpdate.CreatedAt);

            // Remove data from products and orders tables

            var productToRemove = dbContext.products.First(pr => pr.Id == 2);
            dbContext.products.Remove(productToRemove);
            dbContext.SaveChanges();

            var orderToRemove = dbContext.orders.First(or => or.Id == 3);
            dbContext.orders.Remove(orderToRemove);
            dbContext.SaveChanges();


        }
    }
}