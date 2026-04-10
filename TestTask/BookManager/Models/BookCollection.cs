using System.Xml.Serialization;

namespace BookManager.Models
{
    [XmlRoot("Books")]
    public class BookCollection
    {
        [XmlElement("Book")]
        public List<Book> Items { get; set; } = new();
    }
}
