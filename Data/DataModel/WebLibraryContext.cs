using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Data.DataModel
{
    public class WebLibraryContext: DbContext
    {
        public WebLibraryContext(DbContextOptions<WebLibraryContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
    }
}
