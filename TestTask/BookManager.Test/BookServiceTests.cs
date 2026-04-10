using BookManager.Models;
using BookManager.Repositories;
using BookManager.Services;

namespace BookManager.Test
{
    public class BookServiceTests
    {
        private readonly BookService _service;

        public BookServiceTests()
        {
            _service = new BookService(new BookRepository());
        }

        [Fact]
        public void Add_SingleBook_AppearsInList()
        {
            var book = new Book("The Shining", "King", 447);

            _service.Add(book);

            Assert.Single(_service.Books);
            Assert.Equal("The Shining", _service.Books[0].Title);
        }

        [Fact]
        public void Add_NullBook_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _service.Add(null!));
        }

        [Fact]
        public void Sort_ByAuthorThenByTitle()
        {
            _service.Add(new Book("It", "King", 1138));
            _service.Add(new Book("The Ugly Duckling", "Andersen", 32));
            _service.Add(new Book("The Little Mermaid", "Andersen", 40));
            _service.Add(new Book("The Shining", "King", 447));

            _service.Sort();

            var titles = _service.Books.Select(b => b.Title).ToList();

            Assert.Equal(new[]
            {
            "The Little Mermaid",  
            "The Ugly Duckling",    
            "It",                   
            "The Shining"           
        }, titles);
        }

        [Fact]
        public void Sort_EmptyList_DoesNotThrow()
        {
            var ex = Record.Exception(() => _service.Sort());
            Assert.Null(ex);
        }

        [Fact]
        public void Sort_IsCaseInsensitive()
        {
            _service.Add(new Book("beta", "Author", 100));
            _service.Add(new Book("Alpha", "Author", 100));

            _service.Sort();

            Assert.Equal("Alpha", _service.Books[0].Title);
            Assert.Equal("beta", _service.Books[1].Title);
        }

        [Fact]
        public void Search_FindsBySubstring()
        {
            _service.Add(new Book("The Shining", "King", 447));
            _service.Add(new Book("It", "King", 1138));
            _service.Add(new Book("The Little Mermaid", "Andersen", 40));

            var results = _service.Search("the");

            Assert.Equal(2, results.Count);
            Assert.Contains(results, b => b.Title == "The Shining");
            Assert.Contains(results, b => b.Title == "The Little Mermaid");
        }

        [Fact]
        public void Search_NoMatch_ReturnsEmptyList()
        {
            _service.Add(new Book("The Shining", "King", 447));

            var results = _service.Search("xyz");

            Assert.Empty(results);
        }

        [Fact]
        public void Search_EmptyQuery_ReturnsEmptyList()
        {
            _service.Add(new Book("The Shining", "King", 447));

            Assert.Empty(_service.Search(""));
            Assert.Empty(_service.Search(null!));
        }

        [Fact]
        public void Search_IsCaseInsensitive()
        {
            _service.Add(new Book("The Shining", "King", 447));

            var results = _service.Search("SHINING");

            Assert.Single(results);
        }

        [Fact]
        public void Books_ReturnsReadOnlyView()
        {
            _service.Add(new Book("Test", "Author", 100));

            Assert.IsAssignableFrom<IReadOnlyList<Book>>(_service.Books);
        }
    }
}
