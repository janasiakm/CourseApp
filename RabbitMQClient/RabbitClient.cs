using FirstCourseApp.Entities;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQClient.DbContextRabbit;
using System.Text;

namespace RabbitMQClient;

public class RabbitClient : IRabbitClient
{
    private readonly DbContextRabbitClient _context;
    public RabbitClient(DbContextRabbitClient context)
    {
        _context = context;
        _context.Database.EnsureCreated();
    }

    public void Run()
    {
        var factory = new ConnectionFactory() { HostName = "Localhost" };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "FirstApp",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());

                Console.WriteLine("Odebrano z kolejki:  " + message.ToString());

                var columns = message.Split(',');
                _context.Clients.Add(new Client
                {
                    FirstName = columns[1],
                    LastName = columns[2],
                    Adres = columns[3],
                    PhoneNumber = columns[4]

                });
                _context.SaveChanges();
            };

            channel.BasicConsume(queue: "FirstApp",
                autoAck: true,
                consumer: consumer);

        }

    }
}

        

