using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQClient;
using RabbitMQClient.DbContextRabbit;
using Microsoft.Extensions.Configuration.Json;
using System.Configuration;
using System;

    var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

    var service = new ServiceCollection();
    service.AddSingleton<IRabbitClient, RabbitClient>();

    service.AddDbContext<DbContextRabbitClient>(options => options
            .UseSqlServer(configuration.GetConnectionString("Baza")));

    var serviceProvider = service.BuildServiceProvider();
    var app = serviceProvider.GetService<IRabbitClient>();
   
    app.Run();
    


