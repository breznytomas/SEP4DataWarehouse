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
    builder.Services.AddDbContext<DataWarehouseContext>();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
    app.UseSwagger();
    app.UseSwaggerUI();
    }

    // if you are having trouble running the app locally or it's not working in production maybe comment this out, i'm still unsure what it does but sometimes it helped me to get rid of it ???
    // so far i haven't found it to have any effect on whether the app runs with https or not
    // if everything seems to be working okay just disregard this comment
    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
