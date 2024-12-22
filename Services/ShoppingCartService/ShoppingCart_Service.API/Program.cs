using Services.ShoppingCartService.ShoppingCart_Service.API;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddShoppingCartServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseShoppingCartServices();

app.Run();

