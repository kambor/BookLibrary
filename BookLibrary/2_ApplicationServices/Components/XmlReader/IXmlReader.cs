using BookLibrary.DataAccess.Data.Entities;

namespace BookLibrary.ApplicationServices.Components.XmlReader;

public interface IXmlReader
{
    public void CreateXml(List<Book> recordsBook);
    public void QueryXml(string fileName);
}
