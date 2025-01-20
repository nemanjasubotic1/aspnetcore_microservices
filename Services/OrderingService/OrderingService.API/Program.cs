using Services.OrderingService.OrderingService.API;
using Services.OrderingService.OrderingService.API.Infrastructure;
using Services.OrderingService.OrderingService.Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddAPIServices(builder.Configuration)
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UserAPIServices();

app.Run();

