using garbageDetetionApi.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<GarbageDbContext>(options =>
    options.UseSqlServer(
        // builder.Configuration.GetConnectionString("DefaultConnection"),
        "Server=localhost,1433;Database=ICT14;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True;",
        sqlOptions => sqlOptions.EnableRetryOnFailure()
    )
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
