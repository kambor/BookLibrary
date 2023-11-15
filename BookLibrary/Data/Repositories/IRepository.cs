using BookLibrary.Entities;

namespace BookLibrary.Repositories;

public interface IRepository <T> : IWriteRepository<T>, IReadRepository<T> where T : class, IEntity
{
    public const string auditFileName = "AuditDataLog.txt";
    event EventHandler<T>? ItemAdded;
    event EventHandler<T>? ItemRemoved;
}
