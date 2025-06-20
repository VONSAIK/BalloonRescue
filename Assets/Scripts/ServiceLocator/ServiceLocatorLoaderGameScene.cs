using UnityEngine;
using CustomEventBus;

public class ServiceLocatorLoaderGameScene : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;

    private EventBus _eventBus;
    private GameController _gameController;

    private void Awake()
    {
        _eventBus = new EventBus();
        _gameController = new GameController();

        RegisterServices();
        Initialize();
    }

    private void RegisterServices()
    {
        ServiceLocator.Init();

        ServiceLocator.Current.Register(_eventBus);
        ServiceLocator.Current.Register(_gameController);

        ServiceLocator.Current.Register<PlayerMovement>(_playerMovement);
    }

    private void Initialize()
    {
        _playerMovement.Init();
        _gameController.Init();
    }
}
