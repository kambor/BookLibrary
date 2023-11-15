using BookLibrary.Entities;

namespace BookLibrary.Components.XmlReader;

public interface IXmlReader
{
    public void CreateXml(List<Book> recordsBook);
    public void QueryXml(string fileName);
}
