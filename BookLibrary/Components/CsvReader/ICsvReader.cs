using BookLibrary.Components.CsvReader.Models;

namespace BookLibrary.Components.CsvReader;

public interface ICsvReader
{
    List<Car> ProcessCars(string filePath);
    List<Manufacturer> ProcessManufacturers(string filePath);
}
