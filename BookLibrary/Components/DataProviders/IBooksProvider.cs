using BookLibrary.Entities;
using System.Numerics;

namespace BookLibrary.DataProviders;

public interface IBooksProvider
{
    // SELECT
    List<string> GetUniqueAuthors();
    List<Book> GetSpecificColumns();
    string AnonymousClass();

    // ORDER BY
    List<Book> OrderByAuthors();
    List<Book> OrderByAuthorsAndTitleDescending();
    List<Book> OrderByRating();

    //Where
    List<Book> WhereStartsWith(string prefix);
    List<Book> WhereAuthorIs(string author);

    // FIRST, LAST, SINGLE
    Book FirstOrDefaultByAuthorWithDefault(string author);
    Book? FirstByDate();
    Book LastByAuthor(string author);
    Book SingleById(int id);
    Book? SingleOrDefaultById(int id);

    // TAKE
    List<Book> TakeBooks(int howMany);
    List<Book> TakeBooks(Range range);
    List<Book> TakeBooksWhileRealiseDataAfter(int date);

    // SKIP
    List<Book> SkipBooks(int howMany);
    List<Book> SkipBooksWhileRealiseDataAfter(int date);

    // DISTINCT
    //List<string> DistinctAllCategories();
    //List<Book> DistinctByCategory();

    // CHUNK
    List<Book[]> ChunkBooks(int size);
}
