using BookLibrary.Components.CsvReader;
using BookLibrary.Components.CsvReader.Models;
using BookLibrary.Components.XmlReader;
using BookLibrary.Services;
using System.Xml.Linq;

namespace BookLibrary;

public class App : IApp
{
    private readonly IUserCommunication _userCommunication;
    private readonly ICsvReader _csvReader;
    private readonly IXmlReader _xmlReader;

    public App(IUserCommunication userCommunication, ICsvReader csvReader, IXmlReader xmlReader)
    {
        _userCommunication = userCommunication;
        _csvReader = csvReader;
        _xmlReader = xmlReader;
    }
    public void Run()
    {
        //_userCommunication.CommunicationWithUser();  
        var cars = _csvReader.ProcessCars("Resources\\Files\\fuel.csv");
        var manufacturers = _csvReader.ProcessManufacturers("Resources\\Files\\manufacturers.csv");

        _xmlReader.CreateXml(cars, manufacturers);


        //var groups = cars
        //    .GroupBy(x => x.Manufacturer)
        //    .Select(g => new
        //    {
        //        Name = g.Key,
        //        Max = g.Max(c => c.Combined),
        //        Average = g.Average(c => c.Combined)
        //    })
        //    .OrderBy(x=>x.Average);

        //foreach(var group in groups)
        //{
        //    Console.WriteLine($"{group.Name}");
        //    Console.WriteLine($"\t Max: {group.Max}");
        //    Console.WriteLine($"\t Average: {group.Average}");
        //}

        //łączenie danych z dwóch plików za pomocą kluczy (wspólnej rzeczy)
        //var carsInCountry = cars.Join(
        //    manufacturers,
        //    x => x.Manufacturer,//klucz po którym się łączymy z pliku fuel
        //    x => x.Name,//klucz po którym się łączymy
        //    (car, manufacturer) =>
        //        new
        //        {
        //            manufacturer.Country,
        //            car.Name,
        //            car.Combined
        //        }
        //    )
        //    .OrderByDescending(x => x.Combined)
        //    .ThenBy(x => x.Name);

        //foreach (var car in carsInCountry)
        //{
        //    Console.WriteLine($"Country {car.Country}");
        //    Console.WriteLine($"\t Name: {car.Name}");
        //    Console.WriteLine($"\t Combined: {car.Combined}");
        //}

        //var groups = manufacturers.GroupJoin(
        //    cars,
        //    manufacturer => manufacturer.Name,
        //    car => car.Manufacturer,
        //    (m, g) =>
        //        new
        //        {
        //            Manufacturer = m,
        //            Cars = g
        //        })
        //    .OrderBy(x => x.Manufacturer.Name);

        //foreach (var car in groups)
        //{
        //    Console.WriteLine($"Manufacturer {car.Manufacturer.Name}");
        //    Console.WriteLine($"\t Cars: {car.Cars.Count()}");
        //    Console.WriteLine($"\t Max: {car.Cars.Max(x => x.Combined)}");
        //    Console.WriteLine($"\t Min: {car.Cars.Min(x => x.Combined)}");
        //    Console.WriteLine($"\t Average: {car.Cars.Average(x => x.Combined)}");
        //    Console.WriteLine();
        //}
    }
}
