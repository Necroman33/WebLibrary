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
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<BookTag> BooksTags { get; set; }

    }
}
