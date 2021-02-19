using Data.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Logic.Models
{
    public class TagDto
    {
        public int Id { get; set; }
        public string TagName { get; set; }


        public List<String> Books { get; set; } = new List<String>();
    }
}
