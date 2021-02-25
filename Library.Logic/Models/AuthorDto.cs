using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Logic.Models
{
     public class AuthorDto
    {
        public int Id { get; set; }
        public string FIO { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Middlename { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime Deathday { get; set; }
        public string BirthPlace { get; set; }
        public string Biography { get; set; }

        public List<String> Books { get; set; } = new List<String>();
    }
}
