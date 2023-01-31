using FirstCourseApp.Entities;
using FirstCourseApp.Repositories;
using FirstCourseApp.Data;
using Microsoft.EntityFrameworkCore;
using FirstCourseApp.Repositories.Interface;
using FirstCourseApp.CsvReader;
using FirstCourseApp.CsvReader.Extensions;


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

                                case "0":
                                    break;

                                default:
                                    Console.WriteLine("Niewłasciwe polecenie!");
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

        private string Menu()
        {
            Console.WriteLine("Wybierz co chcesz zrobić: ");
            Console.WriteLine(" 1 - Edycja Klient");
            Console.WriteLine(" 2 - Edycja Pracownik");
            Console.WriteLine(" 0 - Zakończ program");
            Console.Write("Twój wybór : ");
            return Console.ReadLine();
        }

        public void Write()
        {
            var result = _context.Clients.ToList();
            Console.WriteLine("Lista klientów: ");
            foreach (var item in result)
            {
                Console.WriteLine(item.ToString());
            }

        }

        private void ImportClientCsv()
        {
            var client = _csvReader.ProcessClient("Files//plik.csv");
            foreach (var item in client)
            {
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
            Console.WriteLine("Zaimportowano klientow z pliku! ");
        }
        public void ReadClientData()
        {
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
                Console.WriteLine("Wybrany kilent to:");
                Console.WriteLine(result.ToString());
                Console.Write("Czy na pewno chcesz usunąć klienta (Y/N) : ");
                var accept = Console.ReadLine();
                if (accept.ToUpper() == "Y")
                {
                    _context.Clients.Remove(result);
                    Console.WriteLine("Klient został usunięty !");
                    _context.SaveChanges();
                }
                else
                    Console.WriteLine("Klient nie został usunięty.");
            }
            else Console.WriteLine("Klient nie został usunięty.");

        }

        public string menuClient()
        {
            Console.WriteLine(" 1 - Dodaj nowego klienta");
            Console.WriteLine(" 2 - Wyświetl Liste klientów");
            Console.WriteLine(" 3 - Usuń klienta");
            Console.WriteLine(" 4 - Import klientów z pliku");
            Console.WriteLine(" 0 - WYJDZ");
            Console.Write("Twój wybór : ");
            var wybor2 = Console.ReadLine();

            return wybor2.ToUpper();

        }

    }
}
