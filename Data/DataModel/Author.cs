using System;
using System.Collections.Generic;
using System.Text;

namespace Data.DataModel
{
    public class Author
    {
        public int Id { get; set; }
        public string FIO { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Middlename { get; set; }
        public string YearsOfLife { get; set; }
        public string BirthPlace { get; set; }
        public string Biography { get; set; }

        public List<Book> Books { get; set; } = new List<Book>();
    }
}
