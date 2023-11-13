using BookLibrary.Components.CsvReader.Models;
using System.Globalization;

namespace BookLibrary.Components.CsvReader.Extensions;

public static class CarExtensions
{
    public static IEnumerable<Car> ToCar(this IEnumerable<string> source)
    {
        foreach(var line in source)
        {
            var columns = line.Split(',');

            //return new Car
            //{
            //    Year = int.Parse(columns[0]),
            //    Manufacturer = columns[1],
            //    Name = columns[2],
            //    Displacement = double.Parse(columns[3]),
            //    Cylinders = int.Parse(columns[4]),
            //    City = int.Parse(columns[5]),
            //    Higway = int.Parse(columns[6]),
            //    Combined = int.Parse(columns[7])
            //};
            yield return new Car
            {
                Year = int.Parse(columns[0]),
                Manufacturer = columns[1],
                Name = columns[2],
                Displacement = double.Parse(columns[3], CultureInfo.InvariantCulture),
                Cylinders = int.Parse(columns[4]),
                City = int.Parse(columns[5]),
                Higway = int.Parse(columns[6]),
                Combined = int.Parse(columns[7])
            };
        }

    }
}
