using System.Linq;
using CustomEventBus;
using CustomEventBus.Signals;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class LevelController : IService
{
    private ILevelLoader _levelLoader;
    private int _currentLevelId;
    private LevelData _currentLevelData;

    private EventBus _eventBus;

    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<LevelTimePassedSignal>(LevelPassed);
        _eventBus.Subscribe<NextLevelSignal>(NextLevel);
        _eventBus.Subscribe<RestartLevelSignal>(RestartLevel);

        _levelLoader = ServiceLocator.Current.Get<ILevelLoader>();
        _currentLevelId = PlayerPrefs.GetInt(StringConstants.CURRENT_LEVEL, 1);

        OnInit();
    }

    private async void OnInit()
    {
        await UniTask.WaitUntil(_levelLoader.IsLoaded);
        _currentLevelData = _levelLoader.GetLevels().FirstOrDefault(x => x.LevelId == _currentLevelId);
        if (_currentLevelData == null)
        {
            Debug.LogErrorFormat("Can't find level with id {0}", _currentLevelId);
            return;
        }
        _eventBus.Invoke(new SetLevelSignal(_currentLevelData));
    }

    private void NextLevel(NextLevelSignal signal)
    {
        _currentLevelId++;
        SelectLevel(_currentLevelId);
    }

    private void RestartLevel(RestartLevelSignal signal)
    {
        _eventBus.Invoke(new SetLevelSignal(_currentLevelData));
    }

    private void SelectLevel(int level)
    {
        _currentLevelId = level;
        _currentLevelData = _levelLoader.GetLevels().FirstOrDefault(x => x.LevelId == _currentLevelId);
        _eventBus.Invoke(new SetLevelSignal(_currentLevelData));
    }

    private void LevelPassed(LevelTimePassedSignal signal)
    {
        var player = ServiceLocator.Current.Get<Player>();
        if (player.Health > 0)
        {
            PlayerPrefs.SetInt(StringConstants.CURRENT_LEVEL, (_currentLevelId + 1));
            _eventBus.Invoke(new LevelFinishedSignal(_currentLevelData));
        }
    }

}