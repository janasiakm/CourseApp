using FirstCourseApp.Entities;
using FirstCourseApp.Repositories;
using FirstCourseApp.Data;
using Microsoft.EntityFrameworkCore;
using FirstCourseApp.Repositories.Interface;
using FirstCourseApp.CsvReader;
using FirstCourseApp.CsvReader.Extensions;
using RabbitMQ.Client;
using System.Text;
using RabbitMQ.Client.Events;
using Serilog;

namespace FirstCourseApp
{
    public class App: IApp
    {
        //private readonly IRepository<Client> _sqlRepository; 
        private readonly ICsvReader _csvReader; 
        private readonly FirstCourseAppDbContext _context;
        public App(ICsvReader CsvReader, FirstCourseAppDbContext context)
        {
            _context = context;
            _csvReader = CsvReader;
            _context.Database.EnsureCreated();
        }
           
        

        public void Run()
        {
            string action = "";
            string wybor2 = "";

            while (action != "0")
            {
                action = Menu();
                wybor2 = "";
                switch (action)
                {
                    case "1":
                        while (wybor2 != "0")
                        {
                            wybor2 = menuClient();
                            switch (wybor2)
                            {
                                case "1":
                                    ReadClientData();
                                    break;

                                case "2":
                                    Write();
                                    break;

                                case "3":                                                                    
                                    Write();
                                    DeleteClient();
                                    break;

                                case "4":
                                    ImportClientCsv();
                                    break;

                                case "5":
                                    RabbitMQMetod();
                                    break;

                                case "6":
                                    RabbitMQDownload();
                                    break;

                                case "0":
                                    break;

                                default:
                                    Console.WriteLine("Niewłasciwe polecenie!!");
                                    break;
                            }

                        }
                        break;
                    case "2":


                        break;

                    case "0":

                        break;

                    default:
                        Console.WriteLine("Niewłaściwe polecenie");
                        break;

                }

            }

            Console.WriteLine("Dziękuje za korzystanie z apliakcji.");
                        
        }

        private void RabbitMQDownload()
        {
            var factory2 = new ConnectionFactory() { HostName = "Localhost" };
            using (var connection2 = factory2.CreateConnection())
            using (var channel2 = connection2.CreateModel())
            {
                channel2.QueueDeclare(queue: "FirstApp",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var download1 = new EventingBasicConsumer(channel2);
                download1.Received += (model, ea) =>
                {
                    var message2 = Encoding.UTF8.GetString(ea.Body.ToArray());
                    Console.WriteLine("Odebrano: " + message2.ToString());
                    
                    //Console.WriteLine(message2.ToString());

                    var columns = message2.Split(',');
                    _context.Clients.Add(new Client
                    {
                        FirstName = columns[1],
                        LastName = columns[2],
                        Adres = columns[3],
                        PhoneNumber = columns[4]

                    });
                    _context.SaveChanges();

                    //channel2.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                    //Console.Write("stop ");
                };

                

                channel2.BasicConsume(queue: "FirstApp",
                    autoAck: true,
                    consumer: download1);
                
            }

        }

        private void RabbitMQMetod()
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

                Write();
                bool stop = true;
                do
                {
                    Console.Write("Podaj id użytkownika: ");
                    string id = Console.ReadLine();
                    var client = _context.Clients.FirstOrDefault(x => x.Id == Int32.Parse(id));
                    if (client != null)
                    {
                        stop = false;
                        var message = Encoding.UTF8.GetBytes(
                         client.Id + "," +
                         client.FirstName + "," +
                         client.LastName + "," +
                         client.Adres + "," +
                         client.PhoneNumber);

                        channel.BasicPublish(exchange: "",
                           routingKey: "FirstApp",
                           basicProperties: null, message);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Brak użytkonika o podanym ID !!! ");
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                } while (stop);
            }

        }

        private string Menu()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Wybierz co chcesz zrobić: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" 1 - Edycja Klient");
            Console.WriteLine(" 2 - Edycja Pracownik");
            Console.WriteLine(" 0 - Zakończ program");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Twój wybór : ");
            return Console.ReadLine();
        }

        public void Write()
        {
            Log.Information("Lista Klientów");
            var result = _context.Clients.ToList();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Lista klientów: ");
            Console.ForegroundColor = ConsoleColor.White;
            foreach (var item in result)
            {
                Console.WriteLine(item.ToString());
            }

        }

        private void ImportClientCsv()
        {
            Log.Information("Import z Pliku CSV");
            var client = _csvReader.ProcessClient("Files//plik.csv");
            foreach (var item in client)
            {
                Log.Information("Import klienta");
                _context.Clients.Add(new Client
                {
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Adres = item.Adres,
                    PhoneNumber = item.PhoneNumber

                });
                
            }
            _context.SaveChanges();
            //_sqlRepository.Save();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Zaimportowano klientow z pliku! ");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public void ReadClientData()
        {
            Console.ForegroundColor = ConsoleColor.White;
            string firstname = "";
            string lastname = "";
            string adres = "";
            string  phonenumber = "";

             Console.Write("Podaj Imie: ");
            firstname = Console.ReadLine();
            Console.Write("Podaj Nazwisko: ");
            lastname = Console.ReadLine();
            Console.Write("Podaj Adres: ");
            adres = Console.ReadLine();
            Console.Write("Podaj Numer telefonu: ");
            phonenumber = Console.ReadLine();

            _context.Clients.Add(new Client
            {
                FirstName = firstname,
                LastName = lastname,
                Adres = adres,
                PhoneNumber = phonenumber
            });
            Log.Information("Wprowadzenie nowego klienta");
            _context.SaveChanges();
            // _sqlRepository.Save();

        }

        public void DeleteClient()
        {
            Console.Write("Podaj id klienta ktorego chcesz usunąć: ");
            string id = Console.ReadLine();
           
            var result = _context.Clients.FirstOrDefault(x => x.Id == Int32.Parse(id));
            if (result is not null)
            {
                Console.Write("Wybrany kilent to:  ");
                Console.WriteLine(result.ToString());

                Console.Write("Czy na pewno chcesz usunąć klienta (Y/N) : ");
                var accept = Console.ReadLine();
                if (accept.ToUpper() == "Y")
                {
                    Log.Information("Usunięcie klienta: "+ result.ToString());
                    _context.Clients.Remove(result);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Klient został usunięty !");
                    Console.ForegroundColor = ConsoleColor.White;
                    _context.SaveChanges();
                    Log.Information("Usunięcie klienta: ");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Klient nie został usunięty.");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            else
            { 
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Klient nie został usunięty.");
                Console.ForegroundColor = ConsoleColor.White; 
            }

        }

        public string menuClient()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("MENU KLIENTA: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" 1 - Dodaj nowego klienta");
            Console.WriteLine(" 2 - Wyświetl Liste klientów");
            Console.WriteLine(" 3 - Usuń klienta");
            Console.WriteLine(" 4 - Import klientów z pliku");
            Console.WriteLine(" 5 - Wyslij użytkownika na kolejke Rabbit");
            Console.WriteLine(" 6 - Pobierz użytkowników z kolejki Rabbit i zapisz do bazy");
            Console.WriteLine(" 0 - WYJDZ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Twój wybór : ");
            var wybor2 = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            
            return wybor2.ToUpper();

        }

    }
}
