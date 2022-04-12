using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SEP4DataWarehouse.DbContext;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//connecting to db
builder.Services.AddDbContext<DataWarehouseContext>(options =>
    options.UseNpgsql(
        "Host=ec2-63-32-248-14.eu-west-1.compute.amazonaws.com;Database=d5g87javtbe0sb;Username=tvxzufojhnbdmi;Password=a4992f6a91bd7d1f61de47b915e66342528b6a310283b29944c6e91924e335f5;"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
