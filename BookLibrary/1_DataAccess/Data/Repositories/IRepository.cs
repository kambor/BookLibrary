using BookLibrary.DataAccess.Data.Entities;

namespace BookLibrary.DataAccess.Data.Repositories;

public interface IRepository <T> : IWriteRepository<T>, IReadRepository<T> where T : class, IEntity
{
    public const string auditFileName = "AuditDataLog.txt";
    event EventHandler<T>? ItemAdded;
    event EventHandler<T>? ItemRemoved;
}
