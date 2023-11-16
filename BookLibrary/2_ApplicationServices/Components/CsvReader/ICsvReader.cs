using BookLibrary.DataAccess.Data.Entities;

namespace BookLibrary.ApplicationServices.Components.CsvReader;

public interface ICsvReader
{
    List<Book> ProcessBooks(string filePath);
}
