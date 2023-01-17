
namespace FirstCourseApp.CsvReader.Extensions;

using FirstCourseApp.CsvReader;

    public static class ClientExtensions
    {
        public static IEnumerable<ClassModel> ToClient(this IEnumerable<string> source)
        {
            foreach (var line in source)
            {
                var columns = line.Split(',');

            yield return new ClassModel
            {
                    FirstName = columns[0],
                    LastName = columns[1],
                    Adres = columns[2],
                    PhoneNumber = columns[3]
                    
                     };

            }   
    
        }
    }

