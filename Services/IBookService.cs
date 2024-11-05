using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IBookService
    {
        public List<Book> GetAllBook();
        public void ChangeAvailable(Book book);
        public int AddBook(Book book);
        public int UpdateBook(Book book);
        public Book? GetBookById(int id);
    }
}
