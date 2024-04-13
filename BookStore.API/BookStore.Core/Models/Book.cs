using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Models
{
    public class Book
    {
        public const int MAX_TITLE_LENGTH = 250;
        private Book (Guid Id, string Title, string Description, decimal Price)
        {
            this.Id = Id;
            this.Title = Title;
            this.Description = Description;
            this.Price = Price;
        }
        public Guid Id { get; }
        public string Title { get; } = string.Empty;
        public string Description { get; } = string.Empty;
        public decimal Price { get; }

        public static (Book Book, string Error) Create(Guid Id, string Title, string Description, decimal Price)
        {
            var error = string.Empty;

            if (string.IsNullOrWhiteSpace(Title) || Title.Length > MAX_TITLE_LENGTH)
            {
                error = $"Title must be between 1 and {MAX_TITLE_LENGTH} characters";
            }
            var book = new Book(Id, Title, Description, Price);

            return (book, error);
        }

    }
}
