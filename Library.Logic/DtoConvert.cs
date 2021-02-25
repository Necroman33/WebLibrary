using Data.DataModel;
using Library.Logic.Models;
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
                Description = book.Description,
                ShortDescription = book.ShortDescription,
                PublicationDate = book.PublicationDate,
                AverageRating = book.AverageRating,
                Status = book.Status,
            };
            if (book.BookTags!=null) {
                foreach (BookTag bookTag in book.BookTags)
                {
                    Book.Tags.Add(bookTag.Tag.TagName);
                } }
            if (book.BookGenres != null)
            {
                foreach (BookGenre bookGenre in book.BookGenres)
                {
                    Book.Genres.Add(bookGenre.Genre.GenreName);
                }
            }
            if (book.BookSeries != null)
            {
                foreach (BookSeries bookSeries in book.BookSeries)
                {
                    Book.Series.Add(bookSeries.Series.SeriesName);
                }
            }
            if (book.Author != null)
            {
                Book.Author = book.Author.FIO; 
            }
            return Book;
        }

        public static TagDto TagDtoFromTag(Tag tag)
        {
            var Tag = new TagDto
            {
                Id = tag.Id,
                TagName = tag.TagName
            };
            if (tag.BookTags != null)
            {
                foreach (BookTag bookTag in tag.BookTags)
                {
                    Tag.Books.Add(bookTag.Book.Title);
                }
            }
            return Tag;
        }
        public static GenreDto GenreDtoFromGenre(Genre genre)
        {
            var Genre = new GenreDto
            {
                Id = genre.Id,
                GenreName = genre.GenreName
            };
            if (genre.BookGenres != null)
            {
                foreach (BookGenre bookGenre in genre.BookGenres)
                {
                    Genre.Books.Add(bookGenre.Book.Title);
                }
            }
            return Genre;
        }

        public static SeriesDto SeriesDtoFromSeries(Series series)
        {
            var Series = new SeriesDto
            {
                Id = series.Id,
                SeriesName = series.SeriesName
            };
            if (series.BookSeries != null)
            {
                foreach (BookSeries bookSeries in series.BookSeries)
                {
                    Series.Books.Add(bookSeries.Book.Title);
                }
            }
            return Series;
        }
        public static AuthorDto AuthorDtoFromAuthor(Author author)
        {
            var Author = new AuthorDto
            {
                Id = author.Id,
                FIO = author.FIO,
                Name = author.Name,
                Surname = author.Surname,
                Middlename = author.Middlename,
                Birthday = author.Birthday,
                Deathday = author.Deathday,
                BirthPlace = author.BirthPlace,
                Biography = author.Biography,
            };
            if (author.Books != null)
            {
                foreach (Book book in author.Books)
                {
                    Author.Books.Add(book.Title);
                }
            }
            return Author;
        }
    }
}
