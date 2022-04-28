using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SEP4DataWarehouse.DbContext;
using SEP4DataWarehouse.Services;
using SEP4DataWarehouse.Services.Implementations;
using SEP4DataWarehouse.Services.Interfaces;
using SEP4DataWarehouse.Utilities;


var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    //adding the database context
    builder.Services.AddDbContext<DataWarehouseDbContext>();

    // Adding the service classes so i can use them in the controller, automatically add in the constructors
builder.Services.AddScoped<IBoardService, DbBoardService>();
    builder.Services.AddScoped<IUserService, DbUserService>();
    builder.Services.AddScoped<ICarbonDioxideService, DbCarbonDioxideService>();
    builder.Services.AddScoped<IHumidityService, DbHumidityService>();
    builder.Services.AddScoped<ILightService, DbLightService>();
    builder.Services.AddScoped<ITemperatureService, DbTemperatureService>();
   
   


// builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
    

    builder.Services.AddScoped<IExceptionUtilityService, ExceptionUtility>();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    // if (app.Environment.IsDevelopment())
   // {
    app.UseSwagger();
    app.UseSwaggerUI();
   //}

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
