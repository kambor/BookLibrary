using BookLibrary.DataProviders;
using BookLibrary.Entities;
using BookLibrary.Repositories;
using BookLibrary.Services;

namespace BookLibrary;

public class App : IApp
{
    private readonly IUserCommunication _userCommunication;

    public App(IUserCommunication userCommunication)
    {
        _userCommunication = userCommunication;
    }
    public void Run()
    {
        _userCommunication.CommunicationWithUser();  
    }  
}
