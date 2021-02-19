using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Logic.Models
{
    public class GenreDto
    {
        public int Id { get; set; }
        public string GenreName { get; set; }


        public List<String> Books { get; set; } = new List<String>();
    }
}
