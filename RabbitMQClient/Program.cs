using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQClient;
using RabbitMQClient.DbContextRabbit;
using Serilog;
using Serilog.Events;

    var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(configuration)
        .CreateLogger();

    var service = new ServiceCollection();
    service.AddSingleton<IRabbitClient, RabbitClient>();
    service.AddDbContext<DbContextRabbitClient>(options => options
        .UseSqlServer(configuration.GetConnectionString("Baza")));

    var serviceProvider = service.BuildServiceProvider();
    var app = serviceProvider.GetService<IRabbitClient>();

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

