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
        public static BookDto BookDtoFromBook(Book book)
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

        public static Book BookFromDtoBook(BookDto book)
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
