using UnityEngine;
using CustomEventBus;
using System.Collections.Generic;

public class ServiceLocatorLoaderGameScene : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private SOLevelLoader _SOLevelLoader;
    [SerializeField] private InteractableSpawner _interactableSpawner;
    [SerializeField] private InteractableMover _interactableMover;

    private EventBus _eventBus;
    private ConfigDataLoader _configDataLoader;
    private GameController _gameController;
    private LevelController _levelController;
    private SignalSpawner _signalSpawner;

    private ILevelLoader _levelLoader;


    private void Awake()
    {
        _eventBus = new EventBus();
        _gameController = new GameController();
        _signalSpawner = new SignalSpawner();
        _levelController = new LevelController();

        _levelLoader = _SOLevelLoader;

        RegisterServices();
        Initialize();
    }

    private void RegisterServices()
    {
        ServiceLocator.Init();

        ServiceLocator.Current.Register(_eventBus);
        ServiceLocator.Current.Register(_gameController);
        ServiceLocator.Current.Register(_levelController);
        ServiceLocator.Current.Register(_signalSpawner);

        ServiceLocator.Current.Register<Player>(_player);
        ServiceLocator.Current.Register<PlayerMovement>(_playerMovement);
        ServiceLocator.Current.Register<InteractableSpawner>(_interactableSpawner);
        ServiceLocator.Current.Register<InteractableMover>(_interactableMover);

        ServiceLocator.Current.Register<ILevelLoader>(_levelLoader);
    }

    private void Initialize()
    {
        _playerMovement.Init();
        _interactableMover.Init();
        _signalSpawner.Init();
        _interactableSpawner.Init();
        _gameController.Init();
        _levelController.Init();

        var loaders = new List<ILoader>();
        loaders.Add(_levelLoader);
        _configDataLoader = new ConfigDataLoader();
        _configDataLoader.Init(loaders);
    }
}
