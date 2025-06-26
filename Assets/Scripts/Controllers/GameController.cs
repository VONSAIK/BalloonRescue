using CustomEventBus;
using CustomEventBus.Signals;
using UI;
using UI.Windows;

public class GameController : IService, IDisposable
{
    private EventBus _eventBus;

    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<PlayerDeadSignal>(OnPlayerDead);
        _eventBus.Subscribe<LevelFinishedSignal>(LevelFinished);
        _eventBus.Subscribe<SetLevelSignal>(StartGame, -1);
    }

    public void StartGame(SetLevelSignal signal)
    {
        _eventBus.Invoke(new StartGameSingal());
        _eventBus.Invoke(new LevelPlayMusicSignal());
    }

    public void StopGame()
    {
        _eventBus.Invoke(new StopGameSingal());
    }

    private void OnPlayerDead(PlayerDeadSignal signal)
    {
        StopGame();

        var scoreController = ServiceLocator.Current.Get<ScoreController>();
        YouLoseWindow youWinWindow = WindowManager.ShowWindow<YouLoseWindow>();
        youWinWindow.Init(scoreController.Score, 0);
    }

    private void LevelFinished(LevelFinishedSignal signal)
    {
        StopGame();

        var scoreController = ServiceLocator.Current.Get<ScoreController>();
        YouWinWindow youWinWindow = WindowManager.ShowWindow<YouWinWindow>();
        youWinWindow.Init(scoreController.Score, signal.LevelData.CoinForPass);

    }

    public void Dispose()
    {
        _eventBus.Unsubscribe<PlayerDeadSignal>(OnPlayerDead);
        _eventBus.Unsubscribe<LevelFinishedSignal>(LevelFinished);
        _eventBus.Unsubscribe<SetLevelSignal>(StartGame);
    }
}
