using BookLibrary.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Data;

public class BookLibraryDbContext :DbContext
{
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Owner> Owners => Set<Owner>();
    public DbSet<Educational> Educationals => Set<Educational>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseInMemoryDatabase("StorageAppDb");
    }
}
