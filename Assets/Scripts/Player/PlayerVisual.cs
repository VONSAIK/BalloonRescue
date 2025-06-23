using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomEventBus;
using CustomEventBus.Signals;

public class PlayerVisual : MonoBehaviour, IService
{
    private SpriteRenderer _spriteRenderer;

    private EventBus _eventBus;
    
    public void Init()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _eventBus = ServiceLocator.Current.Get<EventBus>();

        _eventBus.Subscribe<AllDataLoadedSignal>(OnDataLoaded);
    }

    private void OnDataLoaded(AllDataLoadedSignal signal)
    {
        var balloonDataLoader = ServiceLocator.Current.Get<IBalloonLoader>();
        var balloonData = balloonDataLoader.GetCurrentBalloonData();
        _spriteRenderer.sprite = balloonData.BalloonSprite;
    }

    private void OnDestroy()
    {
        _eventBus.Unsubscribe<AllDataLoadedSignal>(OnDataLoaded);
    }
}
