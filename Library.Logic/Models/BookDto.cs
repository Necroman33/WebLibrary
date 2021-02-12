using Data.DataModel;
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
        public DateTime PublicationDate { get; set; }
        public float AverageRating { get; set; }
        public string Status { get; set; }


        public List<Genre> Genres { get; set; }
        public List<Tag> Tags { get; set; }
    }
}
