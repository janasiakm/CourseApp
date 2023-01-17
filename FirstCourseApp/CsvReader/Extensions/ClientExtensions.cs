
namespace FirstCourseApp.CsvReader.Extensions;

using FirstCourseApp.CsvReader;

public static class ClientExtensions
{
    public static IEnumerable<ClientCsv> ToClient(this IEnumerable<string> source)
    {
        foreach (var line in source)
        {
            var columns = line.Split(',');

            yield return new ClientCsv
            {
                FirstName = columns[0],
                LastName = columns[1],
                Adres = columns[2],
                PhoneNumber = columns[3]

            };

        }

    }

    public static IEnumerable<EmployeeCsv> ToEmployee(this IEnumerable<string> source)
    {
        foreach (var line in source)
        {
            var columns = line.Split(',');

            yield return new EmployeeCsv
            {
                FirstName = columns[0],
                LastName = columns[1],
                Adres = columns[2],
                PhoneNumber = columns[3]

            };

        }

    }
}



