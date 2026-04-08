using BookManager.Models;

namespace BookManager.Interfaces
{
    public interface IBookRepository
    {
        IEnumerable<Book> Load(string filePath);
        void Save(IEnumerable<Book> books, string filePath);
    }
}
