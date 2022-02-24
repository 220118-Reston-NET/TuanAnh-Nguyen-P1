global using Serilog;
using BL.Interfaces;
using BL.Implements;
using DL.Interfaces;
using DL.Implements;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using APIPortal.Areas.Identity.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using APIPortal.AuthenticationService.Interfaces;
using APIPortal.AuthenticationService.Implements;

var builder = WebApplication.CreateBuilder(args);
Log.Logger = new LoggerConfiguration().WriteTo.File("./logs/server.txt").CreateLogger();

// Add services to the container.
var key = builder.Configuration["Token:Key"];

//Identity Role
builder.Services.AddIdentity<IdentityUser, IdentityRole>(x =>
{
  x.Password.RequiredLength = 6;

  x.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<UserDbContext>();


builder.Services.AddScoped<IAccessTokenManager, AccessTokenManager>();

//Authentication
builder.Services.AddAuthentication(x =>
{
  x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
  x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
  x.RequireHttpsMetadata = false;
  x.SaveToken = true;
  x.TokenValidationParameters = new TokenValidationParameters
  {
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
    ValidateIssuer = false,
    ValidateAudience = false
  };
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Reference2DB")));
// builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//      .AddEntityFrameworkStores<UserDbContext>();

builder.Services.AddScoped<ICustomerManagementDL>(repo => new CustomerManagementDL(builder.Configuration.GetConnectionString("Reference2DB")));
builder.Services.AddScoped<IStoreManagementDL>(repo => new StoreManagementDL(builder.Configuration.GetConnectionString("Reference2DB")));
builder.Services.AddScoped<IAdminServiceDL>(repo => new AdminServiceDL(builder.Configuration.GetConnectionString("Reference2DB")));
builder.Services.AddScoped<IOrderManagementDL>(repo => new OrderManagementDL(builder.Configuration.GetConnectionString("Reference2DB")));
builder.Services.AddScoped<IInventoryManagementDL>(repo => new InventoryManagementDL(builder.Configuration.GetConnectionString("Reference2DB")));

builder.Services.AddScoped<ICustomerManagementBL, CustomerManagementBL>();
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
else
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();