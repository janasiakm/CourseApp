using FirstCourseApp.CsvReader;
using FirstCourseApp.CsvReader.Extensions;

namespace FirstCourseApp.CsvReader;

    public class CsvReader : ICsvReader
     
    {
        public List<ClientCsv> ProcessClient(string filePath)
        {
            if (!File.Exists(filePath))
            {
            return new List<ClientCsv>();
            }     
        
            var clients = 
                File.ReadAllLines(filePath)
                .Where(x => x.Length > 1)
                .ToClient();

            return clients.ToList();
        }

    public List<EmployeeCsv> ProcessEmployee(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return new List<EmployeeCsv>();
        }

        var employees =
            File.ReadAllLines(filePath)
            .Skip(1)
            .Where(x => x.Length > 1)
            .ToEmployee();

        return employees.ToList();
    }
}


