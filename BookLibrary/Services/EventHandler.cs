using BookLibrary.Entities;
using BookLibrary.Repositories;

namespace BookLibrary.Services;

public class EventHandler : IEventHandler
{
    private readonly IRepository<Book> _bookRepository;

    public EventHandler(IRepository<Book> bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public void SubscribeToEvents()
    {
        _bookRepository.ItemAdded += BookRepositoryOnItemAdded;
        _bookRepository.ItemRemoved += BookRepositoryOnItemRemoved;
    }

    void BookRepositoryOnItemAdded(object? sender, Book e)
    {
        SaveFile($"[{DateTime.Now}] - BookAdded - [{e.Title}]", (IRepository<IEntity>.auditFileName));
    }

    void BookRepositoryOnItemRemoved(object? sender, Book e)
    {
        SaveFile($"[{DateTime.Now}] - BookDeleted - [{e.Title}]", (IRepository<IEntity>.auditFileName));
    }

    private void SaveFile(string message, string fileName)
    {
        using (var writter = File.AppendText(fileName))
        {
            writter.WriteLine(message);
        }
    }
}
