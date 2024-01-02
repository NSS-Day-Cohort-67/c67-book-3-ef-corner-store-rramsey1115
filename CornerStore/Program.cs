using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;

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
// GET all products - with categories - if 'searc' query param is present, return only products whose names
// include the 'search' value (ignore case)


// POST/add a product


// PUT/update a product




// Orders ---------------------------------------------
// GET order by Id - with cashier, orderProducts, products with category


// GET all orders - check for query string param 'orderDate' that only returns orders from a particulary day
// if not present, return all orders


// DELETE an order by Id


// POST create an order (with products!)



/* *~*~*~*~*~*~*~*~*~BONUS CHALLENGE FOR END~*~*~*~*~*~*~*~*~*~*~*~*~*
GET most popular products, determined by which products have been ordered the most times.
HINT: this requires using GroupBy to group the OrderProducts by ProductId, then using SUM to add
up all the Quantities of the OrderProducts in each group.
Check for a query string param called "AMOUNT" that says how many products to return. Retrun 5 by default. 
*/
// /produts/popular


app.Run();

//don't move or change this!
public partial class Program { }