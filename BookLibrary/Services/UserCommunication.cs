using BookLibrary.DataProviders;
using BookLibrary.Entities;
using BookLibrary.Repositories;
using BookLibrary.Repositories.Extensions;
using System.Text.Json;

namespace BookLibrary.Services;

public class UserCommunication : IUserCommunication
{
    string auditFileName = "Audit_DataLog.txt";

    private readonly IRepository<Book> _booksRepository;
    private readonly IBooksProvider _booksProvider;

    public UserCommunication(
        IRepository<Book> bookRepository,
        IBooksProvider booksProvider)
    {
        _booksRepository = bookRepository;
        _booksProvider = booksProvider;
    }

    public void CommunicationWithUser()
    {
        _booksRepository.ItemAdded += BookRepositoryOnItemAdded;
        _booksRepository.ItemRemoved += BookRepositoryOnItemRemoved;

        void BookRepositoryOnItemAdded(object? sender, Book e)
        {
            SaveFile($"[{DateTime.Now}] - BookAdded - [{e.Title}]", auditFileName);
        }

        void BookRepositoryOnItemRemoved(object? sender, Book e)
        {
            SaveFile($"[{DateTime.Now}] - BookDeleted - [{e.Title}]", auditFileName);
        }

        SaveFile($"[{DateTime.Now}] - Start logging in!", auditFileName);



        var books = GenerateSampleBooks();
        foreach (var book in books)
        {
            _booksRepository.Add(book);
        }

        Console.WriteLine("Welcome to the BookLibrary program.The program stores information about books.");
        Console.WriteLine("====================================================================");
        Console.WriteLine();

        bool isCloseApp = false;
        while (!isCloseApp)
        {
            Console.WriteLine("1 - Add new book to the list.");
            Console.WriteLine("2 - View all books.");
            Console.WriteLine("3 - Remove the book.");
            Console.WriteLine("4 - Find book by id.");
            Console.WriteLine("5 - Find book where author is.");
            Console.WriteLine("6 - Take Books.");
            Console.WriteLine("7 - Get unique <Categories>");
            Console.WriteLine("Q - Close app.");

            var userInput = GetFromUser("Chose key: ").ToUpper();

            switch (userInput)
            {
                case "1":
                    UserAddBook(_booksRepository);
                    break;

                case "2":
                    WriteAllToConsole(_booksRepository);
                    break;

                case "3":
                    var id = int.Parse(GetFromUser("Enter the id of the book to be deleted: "));
                    DeleteBook(id, _booksRepository);
                    break;
                case "4":
                    FindBookByID();
                    break;
                case "5":
                    FindBookWhereAuthorIs();
                    break;
                case "6":
                    TakeBooks();
                    break;
                case "7":
                    GetUniqueCategories();
                    break;
                case "Q":
                    isCloseApp = true;                  
                    break;

                default:
                    Console.WriteLine("Invalid operation.");
                    continue;
            }
        }
    }

    private void GetUniqueCategories()
    {
        Console.WriteLine("Books Categories");
        var categories = _booksProvider.GetUniqueCategory();
        foreach (var category in categories)
        {
            Console.WriteLine(category);
        }
    }

    private void TakeBooks()
    {
        var userInput = GetFromUser("Enter the number of books to display (intiger): ");
        if(int.TryParse(userInput, out int value))
        {
            foreach (var book in _booksProvider.TakeBooks(value))
            {
                Console.WriteLine(book);
            }
        }
        else
        {
            Console.WriteLine("The input value is not a intiger number!");
        }
    }

    private void FindBookWhereAuthorIs()
    {
        var userInput = GetFromUser("Enter the Author of books to display: ");
        var bookList = _booksProvider.WhereAuthorIs(userInput);

        if(bookList != null)
        {
            foreach (var book in bookList)
            {
                Console.WriteLine(book);
            }
        }
        else
        {
            Console.WriteLine("There is no book by this author in the library!");
        }       
    }

