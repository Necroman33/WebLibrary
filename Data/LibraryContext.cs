using System;
using System.Collections.Generic;
using System.Text;
using Data.DataModel;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
    }
}
