using BookLibrary.Services;

namespace BookLibrary;

public class App : IApp
{
    private readonly IUserCommunication _userCommunication;
    private readonly IEventHandler _eventHandler;

    public App(IUserCommunication userCommunication, IEventHandler eventHandler)
    {
        _userCommunication = userCommunication;
        _eventHandler = eventHandler;
    }
    public void Run()
    {
        _eventHandler.SubscribeToEvents();

        _userCommunication.CommunicationWithUser();      
    }
}
