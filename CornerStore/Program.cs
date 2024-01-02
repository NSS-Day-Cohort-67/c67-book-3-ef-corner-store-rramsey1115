using CornerStore.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;

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


// GET a cashier - with orders and orders' products




// Products -----------------------------------------
// GET all products - with categories - if 'searc' query param is present, return only products whose names
// include the 'search' value (ignore case)


// POST/add a product


// PUT/update a product


// Orders ---------------------------------------------
// GET order by ID - with cashier, orderProducts, products with category

// GET all orders - check for query string param 'orderDate' taht only returns orders from a particulary day
// if not present, return all orders




app.Run();

//don't move or change this!
public partial class Program { }