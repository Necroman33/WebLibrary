using Data;
using Data.DataModel;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Logic
{
    public class BookLogic
    {
        private LibraryContext _context;

        public BookLogic(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetBookList()
        {
            return _context.Books.ToList();
        }

        public async Task<Book> GetBookById(int id)
        {
            try
            {
                return _context.Books.First(b => b.Id == id);
            }
            catch 
            {
                return null;
            }
        }

        public async Task<bool> ChangeBook(int id, BookDto book)
        {
                if (!BookExists(id))
                {
                    return false;
                }
             else
             {
                var currBook = _context.Books.First(b => b.Id == id);
                currBook.Title = book.Title;
                currBook.Author = book.Author;
                currBook.Description = book.Description;
                currBook.ShortDescription = book.ShortDescription;
                currBook.PublicationDate = book.PublicationDate;
                currBook.AverageRating = book.AverageRating;
                currBook.Status = book.Status;
                ///currBook.Genres = book.Genres;
                foreach (Tag tag in book.Tags)
               {
                    var currTag = _context.Tags.First(t => t.TagName == tag.TagName);
                    currBook.BookTags.Add(new BookTag { 
                    Tag = currTag
                    });
               }
                await _context.SaveChangesAsync();
                return true;
             }
        }

        public async Task<Book> AddBook(BookDto book)
        {
            var currentBook = DtoConvert.BookFromDtoBook(book);
            foreach (Tag tags in book.Tags)
            {
                var currTag = _context.Tags.First(t => t.TagName == tags.TagName);
                tags.Id = currTag.Id;
                currentBook.Tags.Add(currTag);
            }
            _context.Books.Add(currentBook);
            await _context.SaveChangesAsync();
            return _context.Books.Last();
        }
        public async Task<bool> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return false;
            }
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;

        }
        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
