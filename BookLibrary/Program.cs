using BookLibrary.ApplicationServices.Components.CsvReader;
using BookLibrary.ApplicationServices.Components.DataProviders;
using BookLibrary.ApplicationServices.Components.XmlReader;
using BookLibrary.DataAccess.Data;
using BookLibrary.DataAccess.Data.Entities;
using BookLibrary.DataAccess.Data.Repositories;
using BookLibrary.UI;
using BookLibrary.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


var services = new ServiceCollection();
services.AddSingleton<IApp, App> ();
services.AddSingleton<IRepository<Book>, SqlRepository<Book>>();
services.AddSingleton<IBooksProvider, BooksProvider>();
services.AddSingleton<IUserCommunication, UserCommunication>();
services.AddSingleton<ICsvReader, CsvReader>();
services.AddSingleton<IXmlReader, XmlReader>();
services.AddSingleton<IEventHandler, BookLibrary.UI.Services.EventHandler>();


services.AddDbContext<BookLibraryDbContext>(options => options
    .UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=BookLibraryStorage;Integrated Security=True;TrustServerCertificate=True"));

var serviceProvider = services.BuildServiceProvider ();
var app = serviceProvider.GetService<IApp>()!;
app.Run();




