using BookLibrary.Components.CsvReader.Extensions;
using BookLibrary.Components.CsvReader.Models;

namespace BookLibrary.Components.CsvReader;

public class CsvReader : ICsvReader
{
    public List<Car> ProcessCars(string filePath)
    {
        if(!File.Exists(filePath))
        {
            return new List<Car>();
        }

        var cars = File.ReadAllLines(filePath)
            .Skip(1)
            .Where(x => x.Length > 1)
            .ToCar();

        return cars.ToList();
    }

    public List<Manufacturer> ProcessManufacturers(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return new List<Manufacturer>();
        }

        var manufacturers = File.ReadAllLines(filePath)
           .Where(x => x.Length > 1)
           .Select(x =>
           {
               var colimns = x.Split(',');
               return new Manufacturer()
               {
                   Name = colimns[0],
                   Country = colimns[1],
                   Year = int.Parse(colimns[2])
               };
           });
        return manufacturers.ToList();
    }
}
