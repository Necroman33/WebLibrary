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
    public class GenreLogic
    {
        private LibraryContext _context;

        public GenreLogic(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Genre>> GetGenreList()
        {
            return _context.Genres.Include(b => b.BookGenres).ThenInclude(bt => bt.Book).ToList();
        }


        public async Task<Genre> GetGenreById(int id)
        {
            try
            {
                return _context.Genres.Include(b => b.BookGenres).ThenInclude(bt => bt.Book).First(b => b.Id == id);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> ChangeGenre(int id, string Genre)
        {
            if (!GenreExists(id))
            {
                return false;
            }
            else
            {
                var currGenre = _context.Genres.Include(b => b.BookGenres).ThenInclude(bt => bt.Book).First(b => b.Id == id);
                currGenre.GenreName = Genre;
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<Genre> AddGenre(string Genre)
        {
            var currentGenre = new Genre { GenreName = Genre };
            currentGenre.GenreName = Genre;
            _context.Genres.Add(currentGenre);
            await _context.SaveChangesAsync();
            return currentGenre;
        }
        public async Task<bool> DeleteGenre(int id)
        {
            var Genre = await _context.Genres.FindAsync(id);
            if (Genre == null)
            {
                return false;
            }
            _context.Genres.Remove(Genre);
            await _context.SaveChangesAsync();
            return true;

        }
        private bool GenreExists(int id)
        {
            return _context.Genres.Any(e => e.Id == id);
        }
    }
}
