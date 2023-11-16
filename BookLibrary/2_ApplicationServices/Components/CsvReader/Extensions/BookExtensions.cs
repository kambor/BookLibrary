using BookLibrary.DataAccess.Data.Entities;
using System.Globalization;

namespace BookLibrary.ApplicationServices.Components.CsvReader.Extensions;

public static class BookExtensions
{
    public static IEnumerable<Book> ToBook(this IEnumerable<string> source)
    {
        foreach(var line in source)
        {
            var columns = line.Split(',');

            if(columns.Count() == 6)
            {
                yield return new Book
                {
                    Isbn = columns[0],
                    Author = columns[1],
                    PublicationYear = int.Parse(columns[2]),
                    Title = columns[3],
                    AverageRating = double.Parse(columns[4], CultureInfo.InvariantCulture),
                    ImageUrl = columns[5]
                };
            }
        }
    }
}
