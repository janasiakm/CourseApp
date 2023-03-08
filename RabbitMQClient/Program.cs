using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQClient;
using RabbitMQClient.DbContextRabbit;



var service = new ServiceCollection();
    service.AddSingleton<IRabbitClient, RabbitClient>();
    service.AddDbContext<DbContextRabbitClient>(options => options
        .UseSqlServer("Data Source =.\\SQLEXPRESS; Initial Catalog = TEST2; Integrated Security = True"));

var serviceProvider = service.BuildServiceProvider();
var app = serviceProvider.GetService<IRabbitClient>();
app.Run();
