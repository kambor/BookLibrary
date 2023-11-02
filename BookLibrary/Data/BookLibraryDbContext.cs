using BookLibrary.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace BookLibrary.Data;

public class BookLibraryDbContext :DbContext
{
    //private readonly string _connectionString = @"Server=(localdb)\mssqllocaldb;Database=BookLibrary";
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Owner> Owners => Set<Owner>();


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseInMemoryDatabase("StorageAppDb");
        //optionsBuilder.UseSqlServer(_connectionString);
    }

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<Book>()
    //        .Property(b => b.Title)
    //        .IsRequired();
    //}
}
