using System;
using System.Collections.Generic;
using System.Text;

namespace Data.DataModel
{
    public class Series
    {
        public int Id { get; set; }
        public string SeriesName { get; set; }

        public List<BookSeries> BookSeries { get; set; } = new List<BookSeries>();
    }
}
