using BookLibrary.Components.CsvReader.Models;

namespace BookLibrary.Components.XmlReader;

public interface IXmlReader
{
    public void CreateXml(List<Car> recordsCar, List<Manufacturer> recordsManufacturer);
    public void QueryXml(string fileName);
}
