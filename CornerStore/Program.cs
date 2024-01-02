using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using CornerStore.Models;
using System.Reflection.Metadata.Ecma335;
using System.Collections.Immutable;
using Microsoft.AspNetCore.Components.Forms;
using System.Text.RegularExpressions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// allows passing datetimes without time zone data 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// allows our api endpoints to access the database through Entity Framework Core and provides dummy value for testing
builder.Services.AddNpgsql<CornerStoreDbContext>(builder.Configuration["CornerStoreDbConnectionString"] ?? "testing");

// Set the JSON serializer options
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//---------------------------endpoints go here---------------------------------------------------------------------------

// Cashiers ------------------------------------------
// POST/add a cashier
app.MapPost("/api/cashiers", (CornerStoreDbContext db, Cashier cashier) => {
    try
    {
    db.Cashiers.Add(cashier);
    db.SaveChanges();
    return Results.Created($"api/cashiers/{cashier.Id}", cashier);
    }
    catch (Exception ex)
    {
        return Results.BadRequest($"Bad data submitted: {ex}");
    }
});

// GET a cashier by id - with orders and orders' products
app.MapGet("/api/cashiers/{id}", (CornerStoreDbContext db, int id) =>
{
    try
    {
        var foundC = db.Cashiers
        .Include(c => c.Orders).ThenInclude(o => o.OrderProducts).ThenInclude(op => op.Product).ThenInclude(p => p.Category)
        .SingleOrDefault(c => c.Id == id);

        if (foundC == null)
        {
            return Results.NotFound("No cashier with given id found");
        }
        return Results.Ok(new CashierDTO
        {
            Id = foundC.Id,
            FirstName = foundC.FirstName,
            LastName = foundC.LastName,
            Orders = foundC.Orders.Select(order => new OrderDTO
            {
                Id = order.Id,
                CashierId = order.CashierId,
                PaidOnDate = order.PaidOnDate,
                OrderProducts = order.OrderProducts.Select(op => new OrderProductDTO
                {
                    Id = op.Id,
                    ProductId = op.ProductId,
                    Product = new ProductDTO
                    {
                        Id = op.Product.Id,
                        ProductName = op.Product.ProductName,
                        Price = op.Product.Price,
                        Brand = op.Product.Brand,
                        CategoryId = op.Product.CategoryId,
                        Category = new CategoryDTO
                        {
                            Id = op.Product.Category.Id,
                            CategoryName = op.Product.Category.CategoryName
                        }
                    },
                    OrderId = op.OrderId,
                    Quantity = op.Quantity
                }).ToList()
            }).ToList()
        });
    }
    catch (Exception ex)
    {
        return Results.NotFound($"Bad data. Found exception: {ex}");
    }
});


// Products -----------------------------------------
// GET all products - with categories - if 'search' query param is present, return only products whose names
// include the 'search' value (ignore case)
app.MapGet("/api/products", (CornerStoreDbContext db, string? search) => {
    try
    {
        var allProducts =db.Products
        .Include(p => p.Category)
        .OrderBy(p => p.Id)
        .Select(p => new ProductDTO
        {
            Id = p.Id,
            ProductName = p.ProductName,
            Price = p.Price,
            Brand = p.Brand,
            CategoryId = p.CategoryId,
            Category = new CategoryDTO
            {
                Id = p.Category.Id,
                CategoryName = p.Category.CategoryName
            }
        }).ToList();

        if(search == null)
        {
            return Results.Ok(allProducts);
        }

        var searchedProducts = allProducts.Where(ap => ap.ProductName.ToLower().Contains(search.ToLower())).ToList();
        if(searchedProducts.Count() < 1)
        {
            return Results.NotFound("No names match search parameters");
        }
        return Results.Ok(searchedProducts);
    }
    catch (Exception ex)
    {
        return Results.BadRequest($"Bad Request: {ex}");
    }
});

// POST/add a product
app.MapPost("/api/products", (CornerStoreDbContext db, Product product) => {
    try
    {
    db.Products.Add(product);
    db.SaveChanges();
    return Results.NoContent();
    }
    catch (Exception ex)
    {
        return Results.BadRequest($"Bad data: {ex}");
    }
});

// PUT/update a product
app.MapPut("/api/products", (CornerStoreDbContext db, Product productObj) => {
    try
    {
        var foundP = db.Products.SingleOrDefault(p => p.Id == productObj.Id);
        if (foundP == null)
        {
            return Results.BadRequest("No product found with given id");
        }

        foundP.ProductName = productObj.ProductName;
        foundP.Price = productObj.Price;
        foundP.Brand = productObj.Brand;
        foundP.CategoryId = productObj.CategoryId;

        db.SaveChanges();
        return Results.NoContent();
    }
    catch (Exception ex)
    {
        return Results.NotFound($"Bad data sent: {ex}");
    }
});


// Orders ---------------------------------------------
// GET order by Id - with cashier, orderProducts, products with category
app.MapGet("/api/orders/{id}", (CornerStoreDbContext db, int id) => {
    try
    {
        var foundO = db.Orders
        .OrderBy(o => o.Id)
        .Include(o => o.Cashier)
        .Include(o => o.OrderProducts).ThenInclude(op => op.Product).ThenInclude(prod => prod.Category)
        .SingleOrDefault(o => o.Id == id);

        if (foundO == null)
        {
            return Results.NotFound("No order with matching id");
        }

        return Results.Ok(new OrderDTO
        {
            Id = foundO.Id,
            CashierId = foundO.CashierId,
            Cashier = new CashierDTO
            {
                Id = foundO.Cashier.Id,
                FirstName = foundO.Cashier.FirstName,
                LastName = foundO.Cashier.LastName
            },
            PaidOnDate = foundO.PaidOnDate,
            OrderProducts = foundO.OrderProducts.Select(orderP => new OrderProductDTO
            {
                Id = orderP.Id,
                ProductId = orderP.ProductId,
                Product = new ProductDTO
                {
                    Id = orderP.Product.Id,
                    ProductName = orderP.Product.ProductName,
                    Price = orderP.Product.Price,
                    Brand = orderP.Product.Brand,
                    CategoryId = orderP.Product.CategoryId,
                    Category = new CategoryDTO
                    {
                        Id = orderP.Product.Category.Id,
                        CategoryName = orderP.Product.Category.CategoryName
                    }
                },
                OrderId = orderP.OrderId,
                Quantity = orderP.Quantity
            }).ToList()
        });
    }
    catch (Exception ex)
    {
        return Results.BadRequest($"Bad data: {ex}");
    }
});

