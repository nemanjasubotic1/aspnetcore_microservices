using Marten;
using ProductCategory.API;
using ProductCategory.API.InitialData;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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


