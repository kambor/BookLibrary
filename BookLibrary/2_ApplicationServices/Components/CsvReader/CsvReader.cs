using BookLibrary.ApplicationServices.Components.CsvReader.Extensions;
using BookLibrary.DataAccess.Data.Entities;

namespace BookLibrary.ApplicationServices.Components.CsvReader;

public class CsvReader : ICsvReader
{
    public List<Book> ProcessBooks(string filePath)
    {
        if(!File.Exists(filePath))
        {
            return new List<Book>();
        }

        var books = File.ReadAllLines(filePath)
            .Skip(1)
            .Where(x => x.Length > 1)
            .ToBook();

        return books.ToList();
    }
}
