using BookManager.Models;
using BookManager.Repositories;

namespace BookManager.Test
{
    public class BookRepositoryTests
    {
        private readonly string _tempFile;
        private readonly BookRepository _repository;

        public BookRepositoryTests()
        {
            _tempFile = Path.GetTempFileName();
            _repository = new BookRepository();
        }
        [Fact]
        public void Save_EmptyList_ProducesValidXml()
        {
            _repository.Save(new List<Book>(), _tempFile);
            var loaded = _repository.Load(_tempFile);

            Assert.Empty(loaded);
        }

        [Fact]
        public void Load_NonExistentFile_ThrowsFileNotFoundException()
        {
            Assert.Throws<FileNotFoundException>(() => _repository.Load("/no/such/file.xml"));
        }

        [Fact]
        public void Load_EmptyPath_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _repository.Load(""));
            Assert.Throws<ArgumentException>(() => _repository.Load("   "));
        }

        [Fact]
        public void Save_NullBooks_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _repository.Save(null!, _tempFile));
        }
        [Fact]
        public void Save_ProducesReadableXml()
        {
            var books = new List<Book> { new("Test", "Author", 100) };

            _repository.Save(books, _tempFile);

            string content = File.ReadAllText(_tempFile);
            Assert.Contains("<Title>Test</Title>", content);
            Assert.Contains("<Author>Author</Author>", content);
            Assert.Contains("<Pages>100</Pages>", content);
        }
    }
}
