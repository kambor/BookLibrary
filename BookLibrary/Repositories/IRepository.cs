using BookLibrary.Entities;

namespace BookLibrary.Repositories;

public interface IRepository <T> : IWriteRepository<T>, IReadRepository<T> where T : class, IEntity
{
}
