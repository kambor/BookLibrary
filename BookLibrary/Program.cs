using BookLibrary;
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
var serviceProvider = services.BuildServiceProvider ();
var app = serviceProvider.GetService<IApp>()!;
app.Run();




