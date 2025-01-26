using EmailService.API.Utility;
using Microsoft.EntityFrameworkCore;
using Services.EmailService.EmailService.API.Data;
using Services.EmailService.EmailService.API.Extensions;
using Services.EmailService.EmailService.API.FactoryAppDbContext;
using Services.EmailService.EmailService.API.Messaging.AzureServiceBusConsumer;
using Services.EmailService.EmailService.API.Messaging.RabbitMQMessageConsumer;
using Services.EmailService.EmailService.API.Services.RegistrationNotification;
using Services.EmailService.EmailService.API.Services.ShoppingCartEmail;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<IAppDbContextFactory>(provider =>
{
    var optionBuilder = new DbContextOptionsBuilder<AppDbContext>();

    optionBuilder.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));

    return new AppDbContextFactory(optionBuilder.Options);  

});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddSingleton<IServiceBusConsumer, ServiceBusConsumer>();
builder.Services.AddSingleton<IShoppingCartEmailService, ShoppingCartEmailService>();
builder.Services.AddSingleton<IRegistrationNotify, RegistrationNotify>();

builder.Services.AddScoped<IDbInitializer, DbInitializer>();

builder.Services.AddHostedService<RabbitMQAuthConsumer>();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAzureServiceBusConsumer();

InitDatabase();

app.Run();

void InitDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        dbInitializer.Initialize();
    }
}

