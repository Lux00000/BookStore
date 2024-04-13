using BookStore.API.Contracts;
using BookStore.Application.Services;
using BookStore.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBooksService booksService;
        public BooksController(IBooksService booksService)
        {
            this.booksService = booksService;
        }

        [HttpGet]
        public async Task<ActionResult<List<BooksResponse>>> GetBooks()
        {
            var books = await this.booksService.GetAllBooks();

            var response = books.Select(a => new BooksResponse(a.Id, a.Title, a.Description, a.Price));

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateBook([FromBody] BooksRequest request)
        {
            var (book, error) = Book.Create(Guid.NewGuid(), request.Title, request.Description, request.Price);

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);

            }

            var bookId = await this.booksService.CreateBook(book);
            return Ok(bookId);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateBooks(Guid id, [FromBody] BooksRequest request)
        {
            var bookId = await this.booksService.UpdateBook(id, request.Title, request.Description, request.Price);

            return Ok(bookId);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteBooks(Guid id)
        {
            return Ok(await this.booksService.DeleteBook(id));
        }
    }
}
