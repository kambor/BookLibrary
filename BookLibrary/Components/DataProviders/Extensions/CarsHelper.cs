using BookLibrary.Entities;

namespace BookLibrary.DataProviders.Extensions;

public static class CarsHelper
{
    public static IEnumerable<Car> ByColor(this IEnumerable<Car> querry, string color)
    {
        return querry.Where(x => x.Color == color);
    }
}
