using UnityEngine;
using CustomEventBus;
using CustomEventBus.Signals;

public class GameController : IService
{
    private EventBus _eventBus;

    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        StartGame();
    }

    public void StartGame()
    {
        _eventBus.Invoke(new StartGameSingal());
    }

    public void StopGame()
    {
        _eventBus.Invoke(new StopGameSingal());
    }
}
