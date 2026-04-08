using BookManager.Models;

namespace BookManager.Interfaces
{
    public interface IBookRepository
    {
        List<Book> Load(string filePath);
        void Save(List<Book> books, string filePath);
    }
}
