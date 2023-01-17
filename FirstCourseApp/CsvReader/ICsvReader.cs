using FirstCourseApp.Entities;
using FirstCourseApp.CsvReader;

namespace FirstCourseApp.CsvReader;

    public interface ICsvReader
    {
        List<ClientCsv> ProcessClient(string filePath);
        List<EmployeeCsv> ProcessEmployee(string filePath);

    }

