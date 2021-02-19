using Data;
using Data.DataModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Logic
{
    public class AuthorLogic
    {
        private LibraryContext _context;

        public AuthorLogic(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Author>> GetAuthorList()
        {
            return _context.Authors.Include(b => b.Books).ToList();
        }


        public async Task<Author> GetAuthorById(int id)
        {
            try
            {
                return _context.Authors.Include(b => b.Books).First(b => b.Id == id);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> ChangeAuthor(int id, AuthorDto Author)
        {
            if (!AuthorExists(id))
            {
                return false;
            }
            else
            {
                var currAuthor = _context.Authors.Include(b => b.Books.First(b => b.Id == id);
                currAuthor.SeriesName = Series;
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<Series> AddSeries(string Series)
        {
            var currentSeries = new Series { SeriesName = Series };
            currentSeries.SeriesName = Series;
            _context.Series.Add(currentSeries);
            await _context.SaveChangesAsync();
            return currentSeries;
        }
        public async Task<bool> DeleteSeries(int id)
        {
            var Series = await _context.Series.FindAsync(id);
            if (Series == null)
            {
                return false;
            }
            _context.Series.Remove(Series);
            await _context.SaveChangesAsync();
            return true;

        }
        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(e => e.Id == id);
        }
    }
}
