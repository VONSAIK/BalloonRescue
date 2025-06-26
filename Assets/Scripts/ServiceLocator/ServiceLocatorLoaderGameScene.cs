using UnityEngine;
using CustomEventBus;
using System.Collections.Generic;
using UI;

public class ServiceLocatorLoaderGameScene : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerVisual _playerVisual;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private SOLevelLoader _SOLevelLoader;
    [SerializeField] private SOBalloonLoader _SOBalloonLoader;
    [SerializeField] private InteractableSpawner _interactableSpawner;
    [SerializeField] private InteractableMover _interactableMover;
    [SerializeField] private SoundController _soundController;

    [SerializeField] private GUIHolder _GUIHolder;

    [SerializeField] private HUD _HUD;
    [SerializeField] private HealthBar _healthBar;

    private EventBus _eventBus;
    private ConfigDataLoader _configDataLoader;
    private GameController _gameController;
    private CoinController _coinController;
    private ScoreController _scoreController;
    private LevelController _levelController;
    private SignalSpawner _signalSpawner;

    private ILevelLoader _levelLoader;
    private IBalloonLoader _balloonLoader;

    private List<IDisposable> _disposables = new List<IDisposable>();

    private void Awake()
    {
        _eventBus = new EventBus();
        _signalSpawner = new SignalSpawner();
        _coinController = new CoinController();
        _scoreController = new ScoreController();
        _gameController = new GameController();
        _levelController = new LevelController();

        _configDataLoader = new ConfigDataLoader();

        _levelLoader = _SOLevelLoader;
        _balloonLoader = _SOBalloonLoader;

        RegisterServices();
        Initialize();
        AddDisposables();
    }

    private void RegisterServices()
    {
        ServiceLocator.Init();

        ServiceLocator.Current.Register(_eventBus);
        ServiceLocator.Current.Register(_signalSpawner);
        ServiceLocator.Current.Register(_gameController);
        ServiceLocator.Current.Register(_coinController);
        ServiceLocator.Current.Register( _scoreController);
        ServiceLocator.Current.Register(_levelController);

        ServiceLocator.Current.Register<Player>(_player);
        ServiceLocator.Current.Register<PlayerVisual>(_playerVisual);
        ServiceLocator.Current.Register<PlayerMovement>(_playerMovement);
        ServiceLocator.Current.Register<InteractableSpawner>(_interactableSpawner);
        ServiceLocator.Current.Register<InteractableMover>(_interactableMover);
        ServiceLocator.Current.Register<SoundController>(_soundController);

        ServiceLocator.Current.Register<GUIHolder>(_GUIHolder);

        ServiceLocator.Current.Register<ILevelLoader>(_levelLoader);
        ServiceLocator.Current.Register<IBalloonLoader>(_balloonLoader);
    }

    private void Initialize()
    {
        _playerMovement.Init();
        _interactableMover.Init();
        _signalSpawner.Init();
        _interactableSpawner.Init();
        _player.Init();
        _playerVisual.Init();
        _gameController.Init();
        _coinController.Init();
        _scoreController.Init();
        _levelController.Init();
        _soundController.Init();

        _HUD.Init();
        _healthBar.Init();

        var loaders = new List<ILoader>();
        loaders.Add(_levelLoader);
        loaders.Add(_balloonLoader);
        _configDataLoader.Init(loaders);
    }

    private void AddDisposables()
    {
        _disposables.Add(_configDataLoader);
        _disposables.Add(_gameController);
        _disposables.Add(_coinController);
        _disposables.Add(_scoreController);
        _disposables.Add(_levelController);
        _disposables.Add(_signalSpawner);
    }

    private void OnDestroy()
    {
        foreach (var disposable in _disposables)
        {
            disposable.Dispose();
        }
    }
}
