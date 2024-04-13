using BookStore.Core.Models;
using BookStore.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Services
{
    public class BooksService : IBooksService
    {
        private readonly IBooksRepository booksRepository;

        public BooksService(IBooksRepository booksRepository)
        {
            this.booksRepository = booksRepository;
        }

        public async Task<List<Book>> GetAllBooks()
        {
            return await booksRepository.Get();
        }

        public async Task<Guid> CreateBook(Book book)
        {
            return await booksRepository.Create(book);
        }

        public async Task<Guid> UpdateBook(Guid id, string title, string description, decimal price)
        {
            return await booksRepository.UpDate(id, title, description, price);
        }

        public async Task<Guid> DeleteBook(Guid id)
        {
            return await booksRepository.Delete(id);
        }
    }

}
