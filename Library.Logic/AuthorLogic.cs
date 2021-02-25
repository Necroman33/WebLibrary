using Data;
using Data.DataModel;
using Library.Logic.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        private Author AuthorFromAuthorDto(AuthorDto author)
        {
            var Author = new Author
            {
                FIO = author.FIO,
                Name = author.Name,
                Surname = author.Surname,
                Middlename = author.Middlename,
                Birthday = author.Birthday,
                Deathday = author.Deathday,
                BirthPlace = author.BirthPlace,
                Biography = author.Biography
        };
            foreach (string book in author.Books)
            {
                Author.Books.Add(_context.Books.First(b => b.Title == book));
            }
            
            return Author;
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

        public async Task<IEnumerable<Author>> GetAuthorsByFIO(string FIO)
        {
            try
            {
                var autorsList = _context.Authors.Include(b => b.Books).ToList();
                var filtratedAutors = new List<Author>();
                Regex regex = new Regex(@$"(\w*){FIO}(\w*)");
                foreach (Author autor in autorsList)
                {
                    if (regex.IsMatch(autor.FIO))
                    {
                        filtratedAutors.Add(autor);
                    }
                }
                return filtratedAutors;
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
                var currAuthor = _context.Authors.Include(b => b.Books).First(b => b.Id == id);
                currAuthor.FIO = Author.FIO;
                currAuthor.Name = Author.Name;
                currAuthor.Surname = Author.Surname;
                currAuthor.Middlename = Author.Middlename;
                currAuthor.Birthday = Author.Birthday;
                currAuthor.Deathday = Author.Deathday;
                currAuthor.BirthPlace = Author.BirthPlace;
                currAuthor.Biography = Author.Biography;
                if (Author.Books!=null)
                {
                    currAuthor.Books = new List<Book>();
                    foreach (String book in Author.Books)
                    {
                        var currBook = _context.Books.First(b=>b.Title == book);
                        currAuthor.Books.Add(currBook);
                    }
                }
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<Author> AddAuthor(AuthorDto Author)
        {
            var currentAuthor = AuthorFromAuthorDto(Author);
            _context.Authors.Add(currentAuthor);
            await _context.SaveChangesAsync();
            return currentAuthor;
        }
        public async Task<bool> DeleteAuthor(int id)
        {
            var Author = await _context.Authors.FindAsync(id);
            if (Author == null)
            {
                return false;
            }
            _context.Authors.Remove(Author);
            await _context.SaveChangesAsync();
            return true;

        }
        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(e => e.Id == id);
        }
    }
}
