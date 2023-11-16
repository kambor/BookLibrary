using BookLibrary.UI.Services;

namespace BookLibrary.UI;

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
