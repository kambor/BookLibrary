using BookLibrary.DataAccess.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.DataAccess.Data;

public class BookLibraryDbContext : DbContext
{
    public BookLibraryDbContext(DbContextOptions<BookLibraryDbContext> options)
        : base(options)
    {        
    }

    public DbSet<Book>Books { get; set; }

}
