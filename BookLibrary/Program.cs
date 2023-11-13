using BookLibrary;
using BookLibrary.Components.CsvReader;
using BookLibrary.Components.XmlReader;
using BookLibrary.DataProviders;
using BookLibrary.Entities;
using BookLibrary.Repositories;
using BookLibrary.Services;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddSingleton<IApp, App> ();
services.AddSingleton<IRepository<Book>, ListRepository<Book>>();
services.AddSingleton<IBooksProvider, BooksProvider>();
services.AddSingleton<IUserCommunication, UserCommunication>();
services.AddSingleton<ICsvReader, CsvReader>();
services.AddSingleton<IXmlReader, XmlReader>();
var serviceProvider = services.BuildServiceProvider ();
var app = serviceProvider.GetService<IApp>()!;
app.Run();




