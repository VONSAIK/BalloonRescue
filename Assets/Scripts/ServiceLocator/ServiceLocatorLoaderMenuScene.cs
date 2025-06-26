using System.Collections.Generic;
using CustomEventBus;
using UI;
using UI.Windows;
using UnityEngine;

public class ServiceLocatorLoaderMenuScene: MonoBehaviour
{
    [SerializeField] private GUIHolder _guiHolder;

    [SerializeField] private SOLevelLoader _SOLevelLoader;
    [SerializeField] private SOBalloonLoader _SOBalloonLoader;

    [SerializeField] private SoundController _soundController;

    private ConfigDataLoader _configDataLoader;

    private EventBus _eventBus;
    private CoinController _coinController;
    private ScoreController _scoreController;

    private ILevelLoader _levelLoader;
    private IBalloonLoader _balloonDataLoader;

    private List<IDisposable> _disposables = new List<IDisposable>();

    private void Awake()
    {
        _eventBus = new EventBus();
        _coinController = new CoinController();
        _scoreController = new ScoreController();

        _levelLoader = _SOLevelLoader;
        _balloonDataLoader = _SOBalloonLoader;

        Register();
        Init();
        AddToDisposables();
    }


    private void Init()
    {
        _coinController.Init();
        _scoreController.Init();
        _soundController.Init();

        var loaders = new List<ILoader>();
        loaders.Add(_levelLoader);
        loaders.Add(_balloonDataLoader);
        _configDataLoader = new ConfigDataLoader();
        _configDataLoader.Init(loaders);

        WindowManager.ShowWindow<LoadingWindow>();
    }

    private void Register()
    {
        ServiceLocator.Init();

        ServiceLocator.Current.Register(_coinController);
        ServiceLocator.Current.Register(_scoreController);
        ServiceLocator.Current.Register(_eventBus);

        ServiceLocator.Current.Register<SoundController>(_soundController);

        ServiceLocator.Current.Register<GUIHolder>(_guiHolder);

        ServiceLocator.Current.Register<ILevelLoader>(_levelLoader);
        ServiceLocator.Current.Register<IBalloonLoader>(_balloonDataLoader);
    }

    private void AddToDisposables()
    {
        _disposables.Add(_coinController);
        _disposables.Add(_scoreController);
    }

    private void OnDestroy()
    {
        foreach (var disposable in _disposables)
        {
            disposable.Dispose();
        }
    }
}
