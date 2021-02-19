﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Data.DataModel
{
    public class Genre
    {
        public int Id { get; set; }
        public string GenreName { get; set; }


        public List<BookGenre> BookGenres { get; set; } = new List<BookGenre>();
    }
}
