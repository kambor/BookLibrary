using BookLibrary.Components.CsvReader.Models;
using System.Xml.Linq;

namespace BookLibrary.Components.XmlReader;

public class XmlReader : IXmlReader
{
    public void CreateXml(List<Car> recordsCar, List<Manufacturer> recordsManufacturer)
    {     
        var document = new XDocument();
        var cars = new XElement("Manufacturers", recordsManufacturer
            .Select(m =>
            new XElement("Manufacturer",
                new XAttribute("Name", m.Name),
                new XAttribute("Country", m.Country),
                recordsCar
                .Where(c => c.Manufacturer == m.Name)
                .GroupBy(c => c.Manufacturer)
                .Select(x =>
                new XElement("Cars",
                    new XAttribute("country", m.Country),
                    new XAttribute("CombinedSum", x.Sum(c => c.Combined)),
                    x
                    .Select(c =>
                    new XElement("Car",
                        new XAttribute("Model", c.Name),
                        new XAttribute("Combined", c.Combined)
                   )))))));

        document.Add(cars);
        document.Save("CombinedSumInCountry.xml");
    }

    public void QueryXml(string fileName)
    {
        var document = XDocument.Load(fileName);
        var names = document
            .Element("Cars")?
            .Elements("Car")
            .Where(x => x.Attribute("Manufacturer")?.Value == "BMW")
            .Select(x => x.Attribute("Name")?.Value);

        foreach (var name in names)
        {
            Console.WriteLine(name);
        }
    }
}
