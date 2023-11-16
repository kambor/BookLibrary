using BookLibrary.DataAccess.Data.Entities;

namespace BookLibrary.ApplicationServices.Components.DataProviders;

public interface IBooksProvider
{
    List<string> GetUniqueAuthors();
    List<Book> OrderByRating();
    List<Book> WhereAuthorIs(string author);
}
