using BookManager.Interfaces;
using BookManager.Models;

namespace BookManager.Services
{
    public class BookRepository : IBookRepository
    {
        public List<Book> Load(string filePath)
        {
            throw new NotImplementedException();
        }

        public void Save(List<Book> books, string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
