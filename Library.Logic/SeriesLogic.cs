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
    public class SeriesLogic
    {
        private LibraryContext _context;

        public SeriesLogic(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Series>> GetSeriesList()
        {
            return _context.Series.Include(b => b.BookSeries).ThenInclude(bt => bt.Book).ToList();
        }


        public async Task<Series> GetSeriesById(int id)
        {
            try
            {
                return _context.Series.Include(b => b.BookSeries).ThenInclude(bt => bt.Book).First(b => b.Id == id);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> ChangeSeries(int id, string Series)
        {
            if (!SeriesExists(id))
            {
                return false;
            }
            else
            {
                var currSeries = _context.Series.Include(b => b.BookSeries).ThenInclude(bt => bt.Book).First(b => b.Id == id);
                currSeries.SeriesName = Series;
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
        private bool SeriesExists(int id)
        {
            return _context.Series.Any(e => e.Id == id);
        }
    }
}
