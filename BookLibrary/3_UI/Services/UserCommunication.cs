using BookLibrary.ApplicationServices.Components.CsvReader;
using BookLibrary.ApplicationServices.Components.DataProviders;
using BookLibrary.DataAccess.Data.Entities;
using BookLibrary.DataAccess.Data.Repositories;

namespace BookLibrary.UI.Services;


public class UserCommunication : UserComunicationBase, IUserCommunication
{
    private readonly IRepository<Book> _booksRepository;
    private readonly IBooksProvider _booksProvider;
    private readonly ICsvReader _csvReader;

    public UserCommunication(
        IRepository<Book> bookRepository,
        IBooksProvider booksProvider,
        ICsvReader csvReader)
    {
        _booksRepository = bookRepository;
        _booksProvider = booksProvider;
        _csvReader = csvReader;
    }

    public void CommunicationWithUser()
    {
        Console.WriteLine("Welcome to the BookLibrary program.The program stores information about books. Select the repository you want to use.");
        Console.WriteLine("====================================================================");
        Console.WriteLine();

        bool isCloseApp = false;
        while (!isCloseApp)
        {
            Console.WriteLine("1 - View all books.");
            Console.WriteLine("2 - Add new book.");
            Console.WriteLine("3 - Remove book.");
            Console.WriteLine("4 - Find book by id.");
            Console.WriteLine("5 - Show books ordered by rating.");
            Console.WriteLine("6 - Show unique authors.");
            Console.WriteLine("7 - Show the author's books.");
            Console.WriteLine("8 - Show books from range.");
            Console.WriteLine("9 - Import data from CSV file.");
            Console.WriteLine("Q - Close App.");

            CheckLibrary();

            var userInput = GetInputFromUser("Chose key: ").ToUpper();

            switch (userInput)
            {
                case "1":
                    ReadAllBooks();
                    break;
                case "2":
                    AddBook();
                    break;
                case "3":                  
                    DeleteBook();
                    break;
                case "4":
                    FindBookByID("Enter the number of books to display (intiger): ");
                    break;
                case "5":
                    OrderByRating();
                    break;
                case "6":
                    ShowUniqueAuthors();
                    break;
                case "7":
                    ShowBooksInsertAutor();
                    break;
                case "8":
                    ShowSomeBooks();
                    break;
                case "9":
                    ImportDataFromCsv();
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
    private void CheckLibrary()
    {
        if (!_booksRepository.GetAll().Any())
        {
            Console.WriteLine("Library is empty, you should inport data first. \n Select 9 to import data. ");
        }
    }

    private void ShowSomeBooks()
    {
        var fisrstValue = GetValueFromUser<int>("Enter the starting value of the range:  ");
        var secondValue = GetValueFromUser<int>("Enter the end value of the range:  ");

        foreach (var book in _booksProvider.TakeBooks(fisrstValue..secondValue))
        {
            Console.WriteLine(book);
        }    
    }

    private void ShowBooksInsertAutor()
    {
        var authorName = GetInputFromUser("Enter the author name: ");
        foreach (var book in _booksProvider.ShowBooksWhereAuthorIs(authorName))
        {
            Console.WriteLine(book);
        }
    }

    private void OrderByRating()
    {
        foreach (var book in _booksProvider.OrderByRating())
        {
            Console.WriteLine(book);
        }
    }

    private void ShowUniqueAuthors()
    {
        if (_booksRepository.GetAll().Any())
        {
            foreach (var author in _booksProvider.GetUniqueAuthors())
            {
                Console.WriteLine(author);
            }
        }
        else
        {
            Console.WriteLine("Library is empty");
        }

    }

    private void ImportDataFromCsv()
    {
        var records = _csvReader.ProcessBooks("Resources\\Files\\books.csv");

        foreach (var record in records)
        {
            _booksRepository.Add(record);
        }
    }

    private void ReadAllBooks()
    {
        var booksFromDb = _booksRepository.GetAll();

        foreach (var bookFromDb in booksFromDb)
        {
            Console.WriteLine($"\t{bookFromDb.Title}: {bookFromDb.Author}");
        }
    }

    private void AddBook()
    {
        var book = new Book();
        book.Isbn = GetInputFromUser("Enter the title of the book: ");
        book.Title = GetInputFromUser("Enter the title of the book: ");
        book.Author = GetInputFromUser("Enter the author of the book: ");
        book.PublicationYear = GetValueFromUser<int>("Enter the publication year of the book: ");
        book.AverageRating = GetValueFromUser<double>("Enter the average rating of the book: ");
        book.ImageUrl = GetInputFromUser("Enter the Image Url of the book: ");

        _booksRepository.Add(book);      
        _booksRepository.Save();
    }

    private Book? FindBookByID(string comment)
    {
        var userInput = GetValueFromUser<int>(comment);

        var entity = _booksRepository.GetById((int)userInput);
        if (entity != null)
        {
            Console.WriteLine(entity.ToString());
        }

        return entity;
    }
  
    private void DeleteBook()
    {
        var book = FindBookByID("Enter the id of the book to be deleted: ");  
        if(book != null)
        {
            while (true)
            {
                Console.WriteLine($"Do you really want to remove this {book.Title}?");
                var choice = GetInputFromUser("Press Y if YES or N if NO").ToUpper();
                if (choice == "Y")
                {
                    _booksRepository.Remove(book);
                    break;
                }
                else if (choice == "N")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Please choose Y or N:");
                }
            }
        }
    }

    public static List<Book> GenerateSampleBooksToListRepository()
    {
        return new List<Book>
        {
            new Book
            {
                Id = 1,
                Title = "Gra o tron",
                Author = "George R.R. Martin",
                PublicationYear = 1996
            },
            new Book
            {
                Id = 2,
                Title = "Władca Pierścieni",
                Author = "J.R.R. Tolkien",
                PublicationYear = 1954
            },
            new Book
            {
                Id = 3,
                Title = "Dziennik",
                Author = "Nicholas Sparks",
                PublicationYear = 1996
            },
            new Book
            {
                Id = 4,
                Title = "Kod Leonarda da Vinci",
                Author = "Dan Brown",
                PublicationYear = 2003
            },
            new Book
            {
                Id = 5,
                Title = "Krzyżacy",
                Author = "Henryk Sienkiewicz",
                PublicationYear = 1900
            },
            new Book
            {
                Id = 6,
                Title = "Życie PI",
                Author = "Yann Martel",
                PublicationYear = 2001
            },
            new Book
            {
                Id = 7,
                Title = "Dziewczyna z pociągu",
                Author = "Paula Hawkins",
                PublicationYear = 2015
            },
            new Book
            {
                Id = 8,
                Title = "Harry Potter i Kamień Filozoficzny",
                Author = "J.K. Rowling",
                PublicationYear = 1997
            },
            new Book
            {
                Id = 9,
                Title = "Harry Potter i Komnata Tajemnic",
                Author = "J.K. Rowling",
                PublicationYear = 1998
            },
            new Book
            {
                Id = 10,
                Title = "Harry Potter i więzień Azkabanu",
                Author = "J.K. Rowling",
                PublicationYear = 1999
            },
            new Book
            {
                Id = 11,
                Title = "Harry Potter i Czara Ognia",
                Author = "J.K. Rowling",
                PublicationYear = 2000
            },
            new Book
            {
                Id = 12,
                Title = "Harry Potter i Zakon Feniksa",
                Author = "J.K. Rowling",
                PublicationYear = 2003
            },
            new Book
            {
                Id = 13,
                Title = "Harry Potter i Książę Półkrwi",
                Author = "J.K. Rowling",
                PublicationYear = 2005
            },
            new Book
            {
                Id = 14,
                Title = "Harry Potter i Insygnia Śmierci",
                Author = "J.K. Rowling",
                PublicationYear = 2007
            }
        };
    }
}
