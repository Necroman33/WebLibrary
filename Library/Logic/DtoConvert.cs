using Data.DataModel;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Logic
{
    public class DtoConvert
    {
        public static IEnumerable<BookDto> GetBookListDto(List<Book> books)
        {
            var BookList = books.Select(b =>
            new BookDto
            {
                Id = b.Id,
                Author = b.Author,
                Description = b.Description,
                ShortDescription = b.ShortDescription,
                Title = b.Title
            });
            return BookList;
        }

        public static BookDto GetBookDto(Book book)
        {
            var Book = new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Description = book.Description,
                ShortDescription = book.ShortDescription
            };
            return Book;
        }

        public static Book AddBookDto(BookDto book)
        {
            var Book = new Book
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Description = book.Description,
                ShortDescription = book.ShortDescription
            };
            return Book;
        }
    }
}
