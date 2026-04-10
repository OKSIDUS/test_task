using BookManager.Interfaces;
using BookManager.Models;

namespace BookManager.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;
        private readonly List<Book> _books = new();

        public BookService(IBookRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public IReadOnlyList<Book> Books => _books.AsReadOnly();

        public void Add(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book), "Book must not be null.");
            }
            _books.Add(book);
        }

        public void Load(string path)
        {
            _books.Clear();
            var books = _repository.Load(path);
            _books.AddRange(books);
        }

        public void Save(string path)
        {
            _repository.Save(_books, path);
        }

        public List<Book> Search(string titlePart)
        {
            if (string.IsNullOrWhiteSpace(titlePart))
            {
                return new List<Book>();
            }

            return _books
                .Where(b => b.Title.Contains(titlePart, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public void Sort()
        {
            _books.Sort((a, b) =>
            {
                int authorCmp = string.Compare(a.Author, b.Author, StringComparison.OrdinalIgnoreCase);
                return authorCmp != 0
                    ? authorCmp
                    : string.Compare(a.Title, b.Title, StringComparison.OrdinalIgnoreCase);
            });
        }
    }
}
