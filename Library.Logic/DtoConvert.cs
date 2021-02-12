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
                ShortDescription = book.ShortDescription,
                PublicationDate = book.PublicationDate,
                AverageRating = book.AverageRating,
                Status = book.Status,
                Tags = new List<Tag>()
            };
            if (book.BookTags!=null) {
                foreach (BookTag bookTag in book.BookTags)
                {
                    Book.Tags.Add(new Tag {
                        Id = bookTag.TagId,
                        TagName = bookTag.Tag.TagName,
                    });
                } }
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
                ShortDescription = book.ShortDescription,
                PublicationDate = book.PublicationDate,
                AverageRating = book.AverageRating,
                Status = book.Status,
                BookTags = new List<BookTag>()
            };
           /* if (book.Tags != null)
            {
                foreach (Tag tags in book.Tags)
                {
                    Book.BookTags.Add(new BookTag
                    {
                        BookId = book.Id,
                        TagId = tags.Id,
                    });
                }
            }*/
            return Book;
        }
    }
}
