using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomEventBus;
using CustomEventBus.Signals;

public class Player : MonoBehaviour, IService
{
    [SerializeField] private int _health;
    public int Health => _health;

    private EventBus _eventBus;

    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<PlayerDamagedSignal>(OnPlayerDamaged);
        _eventBus.Subscribe<StartGameSingal>(GameStarted);
    }

    private void GameStarted(StartGameSingal signal)
    {
        _health = 3;
        _eventBus.Invoke(new HealthChangedSignal(_health));
    }

    private void OnPlayerDamaged(PlayerDamagedSignal signal)
    {
        int val = signal.Value;

        _health -= val;
        if (_health < 0)
        {
            _health = 0;
        }

        _eventBus.Invoke(new HealthChangedSignal(_health));

        if (_health == 0)
        {
            _eventBus.Invoke(new PlayerDeadSignal());
        }
    }

    private void OnDestroy()
    {
        _eventBus.Unsubscribe<PlayerDamagedSignal>(OnPlayerDamaged);
        _eventBus.Unsubscribe<StartGameSingal>(GameStarted);
    }
}
