global using Serilog;
using BL.Interfaces;
using BL.Implements;
using DL.Interfaces;
using DL.Implements;

var builder = WebApplication.CreateBuilder(args);
Log.Logger = new LoggerConfiguration().WriteTo.File("./logs/server.txt").CreateLogger();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICustomerManagementDL>(repo => new CustomerManagementDL(builder.Configuration.GetConnectionString("Reference2DB")));
builder.Services.AddScoped<IStoreManagementDL>(repo => new StoreManagementDL(builder.Configuration.GetConnectionString("Reference2DB")));
builder.Services.AddScoped<IAdminServiceDL>(repo => new AdminServiceDL(builder.Configuration.GetConnectionString("Reference2DB")));
builder.Services.AddScoped<IOrderManagementDL>(repo => new OrderManagementDL(builder.Configuration.GetConnectionString("Reference2DB")));
builder.Services.AddScoped<IInventoryManagementDL>(repo => new InventoryManagementDL(builder.Configuration.GetConnectionString("Reference2DB")));

builder.Services.AddScoped<ICustomerMangementBL, CustomerManagementBL>();
builder.Services.AddScoped<IStoreManagementBL, StoreManagementBL>();
builder.Services.AddScoped<IAdminServiceBL, AdminServiceBL>();
builder.Services.AddScoped<IOrderManagementBL, OrderManagementBL>();
builder.Services.AddScoped<IInventoryManagementBL, InventoryManagementBL>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
