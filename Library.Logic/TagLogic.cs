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
    public class TagLogic
    {
        private LibraryContext _context;

        public TagLogic(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tag>> GetTagList()
        {
            return _context.Tags.Include(b => b.BookTags).ThenInclude(bt => bt.Book).ToList();
        }


        public async Task<Tag> GetTagById(int id)
        {
            try
            {
                return _context.Tags.Include(b => b.BookTags).ThenInclude(bt => bt.Book).First(b => b.Id == id);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> ChangeTag(int id, string tag)
        {
            if (!TagExists(id))
            {
                return false;
            }
            else
            {
                var currTag = _context.Tags.Include(b => b.BookTags).ThenInclude(bt => bt.Book).First(b => b.Id == id);
                currTag.TagName = tag;
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<Tag> AddTag(string tag)
        {
            var currentTag = new Tag { TagName = tag};
            currentTag.TagName = tag;
            _context.Tags.Add(currentTag);
            await _context.SaveChangesAsync();
            return currentTag;
        }
        public async Task<bool> DeleteTag(int id)
        {
            var tag = await _context.Tags.FindAsync(id);
            if (tag == null)
            {
                return false;
            }
            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();
            return true;

        }
        private bool TagExists(int id)
        {
            return _context.Tags.Any(e => e.Id == id);
        }
    }
}
