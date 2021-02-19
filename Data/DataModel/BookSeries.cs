using System;
using System.Collections.Generic;
using System.Text;

namespace Data.DataModel
{
    public class BookSeries
    {
        public int Id { get; set; }
        public Series Series { get; set; }
        public int SeriesId { get; set; }
        public Book Book { get; set; }
        public int BookId { get; set; }
    }
}
