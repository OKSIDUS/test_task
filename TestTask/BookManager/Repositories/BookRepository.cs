using BookManager.Interfaces;
using BookManager.Models;
using System.IO;
using System.Xml.Serialization;

namespace BookManager.Repositories
{
    public class BookRepository : IBookRepository
    {    private static readonly XmlSerializer Serializer = new(typeof(BookCollection));

        public List<Book> Load(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("File path must not be empty.", nameof(filePath));
            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("XML file not found.", filePath);

            }

            using var stream = File.OpenRead(filePath);
            var collection = (BookCollection?)Serializer.Deserialize(stream);

            return collection?.Items ?? new List<Book>();

        }

        public void Save(List<Book> books, string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("File path must not be empty.", nameof(filePath));
            }

            if (books == null)
            {
                throw new ArgumentNullException(nameof(books), "Books collection must not be null.");
            }

            var collection = new BookCollection { Items = books };

            using var stream = File.Create(filePath);
            Serializer.Serialize(stream, collection);

        }
    }
}
