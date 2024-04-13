using BookStore.Core.Models;
using BookStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Repository
{
    public class BooksRepository : IBooksRepository
    {
        private readonly BookStoreDbContext context;

        public BooksRepository(BookStoreDbContext context)
        {
            this.context = context;
            context.Database.EnsureCreated();
        }

        public async Task<List<Book>> Get()
        {
            var bookEntities = await context.Books
                .AsNoTracking()
                .ToListAsync();

            var books = bookEntities
                .Select(b => Book.Create(b.Id, b.Title, b.Description, b.Price).Book).ToList();

            return books;
        }

        public async Task<Guid> Create(Book book)
        {
            var bookEntity = new BookEntity
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                Price = book.Price
            };

            await context.Books.AddAsync(bookEntity);
            await context.SaveChangesAsync();

            return bookEntity.Id;
        }

        public async Task<Guid> UpDate(Guid id, string title, string description, decimal price)
        {
            await context.Books
                .Where(x => x.Id == id)
                .ExecuteUpdateAsync(b => b
                .SetProperty(a => a.Title, a => title)
                .SetProperty(a => a.Description, a => description)
                .SetProperty(a => a.Price, a => price));

            return id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await context.Books
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }
    }
}
