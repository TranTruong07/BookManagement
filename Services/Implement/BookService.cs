using DataAccess.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implement
{
    public class BookService : IBookService
    {
        private readonly IBookRepository bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        public int AddBook(Book book)
        {
            return bookRepository.AddBook(book);
        }

        public void ChangeAvailable(Book book)
        {
            bookRepository.ChangeAvailable(book);
        }

        public List<Book> GetAllBook()
        {
            return bookRepository.GetAllBook();
        }

        public Book? GetBookById(int id)
        {
            return bookRepository.GetBookById(id);
        }

        public int UpdateBook(Book book)
        {
            return bookRepository.UpdateBook(book);
        }
    }
}
