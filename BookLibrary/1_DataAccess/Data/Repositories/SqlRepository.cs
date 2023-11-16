using BookLibrary.DataAccess.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.DataAccess.Data.Repositories;

public class SqlRepository<T> : IRepository<T> where T : class, IEntity, new()
{
    private readonly DbSet<T> _dbSet;
    private readonly BookLibraryDbContext _bookLibraryDbContext;

    public event EventHandler<T>? ItemAdded;
    public event EventHandler<T>? ItemRemoved;

    public SqlRepository(BookLibraryDbContext bookLibraryDbContext)
    {
        _bookLibraryDbContext = bookLibraryDbContext;
        _dbSet = _bookLibraryDbContext.Set<T>();
    }

    public IEnumerable<T> GetAll()
    {
        return _dbSet.OrderBy(item => item.Id).ToList();
    }
    public T? GetById(int id)
    {
        return _dbSet.Find(id);
    }
    public void Add(T item)
    {
        _dbSet.Add(item);
        Save();
        ItemAdded?.Invoke(this, item);
    }
    public void Remove(T item)
    {
        _dbSet.Remove(item);
        Save();
        ItemRemoved?.Invoke(this, item);
    }
    public void Save()
    {
        _bookLibraryDbContext.SaveChanges();
    }
    public void EnsureCreated()
    {
        _bookLibraryDbContext.Database.EnsureCreated();
    }
    public IEnumerable<T> Read()
    {
        return _dbSet.ToList();
    }
}
