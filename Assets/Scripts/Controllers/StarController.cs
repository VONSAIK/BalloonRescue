using CustomEventBus;
using CustomEventBus.Signals;
using UnityEngine;

public class StarController : IService, IDisposable
{

    private int _earnedStars;
    public int EarnedStars => _earnedStars;

    private EventBus _eventBus;

    private int _currentPlayerHealth;
    private int _currentScore;
    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<LevelFinishedSignal>(OnLevelFinished, -1);
    }

    private void OnLevelFinished(LevelFinishedSignal signal)
    {
        _currentPlayerHealth = ServiceLocator.Current.Get<Player>().Health;
        _currentScore = ServiceLocator.Current.Get<ScoreController>().Score;

        var level = signal.LevelData;

        int stars = 0;

        stars += 1;

        if (_currentPlayerHealth >= 3)
            stars += 1;

        if (_currentScore >= level.ScoreForStar)
            stars += 1;

        _earnedStars = stars;

        var maxStars = GetMaxStars(_earnedStars);
        if (_earnedStars > maxStars)
        {
            PlayerPrefs.SetInt(StringConstants.MAX_LEVEL_STARS + level.LevelId, stars);
        }
    }

    public int GetMaxStars(int levelId)
    {
        return PlayerPrefs.GetInt(StringConstants.MAX_LEVEL_STARS + levelId, 0);
    }

    public void Dispose()
    {
        _eventBus.Unsubscribe<LevelFinishedSignal>(OnLevelFinished);
    }
}
