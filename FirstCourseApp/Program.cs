using FirstCourseApp;
using FirstCourseApp.CsvReader;
using FirstCourseApp.Data;
using FirstCourseApp.Entities;
using FirstCourseApp.Repositories;
using FirstCourseApp.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddSingleton<IApp,App>();
services.AddSingleton<IRepository<Client>,ListRepository<Client>>();
services.AddSingleton<ICsvReader,CsvReader>();

services.AddDbContext<FirstCourseAppDbContext>(options => options
    .UseSqlServer("Data Source =.\\SQLEXPRESS; Initial Catalog = TEST2; Integrated Security = True"));

var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetService<IApp>();

app.Run();



