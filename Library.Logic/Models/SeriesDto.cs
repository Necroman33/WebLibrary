using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Logic.Models
{
    public class SeriesDto
    {
        public int Id { get; set; }
        public string SeriesName { get; set; }


        public List<String> Books { get; set; } = new List<String>();
    }
}
