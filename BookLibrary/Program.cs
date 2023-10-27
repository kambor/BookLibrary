using BookLibrary.Data;
using BookLibrary.Entities;
using BookLibrary.Repositories;

var bookRepository = new SqlRepository<Book>(new BookLibraryDbContext());
AddBook(bookRepository);
AddEducationalBook(bookRepository);
WriteAllToConsole(bookRepository);

static void AddBook(IRepository<Book> bookRepository)
{
    bookRepository.Add(new Book { Title = "A Game of Thrones", Authors = "George R.R. Martin", RelaseData = new DateTime(1996, 8, 1) });
    bookRepository.Add(new Book { Title = "Harry Potter i Kamień Filozoficzny", Authors = "J.K. Rowling", RelaseData = new DateTime(1997, 6, 26) });
    bookRepository.Add(new Book { Title = "Harry Potter i Insygnia Śmierci", Authors = "J.K. Rowling", RelaseData = new DateTime(2007, 7, 21) });
    bookRepository.Save();
}

static void AddEducationalBook(IWriteRepository<Educational>magazineRepository)
{
    magazineRepository.Add(new Educational { Title = "Finansowy ninja", Authors = "Michał Szafrański", RelaseData = new DateTime(2019,3,18) });
    magazineRepository.Save();
}

static void WriteAllToConsole(IReadRepository<IEntity> repository)
{
    var items = repository.GetAll();
    foreach (var item in items)
    {
        Console.WriteLine(item);
    }
}







