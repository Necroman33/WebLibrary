using System;
using System.Collections.Generic;
using System.Text;

namespace Data.DataModel
{
    public class Tag
    {
        public int Id { get; set; }
        public string TagName { get; set; }


        public List<BookTag> BookTags { get; set; } = new List<BookTag>();
    }
}
