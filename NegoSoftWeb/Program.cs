using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NegoSoftWeb.Data;
using DotNetEnv;
using NegoSoftShared.Models.Entities;
using NegoSoftWeb.Services.ProductService;
using NegoSoftWeb.Services.CartService;
using NegoSoftWeb.Services.CustomerService;
using NegoSoftWeb.Services.AddressService;
using NegoSoftWeb.Services.CustomerOrderService;
using NegoSoftWeb.Services.PaymentsService;
using NegoSoftWeb.Services.SupplierOrderService;
using NegoSoftWeb.Services.SupplierService;
using Stripe;
using NegoSoftWeb.Models.Entities;


Env.Load();
var builder = WebApplication.CreateBuilder(args);

var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
builder.Services.AddDbContext<NegoSoftContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<NegoSoftContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IProductService, NegoSoftWeb.Services.ProductService.ProductService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ICustomerService, NegoSoftWeb.Services.CustomerService.CustomerService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<ICustomerOrderService, CustomerOrderService>();
builder.Services.AddScoped<IPaymentsService, PaymentsService>();
builder.Services.AddScoped<ISupplierOrderService, SupplierOrderService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();

//add session to the application
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // dur�e de la session
    options.Cookie.HttpOnly = true; // le cookie de session ne peut pas �tre acc�d� par le client
    options.Cookie.IsEssential = true; // le cookie de session est essentiel
});
builder.Services.AddHttpContextAccessor(); // pour acc�der � la session dans les services

builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));

StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
