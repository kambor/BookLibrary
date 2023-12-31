﻿using BookLibrary.DataAccess.Data.Entities;
using BookLibrary.DataAccess.Data.Repositories;

namespace BookLibrary.ApplicationServices.Components.DataProviders;

public class BooksProvider : IBooksProvider
{
    private readonly IRepository<Book> _booksRepository;

    public BooksProvider(IRepository<Book> booksRepository)
    {
        _booksRepository = booksRepository;
    }

    public List<string> GetUniqueAuthors()
    {
        var books = _booksRepository.GetAll();
        return books
            .Select(x => x.Author
            .ToString())
            .Distinct()
            .ToList();
    }

    public List<Book> OrderByRating()
    {
        var books = _booksRepository.GetAll();
        return books.OrderByDescending(x => x.AverageRating).ToList();
    }

    public List<Book> ShowBooksWhereAuthorIs(string author)
    {
        var books = _booksRepository.GetAll();
        return books
            .Where(x => x.Author == author)
            .OrderBy(x => x.Title)
            .ToList();
    }

    public List<Book> TakeBooks(Range range)
    {
        var books = _booksRepository.GetAll();
        return books
            .OrderBy(x => x.Id)
            .Take(range)
            .ToList();
    }
}
