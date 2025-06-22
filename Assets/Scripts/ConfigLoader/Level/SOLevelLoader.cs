using System.Collections.Generic;
using CustomEventBus;
using CustomEventBus.Signals;
using UnityEngine;

public class SOLevelLoader : MonoBehaviour, ILevelLoader
{
    [SerializeField] private LevelsConfig _levelsConfig;

    public IEnumerable<LevelData> GetLevels()
    {
        return _levelsConfig.LevelsData;
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