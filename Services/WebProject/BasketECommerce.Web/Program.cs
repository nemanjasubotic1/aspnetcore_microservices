using BasketECommerce.Web.Services;
using BasketECommerce.Web.Services.AuthenticationService;
using BasketECommerce.Web.Services.Ordering;
using BasketECommerce.Web.Services.ProductCategory;
using BasketECommerce.Web.Services.ShoppingCart;
using Microsoft.AspNetCore.Authentication.Cookies;
using Refit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ITokenProvider, TokenProvider>();

builder.Services.AddScoped<AuthenticatedHttpClientHandler>();

builder.Services.AddRefitClient<ICategoryService>()
    .ConfigureHttpClient(config =>
    {
        config.BaseAddress = new Uri(builder.Configuration["ApiSettings:GatewayAddress"]!);
    }).AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

builder.Services.AddRefitClient<IProductService>()
    .ConfigureHttpClient(config =>
    {
        config.BaseAddress = new Uri(builder.Configuration["ApiSettings:GatewayAddress"]!);
    }).AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

builder.Services.AddRefitClient<IShoppingCartService>()
    .ConfigureHttpClient(config =>
    {
        config.BaseAddress = new Uri(builder.Configuration["ApiSettings:GatewayAddress"]!);
    }).AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

builder.Services.AddRefitClient<IOrderingService>()
    .ConfigureHttpClient(config =>
    {
        config.BaseAddress = new Uri(builder.Configuration["ApiSettings:GatewayAddress"]!);
    }).AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

builder.Services.AddRefitClient<IAuthService>()
    .ConfigureHttpClient(config =>
    {
        config.BaseAddress = new Uri(builder.Configuration["ApiSettings:GatewayAddress"]!);
    }).AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);

        options.LoginPath = $"/Authentication/Login";
        options.LogoutPath = $"/Authentication/Logout";
        options.AccessDeniedPath = $"/Authentication/AccessDenied";

        //options.SlidingExpiration = true;
    });

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpContextAccessor();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
