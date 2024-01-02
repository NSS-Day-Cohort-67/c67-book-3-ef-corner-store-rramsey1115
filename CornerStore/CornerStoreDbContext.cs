using Microsoft.EntityFrameworkCore;
using CornerStore.Models;
public class CornerStoreDbContext : DbContext
{
    public DbSet<Cashier> Cashiers { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }
    public DbSet<Product> Products { get; set; }

    public CornerStoreDbContext(DbContextOptions<CornerStoreDbContext> context) : base(context)
    {

    }

    //allows us to configure the schema when migrating as well as seed data
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Cashiers
        modelBuilder.Entity<Cashier>().HasData(new Cashier[]
        {
            new Cashier { Id = 1, FirstName = "Bill", LastName = "Gates" },
            new Cashier { Id = 2, FirstName = "Will", LastName = "Andrews" }
        });

        // Orders
        modelBuilder.Entity<Order>().HasData(new Order[]
        {
            new Order { Id = 1, CashierId = 1 },
            new Order { Id = 2, CashierId = 2 },
            new Order { Id = 3, CashierId = 1 },
            new Order { Id = 4, CashierId = 1 },
            new Order { Id = 5, CashierId = 1 },
            new Order { Id = 6, CashierId = 2 },
            new Order { Id = 7, CashierId = 2 },
            new Order { Id = 8, CashierId = 2 },
            new Order { Id = 9, CashierId = 1 },
            new Order { Id = 10, CashierId = 2 }
        });

        // OrderProducts
        modelBuilder.Entity<OrderProduct>().HasData(new OrderProduct[]
        {
            new OrderProduct { Id = 1, ProductId = 1, OrderId = 1, Quantity = 2 },
            new OrderProduct { Id = 2, ProductId = 2, OrderId = 1, Quantity = 1 },
            new OrderProduct { Id = 3, ProductId = 3, OrderId = 1, Quantity = 2 },
            new OrderProduct { Id = 4, ProductId = 6, OrderId = 2, Quantity = 1 },
            new OrderProduct { Id = 5, ProductId = 4, OrderId = 2, Quantity = 1 },
            new OrderProduct { Id = 6, ProductId = 5, OrderId = 2, Quantity = 1 },
            new OrderProduct { Id = 7, ProductId = 4, OrderId = 3, Quantity = 2 },
            new OrderProduct { Id = 8, ProductId = 2, OrderId = 4, Quantity = 3 },
            new OrderProduct { Id = 9, ProductId = 5, OrderId = 5, Quantity = 2 },
            new OrderProduct { Id = 10, ProductId = 2, OrderId = 5, Quantity = 1 },
            new OrderProduct { Id = 11, ProductId = 6, OrderId = 6, Quantity = 1 },
            new OrderProduct { Id = 12, ProductId = 1, OrderId = 7, Quantity = 1 },
            new OrderProduct { Id = 13, ProductId = 3, OrderId = 7, Quantity = 3 },
            new OrderProduct { Id = 14, ProductId = 4, OrderId = 7, Quantity = 2 },
            new OrderProduct { Id = 15, ProductId = 5, OrderId = 8, Quantity = 2 },
            new OrderProduct { Id = 16, ProductId = 3, OrderId = 9, Quantity = 1 },
            new OrderProduct { Id = 17, ProductId = 4, OrderId = 9, Quantity = 1 },
            new OrderProduct { Id = 18, ProductId = 6, OrderId = 10, Quantity = 2 },
            new OrderProduct { Id = 19, ProductId = 3, OrderId = 10, Quantity = 3 }
        });

        // Products
        modelBuilder.Entity<Product>().HasData(new Product[]
        {
            new Product { Id = 1, ProductName = "12oz Can", Price = 2.99M, Brand = "Sprite", CategoryId = 1 },
            new Product { Id = 2, ProductName = "20oz Bottle", Price = 2.50M, Brand = "Coca-Cola", CategoryId = 1 },
            new Product { Id = 3, ProductName = "Small Candy", Price = 2.75M, Brand = "M&Ms", CategoryId = 2 },
            new Product { Id = 4, ProductName = "Large Candy", Price = 3.50M, Brand = "Twix", CategoryId = 2 },
            new Product { Id = 5, ProductName = "Small Chips", Price = 3.25M, Brand = "Lays", CategoryId = 3 },
            new Product { Id = 6, ProductName = "Large Chips", Price = 5.99M, Brand = "Doritos", CategoryId = 3 }
        });

        // Categories
        modelBuilder.Entity<Category>().HasData(new Category[]
        {
            new Category { Id = 1, CategoryName = "Beverages" },
            new Category { Id = 2, CategoryName = "Candies" },
            new Category { Id = 3, CategoryName = "Snacks" }
        });
    }
}