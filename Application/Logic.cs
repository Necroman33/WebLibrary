using Data.DataModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application
{
    public class Logic
    {
        private readonly WebLibraryContext _context;

        public Logic(WebLibraryContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<Book>>> GetBookList()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<ActionResult<Book>> GetBookById(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task<ActionResult<bool>> ChangeBook(int id, Book book)
        {
            _context.Entry(book).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
            return true;

        }

        public async Task<ActionResult<Book>> AddBook(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }
        public async Task<ActionResult<bool>> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return null;
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
