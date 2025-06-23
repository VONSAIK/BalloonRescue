using UnityEngine;
using CustomEventBus;
using CustomEventBus.Signals;

public class GameController : IService
{
    private EventBus _eventBus;

    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<SetLevelSignal>(StartGame, -1);
    }

    public void StartGame(SetLevelSignal signal)
    {
        _eventBus.Invoke(new StartGameSingal());
    }

    public void StopGame()
    {
        _eventBus.Invoke(new StopGameSingal());
    }
}