// GET all orders - check for query string param 'orderDate' that only returns orders from a particulary day
// if not present, return all orders
app.MapGet("/api/orders", (CornerStoreDbContext db, DateTime? orderDate) => {
    try
    {
        var allOrders = db.Orders
        .OrderBy(o => o.Id)
        .Include(o => o.Cashier)
        .Include(o => o.OrderProducts).ThenInclude(op => op.Product).ThenInclude(p => p.Category)
        .Select(o => new OrderDTO
        {
            Id = o.Id,
            CashierId = o.CashierId,
            Cashier = new CashierDTO
            {
                Id = o.Cashier.Id,
                FirstName = o.Cashier.FirstName,
                LastName = o.Cashier.LastName
            },
            PaidOnDate = o.PaidOnDate,
            OrderProducts = o.OrderProducts.Select(orderP => new OrderProductDTO
            {
                Id = orderP.Id,
                ProductId = orderP.ProductId,
                Product = new ProductDTO
                {
                    Id = orderP.Product.Id,
                    ProductName = orderP.Product.ProductName,
                    Price = orderP.Product.Price,
                    Brand = orderP.Product.Brand,
                    CategoryId = orderP.Product.CategoryId,
                    Category = new CategoryDTO
                    {
                        Id = orderP.Product.Category.Id,
                        CategoryName = orderP.Product.Category.CategoryName
                    }
                },
                OrderId = orderP.OrderId,
                Quantity = orderP.Quantity
            }).ToList()
        }).ToList();

        if(orderDate == null)
        {
            return Results.Ok(allOrders);
        }

        var filteredOrders = allOrders.Where(order => order.PaidOnDate == orderDate).ToList();

        if(filteredOrders.Count == 0)
        {
            return Results.NotFound("No results matching DateTime query parameters");
        }

        return Results.Ok(filteredOrders);

    }
    catch (Exception ex)
    {
        return Results.BadRequest($"Bad data: {ex}");
    }
});

// DELETE an order by Id
app.MapDelete("/api/orders/{id}", (CornerStoreDbContext db, int id) => {
    try
    {
        var foundO = db.Orders.SingleOrDefault(o => o.Id == id);

        if(foundO == null)
        {
            return Results.NotFound("No order with given param id");
        }

        db.Orders.Remove(foundO);
        db.SaveChanges();
        return Results.NoContent();
    }
    catch (Exception ex)
    {
        return Results.BadRequest($"Bad data: {ex}");
    }
});

// POST create an order (with products!)
app.MapPost("/api/orders", (CornerStoreDbContext db, Order order) => {
    try
    {
        int id = db.Orders.Count() + 1;
        foreach(OrderProduct op in order.OrderProducts)
        {
            op.OrderId = id;
            db.OrderProducts.Add(op);
        }

        db.Orders.Add(order);
        db.SaveChanges();

        return Results.Created($"/api/orders/{order.Id}", order);
    }
    catch (Exception ex)
    {
        return Results.BadRequest($"Bad request: {ex}");
    }
});


/* *~*~*~*~*~*~*~*~*~BONUS CHALLENGE FOR END~*~*~*~*~*~*~*~*~*~*~*~*~*
GET most popular products, determined by which products have been ordered the most times.
HINT: this requires using GroupBy to group the OrderProducts by ProductId, then using SUM to add
up all the Quantities of the OrderProducts in each group.
Check for a query string param called "AMOUNT" that says how many products to return. Retrun 5 by default. 
*/
// /produts/popular
app.MapGet("/api/products/popular", (CornerStoreDbContext db, int? amount) => {
    try
    {
        // generate list of OrderProducts, including Product with Category
        var OPs = db.OrderProducts
        .Include(p => p.Product).ThenInclude(prod => prod.Category)
        .ToList();

        // Group by ProductId & sort by descending count
        var groupedOPs = OPs.GroupBy(op => op.ProductId)
        .Select(group => new
        {
            ProductId = group.Key,
            QuantitySum = group.Sum(op => op.Quantity)
        })
        .OrderByDescending(group => group.QuantitySum)
        .Take(amount ?? 5)
        .ToList();

        var arr = new List<Product>();
        foreach(var gop in groupedOPs)
        {
            var found = db.Products.SingleOrDefault(p => p.Id == gop.ProductId);
            arr.Add(found);
        }

        return Results.Ok(arr.Select(a => new ProductDTO
        {
            Id = a.Id,
            ProductName = a.ProductName,
            Price = a.Price,
            Brand = a.Brand,
            CategoryId = a.CategoryId,
            Category = new CategoryDTO
            {
                Id = a.Category.Id,
                CategoryName = a.Category.CategoryName
            }
        }));

    }
    catch (Exception ex)
    {
        return Results.BadRequest($"Bad data request: {ex}");
    }

});

app.Run();

//don't move or change this!
public partial class Program { }