using FirstCourseApp;
using FirstCourseApp.CsvReader;
using FirstCourseApp.Data;
using FirstCourseApp.Entities;
using FirstCourseApp.Repositories;
using FirstCourseApp.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using System.Configuration;
using System;
using Serilog;

var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(configuration)
        .CreateLogger();

var services = new ServiceCollection();
services.AddSingleton<IApp,App>();
services.AddSingleton<IRepository<Client>,ListRepository<Client>>();
services.AddSingleton<ICsvReader,CsvReader>();

services.AddDbContext<FirstCourseAppDbContext>(options => options
    .UseSqlServer(configuration.GetConnectionString("Baza")));

var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetService<IApp>();

try
{
    Log.Information("App Start");
    app.Run();
    Log.Information("App Stop");

}
catch (Exception ex)
{
    Log.Fatal(ex, "ERROR in START APP");
}
finally
{
    Log.CloseAndFlush();
}


