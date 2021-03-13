using Data;
using Data.DataModel;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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


        private Book BookFromBookDto(BookDto book)
        {
            var Book = new Book
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                ShortDescription = book.ShortDescription,
                PublicationDate = book.PublicationDate,
                AverageRating = book.AverageRating,
                Status = book.Status,
            };
            if (book.Author != null)
            {
                Book.Author = _context.Authors.First(a => a.FIO == book.Author);
            }
            foreach (String tag in book.Tags)
            {
                var currTag = _context.Tags.First(t => t.TagName == tag);
                Book.BookTags.Add(new BookTag
                {
                    Tag = currTag
                });
            }
            foreach (string genre in book.Genres)
            {
                var currGenre = _context.Genres.First(g => g.GenreName == genre);
                Book.BookGenres.Add(new BookGenre
                {
                    Genre = currGenre
                });
            }
            foreach (string series in book.Series)
            {
                var currSeries = _context.Series.First(s => s.SeriesName == series);
                Book.BookSeries.Add(new BookSeries
                {
                    Series = currSeries
                });
            }
            return Book;
        }

        public async Task<IEnumerable<Book>> GetBookList()
        {
            return _context.Books
                .Include(b => b.BookTags).ThenInclude(bt => bt.Tag)
                .Include(b => b.BookGenres).ThenInclude(bg => bg.Genre)
                .Include(b => b.BookSeries).ThenInclude(bg => bg.Series)
                .Include(b=>b.Author)
                .ToList();
        }

        public async Task<Book> GetBookById(int id)
        {
            try
            {
                return _context.Books
                    .Include(b => b.BookTags).ThenInclude(bt => bt.Tag)
                    .Include(b => b.BookGenres).ThenInclude(bg => bg.Genre)
                    .Include(b => b.BookSeries).ThenInclude(bg => bg.Series)
                    .Include(b => b.Author)
                    .First(b => b.Id == id);
            }
            catch 
            {
                return null;
            }
        }

        public async Task<IEnumerable<Book>> GetBookByPharam(string value, string type)
        {
            try
            {
                var BookList = _context.Books
                    .Include(b => b.BookTags).ThenInclude(bt => bt.Tag)
                    .Include(b => b.BookGenres).ThenInclude(bg => bg.Genre)
                    .Include(b => b.BookSeries).ThenInclude(bg => bg.Series)
                    .Include(b => b.Author)
                    .ToList();
                var filtratedBooks = new List<Book>();
                Regex regex = new Regex(@$"(\w*){value}(\w*)");
                switch (type)
                {
                    case "title":
                        foreach (Book book in BookList)
                        {
                            if (regex.IsMatch(book.Title))
                            {
                                filtratedBooks.Add(book);
                            }
                        }
                        break;
                    case "author":
                        foreach (Book book in BookList)
                        {
                            if (regex.IsMatch(book.Author.FIO))
                            {
                                filtratedBooks.Add(book);
                            }
                        }
                        break;
                    case "series":
                        foreach (Book book in BookList)
                        {
                            bool flag = false;
                            foreach(BookSeries series in book.BookSeries)
                            {
                                if (regex.IsMatch(series.Series.SeriesName)&&flag==false)
                                {
                                    filtratedBooks.Add(book);
                                    flag = true;
                                }
                            }
                        }
                        break;
                    case "publicationYear":
                        foreach (Book book in BookList)
                        {
                            if (book.PublicationDate.Year == Convert.ToInt32(value))
                            {
                                filtratedBooks.Add(book);
                            }
                        }
                        break;
                    case "genre":
                        foreach (Book book in BookList)
                        {
                            bool flag = false;
                            foreach (BookGenre genre in book.BookGenres)
                            {
                                if (regex.IsMatch(genre.Genre.GenreName) && flag == false)
                                {
                                    filtratedBooks.Add(book);
                                    flag = true;
                                }
                            }
                        }
                        break;
                    case "tag":
                        foreach (Book book in BookList)
                        {
                            bool flag = false;
                            foreach (BookTag tag in book.BookTags)
                            {
                                if (regex.IsMatch(tag.Tag.TagName) && flag == false)
                                {
                                    filtratedBooks.Add(book);
                                    flag = true;
                                }
                            }
                        }
                        break;
                }
                return filtratedBooks;
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
                var currBook = _context.Books
                    .Include(b => b.BookTags).ThenInclude(bt => bt.Tag)
                    .Include(b => b.BookGenres).ThenInclude(bg => bg.Genre)
                    .Include(b => b.BookSeries).ThenInclude(bg => bg.Series)
                    .Include(b=>b.Author)
                    .First(b => b.Id == id);

                currBook.Title = book.Title;
                currBook.Author = _context.Authors.First(a => a.FIO == book.Author);
                currBook.Description = book.Description;
                currBook.ShortDescription = book.ShortDescription;
                currBook.PublicationDate = book.PublicationDate;
                currBook.AverageRating = book.AverageRating;
                currBook.Status = book.Status;
                if (book.Tags!=null) 
                {
                    currBook.BookTags = new List<BookTag>();
                    foreach (String tag in book.Tags)
                    {
                        var currTag = _context.Tags.First(t => t.TagName == tag);
                        currBook.BookTags.Add(new BookTag {
                            Tag = currTag
                        });
                    }
                }
                if (book.Genres != null)
                {
                    currBook.BookGenres = new List<BookGenre>();
                    foreach (String genre in book.Genres)
                    {
                        var currGenre = _context.Genres.First(t => t.GenreName == genre);
                        currBook.BookGenres.Add(new BookGenre
                        {
                            Genre = currGenre
                        });
                    }
                }
                if (book.Series != null)
                {
                    currBook.BookSeries = new List<BookSeries>();
                    foreach (String series in book.Series)
                    {
                        var currSeries = _context.Series.First(t => t.SeriesName == series);
                        currBook.BookSeries.Add(new BookSeries
                        {
                            Series = currSeries
                        });
                    }
                }
                await _context.SaveChangesAsync();
                return true;
             }
        }

        public async Task<Book> AddBook(BookDto book)
        {
            var currentBook = BookFromBookDto(book);
            _context.Books.Add(currentBook);
            await _context.SaveChangesAsync();
            return currentBook;
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
