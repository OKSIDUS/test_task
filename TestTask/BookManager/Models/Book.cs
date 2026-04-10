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
        public Book(string title, string author, int pages)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Author = author ?? throw new ArgumentNullException(nameof(author));

            if (pages <= 0)
                throw new ArgumentOutOfRangeException(nameof(pages), "Page count must be positive.");

            Pages = pages;
        }
    }
}
