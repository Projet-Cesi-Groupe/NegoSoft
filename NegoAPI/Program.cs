using NegoAPI.Services.ProductService;
using NegoAPI.Services.SupplierService;
using NegoAPI.Services.TypeService;
using NegoAPI.Services.CustomerService;
using Microsoft.EntityFrameworkCore;
using NegoSoftWeb.Data;
using DotNetEnv;
using NegoAPI.Services.SupplierOrderDetailsService;
using NegoAPI.Services.SupplierOrderService;
using NegoAPI.Services.CustomerOrderDetailsService;
using NegoAPI.Services.CustomerOrderService;
using NegoAPI.Services.AddressService;
using Microsoft.AspNetCore.Identity;
using NegoSoftWeb.Models.Entities;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//prendre exemple sur NegoSoftWeb/Program.cs pour la configuration de la base de données
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Env.Load();
var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
builder.Services.AddDbContext<NegoSoftContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<NegoSoftContext>()
    .AddDefaultTokenProviders();


builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<ITypeService, TypeService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ISupplierOrderDetailsService, SupplierOrderDetailsService>();
builder.Services.AddScoped<ISupplierOrderService, SupplierOrderService>();
builder.Services.AddScoped<ICustomerOrderDetailsService, CustomerOrderDetailsService>();
builder.Services.AddScoped<ICustomerOrderService, CustomerOrderService>();
builder.Services.AddScoped<IAddressService, AddressService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();

app.MapControllers();

app.Run();
