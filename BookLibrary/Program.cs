using BookLibrary.Data;
using BookLibrary.Entities;
using BookLibrary.Repositories;
using BookLibrary.Repositories.Extensions;
using System.Text.Json;

string auditFileName = "Audit_DataLog.txt";
string bookFileName = "BooksList.json";
bool isCloseApp = false;

var bookRepository = new SqlRepository<Book>(new BookLibraryDbContext());
bookRepository.ItemAdded += BookRepositoryOnItemAdded;
bookRepository.ItemRemoved += BookRepositoryOnItemRemoved;

void BookRepositoryOnItemAdded(object? sender, Book e)
{
    //Console.WriteLine($"Book added => {e.Title} from {sender?.GetType().Name}");
    SaveFile($"[{DateTime.Now}] - BookAdded - [{e.Title}]", auditFileName);
}

void BookRepositoryOnItemRemoved(object? sender, Book e)
{
    //Console.WriteLine($"Book removed => {e.Title} from {sender?.GetType().Name}");
    SaveFile($"[{DateTime.Now}] - BookDeleted - [{e.Title}]", auditFileName);
}

SaveFile($"[{DateTime.Now}] - Start logging in!", auditFileName);
AddBook(bookRepository, bookFileName);

Console.WriteLine("Welcome to the BookLibrary program.The program stores information about books.");
Console.WriteLine("====================================================================");
Console.WriteLine();

while (!isCloseApp)
{
    Console.WriteLine("1 - Add new book to the list.");
    Console.WriteLine("2 - Load book list.");
    Console.WriteLine("3 - Delete the book.");
    Console.WriteLine("Q - Close app.");

    var userInput = GetFromUser("Chose key 1, 2, 3 or Q: ").ToUpper();

    switch (userInput)
    {
        case "1":
            UserAddBook(bookRepository);
            break;

        case "2":
            WriteAllToConsole(bookRepository);
            break;

        case "3":
            var id = int.Parse(GetFromUser("Enter the id of the book to be deleted: "));
            DeleteBook(id, bookRepository);
            break;

        case "Q":
            isCloseApp = true;
            SaveBooksInFile(bookFileName, bookRepository);
            SaveFile($"[{DateTime.Now}] - Stop program!", auditFileName);
            break;

        default:
            Console.WriteLine("Invalid operation.");
            continue;
    }
}

static string GetFromUser(string comment)
{
    Console.Write(comment);
    var userInput = Console.ReadLine();
    return userInput;
}
static int GetIntFromUser(string comment)
{
    while (true)
    {
        var input = GetFromUser(comment);
        ;
        if (int.TryParse(input, out int value))
        {
            return value;
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter int value.");
        }
    }
}

static void SaveFile(string message, string fileName)
{
    using (var writter = File.AppendText(fileName))
    {
        writter.WriteLine(message);
    }
}

static void SaveBooksInFile(string fileName, IReadRepository<Book> repository)
{
    var items = repository.GetAll();
    List<Book> books = new List<Book>(items);

    var objectsSerialized = JsonSerializer.Serialize<IEnumerable<Book>>(books);
    File.WriteAllText(fileName, objectsSerialized);
}

static void UserAddBook(IRepository<Book> bookRepository)
{

    var book = new Book();
    book.Title = GetFromUser("Enter the title of the book: ");
    book.Authors = GetFromUser("Enter the authors of the book: ");
    string categoryInput = GetFromUser("Enter the category of the book: ");
    Category category; ;
    if (Enum.TryParse(categoryInput, out category))
    {
        book.Category = category;
    }
    var year = GetIntFromUser("Enter the year of relase of the book: ");
    var month = GetIntFromUser("Enter the month of relase of the book: ");
    var day = GetIntFromUser("Enter the day of relase of the book: ");
    book.RelaseData = new DateTime(year, month, day);

    bookRepository.Add(book);
    bookRepository.Save();
}

static List<Book> ReadBooksFromFile(string fileName)
{
    List<Book> books = new List<Book>();

    if (File.Exists(fileName))
    {
        var jsonContent = File.ReadAllText(fileName);
        books = JsonSerializer.Deserialize<IEnumerable<Book>>(jsonContent)?.ToList();
    }
    else
    {
        Console.WriteLine("File not found.");
    }

    return books;
}

static void DeleteBook(int id, IRepository<Book> bookRepository)
{
    var book = bookRepository.GetById(id);
    if (book != null)
    {
        bookRepository.Remove(book);
        bookRepository.Save();
    }
}

static void AddBook(IRepository<Book> bookRepository, string fileName)
{
    Book[] book = ReadBooksFromFile(fileName).ToArray();
    bookRepository.AddBatch(book);
}

static void WriteAllToConsole(IReadRepository<IEntity> repository)
{
    var items = repository.GetAll();
    foreach (var item in items)
    {
        Console.WriteLine(item);
    }
}