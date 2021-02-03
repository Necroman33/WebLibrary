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

        public async Task<IEnumerable<BookDto>> GetBookList()
        {
            var books = _context.Books.ToList();
            return DtoConvert.GetBookListDto(books);
        }

        public async Task<BookDto> GetBookById(int id)
        {
            try
            {
                var book = _context.Books.First(b => b.Id == id);
                return DtoConvert.GetBookDto(book);
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
                await _context.SaveChangesAsync();
                return true;
             }
        }

        public async Task<BookDto> AddBook(BookDto book)
        {
            _context.Books.Add(DtoConvert.AddBookDto(book));
            await _context.SaveChangesAsync();
            book.Id = _context.Books.Last().Id;
            return book;
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
