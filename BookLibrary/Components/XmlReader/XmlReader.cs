using BookLibrary.Entities;
using System.Xml.Linq;

namespace BookLibrary.Components.XmlReader;

public class XmlReader : IXmlReader
{
    public void CreateXml(List<Book> recordsBook)
    {
        var document = new XDocument();
        var books = new XElement("Books", recordsBook
            .Select(m =>
            new XElement("Book",
                new XAttribute("Title", m.Title),
                new XAttribute("Author", m.Author)          
                   )));

        document.Add(books);
        document.Save("BookData.xml");
    }

    public void QueryXml(string fileName)
    {
        var document = XDocument.Load(fileName);
        var names = document
            .Element("Books")?
            .Elements("Book")
            .Where(x => x.Attribute("Author")?.Value == "J.K. Rowling")
            .Select(x => x.Attribute("Title")?.Value);

        foreach (var name in names)
        {
            Console.WriteLine(name);
        }
    }
}