    private void FindBookByID()
    {
        var userInput = GetFromUser("Enter the number of books to display (intiger): ");
        if (int.TryParse(userInput, out int value))
        {
            try
            {
                Console.WriteLine(_booksProvider.SingleById(value));
            }
            catch
            {
                Console.WriteLine("No book for given id!");
            }        
      
        }
        else
        {
            Console.WriteLine("The input value is not a intiger number!");
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

    static void UserAddBook(IRepository<Book> bookRepository)
    {

        var book = new Book();
        book.Title = GetFromUser("Enter the title of the book: ");
        book.Author = GetFromUser("Enter the authors of the book: ");
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

    static void DeleteBook(int id, IRepository<Book> bookRepository)
    {
        var book = bookRepository.GetById(id);
        if (book != null)
        {
            bookRepository.Remove(book);
            bookRepository.Save();
        }
    }

    static void WriteAllToConsole(IReadRepository<IEntity> repository)
    {
        var items = repository.GetAll();
        foreach (var item in items)
        {
            Console.WriteLine(item);
        }
    }
    public static List<Book> GenerateSampleBooks()
    {
        return new List<Book>
        {
            new Book
            {
                Id = 1,
                Title = "Gra o tron",
                Author = "George R.R. Martin",
                RelaseData = new DateTime(1996,6,26),
                Category = Category.Fantasy,
                IsAvailable = false,
            },
            new Book
            {
                Id = 2,
                Title = "Władca Pierścieni",
                Author = "J.R.R. Tolkien",
                RelaseData = new DateTime(1954, 7, 29),
                Category = Category.Fantasy,
                IsAvailable = true
            },
            new Book
            {
                Id = 3,
                Title = "Dziennik",
                Author = "Nicholas Sparks",
                RelaseData = new DateTime(1996, 10, 1),
                Category = Category.Romance,
                IsAvailable = true
            },
            new Book
            {
                Id = 4,
                Title = "Kod Leonarda da Vinci",
                Author = "Dan Brown",
                RelaseData = new DateTime(2003, 3, 18),
                Category = Category.Mystery,
                IsAvailable = true
            },
            new Book
            {
                Id = 5,
                Title = "Krzyżacy",
                Author = "Henryk Sienkiewicz",
                RelaseData = new DateTime(1900, 1, 1),
                Category = Category.History,
                IsAvailable = false
            },
            new Book
            {
                Id = 6,
                Title = "Życie PI",
                Author = "Yann Martel",
                RelaseData = new DateTime(2001, 9, 11),
                Category = Category.Adventure,
                IsAvailable = true
            },
            new Book
            {
                Id = 7,
                Title = "Dziewczyna z pociągu",
                Author = "Paula Hawkins",
                RelaseData = new DateTime(2015, 1, 13),
                Category = Category.Mystery,
                IsAvailable = true
            },
            new Book
            {
                Id = 8,
                Title = "Harry Potter i Kamień Filozoficzny",
                Author = "J.K. Rowling",
                RelaseData = new DateTime(1997, 6, 26),
                Category = Category.Fantasy,
                IsAvailable = true
            },
            new Book
            {
                Id = 9,
                Title = "Harry Potter i Komnata Tajemnic",
                Author = "J.K. Rowling",
                RelaseData = new DateTime(1998, 7, 2),
                Category = Category.Fantasy,
                IsAvailable = true
            },
            new Book
            {
                Id = 10,
                Title = "Harry Potter i więzień Azkabanu",
                Author = "J.K. Rowling",
                RelaseData = new DateTime(1999, 7, 8),
                Category = Category.Fantasy,
                IsAvailable = true
            },
            new Book
            {
                Id = 11,
                Title = "Harry Potter i Czara Ognia",
                Author = "J.K. Rowling",
                RelaseData = new DateTime(2000, 7, 8),
                Category = Category.Fantasy,
                IsAvailable = true
            },
            new Book
            {
                Id = 12,
                Title = "Harry Potter i Zakon Feniksa",
                Author = "J.K. Rowling",
                RelaseData = new DateTime(2003, 6, 21),
                Category = Category.Fantasy,
                IsAvailable = true
            },
            new Book
            {
                Id = 13,
                Title = "Harry Potter i Książę Półkrwi",
                Author = "J.K. Rowling",
                RelaseData = new DateTime(2005, 7, 16),
                Category = Category.Fantasy,
                IsAvailable = true
            },
            new Book
            {
                Id = 14,
                Title = "Harry Potter i Insygnia Śmierci",
                Author = "J.K. Rowling",
                RelaseData = new DateTime(2007, 7, 21),
                Category = Category.Fantasy,
                IsAvailable = true
            }
        };
    }

    static void SaveFile(string message, string fileName)
    {
        using (var writter = File.AppendText(fileName))
        {
            writter.WriteLine(message);
        }
    }


    
    //string bookFileName = "BooksList.json";
    //AddBook(bookRepository, bookFileName);

    //static void SaveBooksInFile(string fileName, IReadRepository<Book> repository)
    //{
    //    var items = repository.GetAll();
    //    List<Book> books = new List<Book>(items);

    //    var objectsSerialized = JsonSerializer.Serialize<IEnumerable<Book>>(books);
    //    File.WriteAllText(fileName, objectsSerialized);
    //}
    //static List<Book> ReadBooksFromFile(string fileName)
    //{
    //    List<Book> books = new List<Book>();

    //    if (File.Exists(fileName))
    //    {
    //        var jsonContent = File.ReadAllText(fileName);
    //        books = JsonSerializer.Deserialize<IEnumerable<Book>>(jsonContent)?.ToList();
    //    }
    //    else
    //    {
    //        Console.WriteLine("File not found.");
    //    }

    //    return books;
    //}

    //static void AddBook(IRepository<Book> bookRepository, string fileName)
    //{
    //    Book[] book = ReadBooksFromFile(fileName).ToArray();
    //    bookRepository.AddBatch(book);
    //}
}
