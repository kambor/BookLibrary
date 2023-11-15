using BookLibrary;
using BookLibrary.Components.CsvReader;
using BookLibrary.Components.XmlReader;
using BookLibrary.Data;
using BookLibrary.DataProviders;
using BookLibrary.Entities;
using BookLibrary.Repositories;
using BookLibrary.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddSingleton<IApp, App> ();
services.AddSingleton<IRepository<Book>, SqlRepository<Book>>();
services.AddSingleton<IBooksProvider, BooksProvider>();
services.AddSingleton<IUserCommunication, UserCommunication>();
services.AddSingleton<ICsvReader, CsvReader>();
services.AddSingleton<IXmlReader, XmlReader>();
services.AddSingleton<IEventHandler, BookLibrary.Services.EventHandler>();


services.AddDbContext<BookLibraryDbContext>(options => options
    .UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=BookLibraryStorage;Integrated Security=True;TrustServerCertificate=True"));

var serviceProvider = services.BuildServiceProvider ();
var app = serviceProvider.GetService<IApp>()!;
app.Run();




