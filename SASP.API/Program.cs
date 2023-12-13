using Microsoft.EntityFrameworkCore;
using SASP.API.Data;
using SASP.API.Entities;
using SASP.API.Repositories;
using SASP.API.Repositories.Contracts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy", builder =>
    {
        builder
        .AllowAnyMethod()
        .AllowAnyHeader()
        .WithOrigins("http://localhost:3000",
        "http://localhost:3001",
        "http://localhost:3002",
        "http://localhost:3003",
        "http://appname.azurestaticapps.net");
    });
});

builder.Services.AddControllers();

builder.Services.AddDbContextPool<SASPDbContext>(option =>
    option.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection")));

builder.Services.AddDbContextPool<AdminSASPDbContext>(option =>
    option.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection2")));

#region Repository Services

builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IRepository<User>, UserRepository>();
builder.Services.AddScoped<IRepository<City>, CityRepository>();
builder.Services.AddScoped<IRepository<Country>, CountryRepository>();
builder.Services.AddScoped<IRepository<Catalog>, CatalogRepository>();
builder.Services.AddScoped<IRepository<Issue>, IssueRepository>();
builder.Services.AddScoped<IRepository<OrderHistory>, OrderHistoryRepository>();
builder.Services.AddScoped<IRepository<Subscription>, SubscriptionRepository>();
builder.Services.AddScoped<IRepository<TypeIssue>, TypeIssueRepository>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("CORSPolicy");

app.MapControllers();

app.Run();
