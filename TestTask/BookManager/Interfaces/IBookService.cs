using BookManager.Models;

namespace BookManager.Interfaces
{
    public interface IBookService
    {
        void Load(string path);
        void Save(string path);
        void Add(Book book);
        void Sort();
        List<Book> Search(string titlePart);
    }
}
