using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SEP4DataWarehouse.BusinessLogic;
using SEP4DataWarehouse.Contexts.DbContext;
using SEP4DataWarehouse.Contexts.DwContext;
using SEP4DataWarehouse.Services;
using SEP4DataWarehouse.Services.DbServices;
using SEP4DataWarehouse.Services.DbServices.Implementations;
using SEP4DataWarehouse.Utilities;


var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    //adding the database context
    builder.Services.AddDbContext<GreenHouseDbContext>();
    builder.Services.AddDbContext<GreenHouseDwContext>();

    // Adding the service classes so i can use them in the controller, automatically add in the constructors
    builder.Services.AddScoped<IBoardService, DbBoardService>();
    builder.Services.AddScoped<IUserService, DbUserService>();
    builder.Services.AddScoped<ICarbonDioxideService, DbCarbonDioxideService>();
    builder.Services.AddScoped<IHumidityService, DbHumidityService>();
    builder.Services.AddScoped<ILightService, DbLightService>();
    builder.Services.AddScoped<ITemperatureService, DbTemperatureService>();
    builder.Services.AddScoped<IEventService, DbEventService>();
    builder.Services.AddScoped<IReadingService, DbReadingService>();
    

    builder.Services.AddScoped<CheckForValues>();
   
    builder.Services.AddScoped<IExceptionUtilityService, ExceptionUtility>();


   


// builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
    

   

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
