using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstCourseApp.Entities
{
    public class Employee : BaseEntity
    {

        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Type { get; set; }
        public bool IsActive { get; set; }

        public override string ToString() => $" " +
            $"ID: {Id}, " +
            $"Imie: {FirstName}, " +
            $"Nazwisko: {LastName}, " +
            $"Type: {Type}";

    }
}
