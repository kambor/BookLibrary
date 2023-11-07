﻿using BookLibrary.Entities;
using BookLibrary.Repositories;
using System.Drawing;
using System.Text;

namespace BookLibrary.DataProviders;

public class BooksProvider : IBooksProvider
{
    private readonly IRepository<Book> _booksRepository;

    public BooksProvider(IRepository<Book> booksRepository)
    {
        _booksRepository = booksRepository;
    }

    //SELECT
    public List<string> GetUniqueCategory()
    {
        var books = _booksRepository.GetAll();
        return books.Select(x => x.Category.ToString()).Distinct().ToList();
    }

    public List<Book> GetSpecificColumns()
    {
        var books = _booksRepository.GetAll();
        return books.Select(book => new Book
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author
        }).ToList();
    }

    public string AnonymousClass()
    {
        var books = _booksRepository.GetAll();
        var list = books.Select(book => new
        {
            Identifier = book.Id,
            BookTitle = book.Title,
            BookAuthor = book.Author
        });
        StringBuilder sb = new StringBuilder(2048);
        foreach (var book in list)
        {
            sb.AppendLine($"Product ID: {book.Identifier}");
            sb.AppendLine($"    Book Title: {book.BookTitle}");
            sb.AppendLine($"    Book Author: {book.BookAuthor}");
        }

        return sb.ToString();
    }

    // ORDER BY
    public List<Book> OrderByAuthors()
    {
        var books = _booksRepository.GetAll();
        return books.OrderBy(x => x.Author).ToList();
    }
    public List<Book> OrderByAuthorsAndTitleDescending()
    {
        var books = _booksRepository.GetAll();
        return books
            .OrderByDescending(x => x.Author)
            .ThenByDescending(x => x.Title)
            .ToList();
    }

    public List<Book> OrderByRelaseData()
    {
        var books = _booksRepository.GetAll();
        return books.OrderBy(x => x.RelaseData).ToList();
    }

    // WHERE

    public List<Book> WhereStartsWith(string prefix)
    {
        var books = _booksRepository.GetAll();
        return books.Where(x => x.Title.StartsWith(prefix)).ToList();
    }

    public List<Book> WhereAuthorIs(string author)
    {
        var books = _booksRepository.GetAll();
        return books.Where(x => x.Author == author).ToList();
    }

    // FIRST, LAST, SINGLE
    public Book FirstOrDefaultByAuthorWithDefault(string author)
    {
        var books = _booksRepository.GetAll();
        return books
            .FirstOrDefault(
            x => x.Author == author,
            new Book { Id = -1, Title = "NOT FOUND" });

    }

    public Book? FirstByDate()
    {
        var books = _booksRepository.GetAll();
        return books.OrderBy(x => x.RelaseData).FirstOrDefault();       
    }
    public Book LastByAuthor(string author)
    {
        var books = _booksRepository.GetAll();
        return books.Last(x => x.Author == author);
    }

    public Book SingleById(int id)
    {
        var books = _booksRepository.GetAll();
        return books.Single(x => x.Id == id);
    }

    public Book? SingleOrDefaultById(int id)
    {
        var books = _booksRepository.GetAll();
        return books.SingleOrDefault(x => x.Id == id);
    }

    // TAKE
    public List<Book> TakeBooks(int howMany)
    {
        var books = _booksRepository.GetAll();
        return books
            .OrderBy(x => x.Title)
            .Take(howMany)
            .ToList();
    }

    public List<Book> TakeBooks(Range range)
    {
        var books = _booksRepository.GetAll();
        return books
            .OrderBy(x => x.Title)
            .Take(range)
            .ToList();
    }

    public List<Book> TakeBooksWhileRealiseDataAfter(DateTime date)
    {
        var books = _booksRepository.GetAll();
        return books
            .OrderByDescending(x => x.RelaseData)
            .TakeWhile(x => x.RelaseData >= date)
            .ToList();
    }

    // SKIP
    public List<Book> SkipBooks(int howMany)
    {
        var books = _booksRepository.GetAll();
        return books
            .OrderBy(x => x.Title)
            .Skip(howMany)
            .ToList();
    }

    public List<Book> SkipBooksWhileRealiseDataAfter(DateTime date)
    {
        var books = _booksRepository.GetAll();
        return books
           .OrderByDescending(x => x.RelaseData)
           .SkipWhile(x => x.RelaseData >= date)
           .ToList();
    }

    // DISTINCT
    public List<string> DistinctAllCategories()
    {
        var books = _booksRepository.GetAll();
        return books
           .Select(x => x.Category.ToString())
           .Distinct()
           .OrderBy(c => c)
           .ToList();
    }

    public List<Book> DistinctByCategory()
    {
        var books = _booksRepository.GetAll();
        return books
            .DistinctBy(x => x.Category)
            .OrderBy(X => X.Category)
            .ToList();
    }

    // CHUNK
    public List<Book[]> ChunkBooks(int size)
    {
        var books = _booksRepository.GetAll();
        return books.Chunk(size).ToList();
    }
}
