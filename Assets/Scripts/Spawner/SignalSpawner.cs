using System;
using CustomEventBus;
using CustomEventBus.Signals;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class SignalSpawner : IService, CustomEventBus.IDisposable
{
    private bool _isLevelRunning = false;
    private LevelData _levelData;

    private float _curTime;

    private EventBus _eventBus;

    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<SetLevelSignal>(LevelSet);
        _eventBus.Subscribe<StartGameSingal>(GameStart);
        _eventBus.Subscribe<StopGameSingal>(GameStop);
    }

    private void LevelSet(SetLevelSignal signal)
    {
        _levelData = signal.LevelData;
    }

    private void GameStart(StartGameSingal signal)
    {
        _isLevelRunning = true;

        _curTime = 0f;

        var interactables = _levelData.InteractableData;

        foreach (var interactableData in interactables)
        {
            _ = SpawnInteractable(interactableData);
        }

        _ = TrackLevelProgress();
    }

    private async UniTask SpawnInteractable(InteractableData interactableData)
    {
        var cooldown = interactableData.StartCooldown;

        await UniTask.Delay(TimeSpan.FromSeconds(interactableData.PrewarmTime));
        while (_isLevelRunning)
        {
            _eventBus.Invoke(new SpawnInteractableSignal(interactableData.Prefab));

            await UniTask.Delay(TimeSpan.FromSeconds(cooldown));
            cooldown = Mathf.Lerp(interactableData.StartCooldown,
                interactableData.EndCooldown, (_curTime / _levelData.LevelLength));
        }
    }
    private async UniTask TrackLevelProgress()
    {
        while (_isLevelRunning)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
            _curTime += 0.1f;

            var levelProgress = _curTime / _levelData.LevelLength;

            _eventBus.Invoke(new LevelProgressChangedSignal(levelProgress));

            if (_curTime >= _levelData.LevelLength)
            {
                _eventBus.Invoke(new LevelTimePassedSignal());
                _isLevelRunning = false;
            }
        }
    }
    private void GameStop(StopGameSingal signal)
    {
        _isLevelRunning = false;
    }

    public void Dispose()
    {
        _eventBus.Subscribe<SetLevelSignal>(LevelSet);
        _eventBus.Subscribe<StartGameSingal>(GameStart);
        _eventBus.Subscribe<StopGameSingal>(GameStop);
    }
}