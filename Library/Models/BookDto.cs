using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Models
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
    }
}
