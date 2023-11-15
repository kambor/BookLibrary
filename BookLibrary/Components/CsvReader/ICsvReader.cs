using BookLibrary.Entities;

namespace BookLibrary.Components.CsvReader;

public interface ICsvReader
{
    List<Book> ProcessBooks(string filePath);
}
