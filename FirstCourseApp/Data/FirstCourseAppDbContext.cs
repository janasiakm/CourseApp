using Microsoft.EntityFrameworkCore;
using FirstCourseApp.Entities;

namespace FirstCourseApp.Data;

    public class FirstCourseAppDbContext : DbContext
    {
        public DbSet<Client> Clients => Set<Client>();
        public DbSet<Employee> Employees => Set<Employee>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
             base.OnConfiguring(optionsBuilder);
             optionsBuilder.UseInMemoryDatabase("StorageAppDb");
         }


    }
