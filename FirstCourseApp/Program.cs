using FirstCourseApp;
using FirstCourseApp.CsvReader;
using FirstCourseApp.Data;
using FirstCourseApp.Entities;
using FirstCourseApp.Repositories;
using FirstCourseApp.Repositories.Interface;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddSingleton<IApp,App>();
services.AddSingleton<IRepository<Client>,ListRepository<Client>>();
services.AddSingleton<ICsvReader,CsvReader>();

var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetService<IApp>();

app.Run();



