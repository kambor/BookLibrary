using BookLibrary.Entities;

namespace BookLibrary.Services;

public interface IEventHandler
{
    void SubscribeToEvents();
}
