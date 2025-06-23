using System.Collections.Generic;
using CustomEventBus;
using CustomEventBus.Signals;
using UnityEngine;
using Random = UnityEngine.Random;

public class InteractableSpawner : MonoBehaviour, IService
{
    [SerializeField] private Transform _parent;
    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;
    [SerializeField] private float _defaultY;

    public float MinX => _minX;
    public float MaxX => _maxX;

    private readonly Dictionary<string, ObjectPool<Interactable>> _pools =
        new Dictionary<string, ObjectPool<Interactable>>();

    private EventBus _eventBus;

    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<SpawnInteractableSignal>(Spawn);
        _eventBus.Subscribe<DisposeInteractableSignal>(Dispose);
    }

    private void Spawn(SpawnInteractableSignal signal)
    {
        var interactable = signal.InteractablePrefab;
        var pool = GetPool(interactable);

        var item = pool.Get();
        item.transform.parent = _parent;
        item.transform.position = RandomizeSpawnPosition();

        _eventBus.Invoke(new InteractableActivatedSignal(item));
    }

    private void Dispose(DisposeInteractableSignal signal)
    {
        var interactable = signal.Interactable;
        var pool = GetPool(interactable);
        pool.Release(interactable);

        _eventBus.Invoke(new InteractableDisposedSignal(interactable));
    }

    private ObjectPool<Interactable> GetPool(Interactable interactable)
    {
        var objectTypeStr = interactable.GetType().ToString();
        ObjectPool<Interactable> pool;

        if (!_pools.ContainsKey(objectTypeStr))
        {
            pool = new ObjectPool<Interactable>(interactable, 5);
            _pools.Add(objectTypeStr, pool);
        }
        else
        {
            pool = _pools[objectTypeStr];
        }

        return pool;
    }

    private Vector3 RandomizeSpawnPosition()
    {
        float x = Random.Range(_minX, _maxX);
        return new Vector3(x, _defaultY, 0);
    }
}