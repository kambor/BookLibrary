using BookLibrary.Entities;
using System.Numerics;

namespace BookLibrary.DataProviders;

public interface IBooksProvider
{
    // SELECT
    List<string> GetUniqueCategory();
    List<Book> GetSpecificColumns();
    string AnonymousClass();

    // ORDER BY
    public List<Book> OrderByAuthors();
    public List<Book> OrderByAuthorsAndTitleDescending();
    public List<Book> OrderByRelaseData();

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
    List<Book> TakeBooksWhileRealiseDataAfter(DateTime date);

    // SKIP
    List<Book> SkipBooks(int howMany);
    List<Book> SkipBooksWhileRealiseDataAfter(DateTime date);

    // DISTINCT
    List<string> DistinctAllCategories();
    List<Book> DistinctByCategory();

    // CHUNK
    List<Book[]> ChunkBooks(int size);
}
