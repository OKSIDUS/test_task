using System.Xml.Serialization;

namespace BookManager.Models
{
    [XmlRoot("Book")]
    public class Book
    {
        [XmlElement("Title")]
        public string Title { get; set; } = string.Empty;
        [XmlElement("Author")]
        public string Author { get; set; } = string.Empty;
        [XmlElement("Pages")]
        public int Pages { get; set; }

        public Book() { }
    }
}
