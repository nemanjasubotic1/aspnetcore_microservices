using CouponService.API.Data;
using Microsoft.EntityFrameworkCore;
using CouponService.API.Services;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using CouponService.API.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddGrpc();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.WebHost.UseKestrel(options =>
{
    options.ListenAnyIP(50051, o => o.Protocols = HttpProtocols.Http2);
});

builder.Services.AddScoped<IDbInitializer, DbInitializer>();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseRouting();

app.MapGrpcService<CouponServiceProvider>();

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