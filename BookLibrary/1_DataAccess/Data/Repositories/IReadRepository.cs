using BookLibrary.DataAccess.Data.Entities;

namespace BookLibrary.DataAccess.Data.Repositories;

public interface IReadRepository<out T> where T : class, IEntity
{
    IEnumerable<T> GetAll();
    T? GetById(int id);
}
