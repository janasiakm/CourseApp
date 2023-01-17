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
        private readonly IRepository<Client> _sqlRepository; 
        private readonly ICsvReader _csvReader; 
        public App(IRepository<Client> baseRepository,ICsvReader CsvReader)
        {
            _sqlRepository = baseRepository;
            _csvReader = CsvReader;
        }
                
        public void Run()
        {
            string action = "";
            string wybor2 = "";

                        
            for (int i = 0; i < 5; i++)
            {
                _sqlRepository.Add(new Client
                {
                    FirstName = "Imie" /*+ i.ToString()*/,
                    LastName = "Nazwisko" /*+ i.ToString()*/,
                    Adres = "Dom" /*+ i.ToString()*/,
                    PhoneNumber = "0000" /*+ i.ToString()*/
                });

                _sqlRepository.Save();
            }


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

                                    var client = _csvReader.ProcessClient("Files//plik.csv");
                                    foreach (var item in client)
                                    {
                                        _sqlRepository.Add(new Client
                                        {

                                            FirstName = item.FirstName,
                                            LastName = item.LastName,
                                            Adres = item.Adres,
                                            PhoneNumber = item.PhoneNumber

                                        });
                                        
                                    }
                                    _sqlRepository.Save();
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
            return Console.ReadLine();
        }

        public void Write()
        {
            var result = _sqlRepository.GetAll();
            Console.WriteLine("Lista klientów: ");
            foreach (var item in result)
            {
               Console.WriteLine(item.ToString());
            }

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

            _sqlRepository.Add(new Client
            {
                FirstName = firstname,
                LastName = lastname,
                Adres = adres,
                PhoneNumber = phonenumber
            });

            _sqlRepository.Save();

        }

        public void DeleteClient()
        {
            Console.WriteLine("Podaj id klienta ktorego chcesz usunąć");
            string id = Console.ReadLine();
            var result = _sqlRepository.GetById(Int32.Parse(id));
            if (result is not null)
            {
                Console.WriteLine("Wybrany kilent to: /ln" + result.ToString());
                Console.WriteLine("Czy na pewno chcesz usunąć klienta (Y/N): ");
                var accept = Console.ReadLine();
                if (accept.ToUpper() == "Y")
                {
                    _sqlRepository.Remove(result);
                    Console.WriteLine("Klient został usunięty.");
                    _sqlRepository.Save();
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
            var wybor2 = Console.ReadLine();

            return wybor2.ToUpper();

        }

    }
}
