using Main.ProductService.ProductCategory.API.InitialData;
using Marten;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("DefaultConnection")!);

    options.Schema.For<Category>().Identity(l => l.Id).SoftDeleted();
    options.Schema.For<Product>().Identity(l => l.Id).Index(l => l.CategoryId);

}).UseLightweightSessions();



builder.Services.AddProductServices(builder.Configuration);

if (builder.Environment.IsDevelopment())
{
    builder.Services.InitializeMartenWith<CategoryInitialData>();
    builder.Services.InitializeMartenWith<ProductInitialData>();
}

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseProductServices();

app.Run();


