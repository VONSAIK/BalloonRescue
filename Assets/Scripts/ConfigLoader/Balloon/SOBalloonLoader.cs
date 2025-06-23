using CustomEventBus.Signals;
using CustomEventBus;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class SOBalloonLoader : MonoBehaviour, IBalloonLoader
{
    [SerializeField] private BalloonConfig _config;

    public IEnumerable<BalloonData> GetBalloonsData()
    {
        return _config.BalloonsData;
    }

    public BalloonData GetCurrentBalloonData()
    {
        var id = PlayerPrefs.GetInt(StringConstants.SELECTED_BALLOON, 1);
        return _config.BalloonsData.FirstOrDefault(x => x.BalloonId == id);
    }

    public bool IsLoaded()
    {
        return true;
    }

    public void Load()
    {
        var eventBus = ServiceLocator.Current.Get<EventBus>();
        eventBus.Invoke(new DataLoadedSignal(this));
    }

    public bool IsLoadingInstant()
    {
        return true;
    }
}
