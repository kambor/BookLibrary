using BookLibrary.Entities;

namespace BookLibrary.Repositories.Extensions;

public static class RepositoryExtensions
{
    public static void AddBatch<T>(this IRepository<T> repository, T[] items) //this powoduje że w każdej klasie która implementuje IRepository będziemy mogli dokleić taką metodę 
        where T : class, IEntity
    {
        foreach (var item in items)
        {
            repository.Add(item);
        }
        repository.Save();
    }
}
