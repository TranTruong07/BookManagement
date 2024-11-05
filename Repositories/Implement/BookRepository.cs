using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implement
{
    public class BookRepository : IBookRepository
    {
        private readonly BookManagementContext context;
        public BookRepository()
        {
            this.context = new BookManagementContext();
        }

        public int AddBook(Book book)
        {
            context.Books.Add(book);
            return context.SaveChanges();
        }

        public void ChangeAvailable(Book book)
        {
            context.Books.Update(book);
            context.SaveChanges();
        }

        public List<Book> GetAllBook()
        {
            return context.Books.Include(x => x.Category).ToList();
        }

        public Book? GetBookById(int id)
        {
            return context.Books.Where(x => x.BookId == id).FirstOrDefault();
        }

        public int UpdateBook(Book book)
        {
            context.Books.Update(book);
            return context.SaveChanges();
        }
    }
}
