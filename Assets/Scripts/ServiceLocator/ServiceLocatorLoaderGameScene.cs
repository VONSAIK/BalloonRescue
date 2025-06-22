using UnityEngine;
using CustomEventBus;
using System.Collections.Generic;

public class ServiceLocatorLoaderGameScene : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private SOLevelLoader _SOLevelLoader;

    private EventBus _eventBus;
    private ConfigDataLoader _configDataLoader;
    private GameController _gameController;

    private ILevelLoader _levelLoader;


    private void Awake()
    {
        _eventBus = new EventBus();
        _gameController = new GameController();

        _levelLoader = _SOLevelLoader;

        RegisterServices();
        Initialize();
    }

    private void RegisterServices()
    {
        ServiceLocator.Init();

        ServiceLocator.Current.Register(_eventBus);
        ServiceLocator.Current.Register(_gameController);

        ServiceLocator.Current.Register<Player>(_player);
        ServiceLocator.Current.Register<PlayerMovement>(_playerMovement);

        ServiceLocator.Current.Register<ILevelLoader>(_levelLoader);
    }

    private void Initialize()
    {
        _playerMovement.Init();
        _gameController.Init();

        var loaders = new List<ILoader>();
        loaders.Add(_levelLoader);
        _configDataLoader = new ConfigDataLoader();
        _configDataLoader.Init(loaders);
    }
}
