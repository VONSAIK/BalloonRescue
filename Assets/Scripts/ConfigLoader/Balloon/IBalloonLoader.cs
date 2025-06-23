using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBalloonLoader : IService, ILoader
{
    public IEnumerable<BalloonData> GetBalloonsData();
    public BalloonData GetCurrentBalloonData();
}