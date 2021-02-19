using System;
using System.Collections.Generic;
using System.Text;

namespace Data.DataModel
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public DateTime PublicationDate { get; set; }
        public float AverageRating { get; set; }
        public string Status { get; set; }


        public int AuthorId { get; set; }
        public Author Author { get; set; }

        public List<BookGenre> BookGenres { get; set; } = new List<BookGenre>();
        public List<BookTag> BookTags { get; set; } = new List<BookTag>();
        public List<BookSeries> BookSeries { get; set; } = new List<BookSeries>();
    }
}