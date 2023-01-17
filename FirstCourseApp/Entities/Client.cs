using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstCourseApp.Entities
{
    public class Client : BaseEntity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Adres { get; set; }
        public string? PhoneNumber { get; set; }

        public override string ToString() => $" " +
            $"ID: {Id}, " +
            $"Imie: {FirstName}, " +
            $"Nazwisko: {LastName}, " +
            $"Adres: {Adres}, " +
            $"Tel: : {PhoneNumber}, "+
            $"Utworzono: {Created}" ;



    }
}
