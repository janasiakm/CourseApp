using FirstCourseApp.CsvReader;
using FirstCourseApp.CsvReader.Extensions;

namespace FirstCourseApp.CsvReader;

    public class CsvReader : ICsvReader
     
    {
        public List<ClassModel> ProcessClient(string filePath)
        {
            if (!File.Exists(filePath))
            {
            return new List<ClassModel>();
            }     
        
            var clients = 
                File.ReadAllLines(filePath)
                .Where(x => x.Length > 1)
                .ToClient();

            return clients.ToList();
        }
    }


