using FirstCourseApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace RabbitMQClient.DbContextRabbit;

public class DbContextRabbitClient : DbContext
    {
    public DbContextRabbitClient(DbContextOptions<DbContextRabbitClient> options)
        : base(options) { }

    public DbSet<Client> Clients { get; set; }

    }

