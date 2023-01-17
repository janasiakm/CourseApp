using FirstCourseApp.Entities;
using FirstCourseApp.CsvReader;

namespace FirstCourseApp.CsvReader;

    public interface ICsvReader
    {
        List<ClassModel> ProcessClient(string filePath);

    }

