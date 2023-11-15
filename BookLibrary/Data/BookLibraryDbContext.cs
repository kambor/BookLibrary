using BookLibrary.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Data;

public class BookLibraryDbContext : DbContext
{
    public BookLibraryDbContext(DbContextOptions<BookLibraryDbContext> options)
        : base(options)
    {        
    }

    public DbSet<Book>Books { get; set; }

}
