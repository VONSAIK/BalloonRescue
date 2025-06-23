using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BalloonDataConfig", menuName = "ScriptableObjects/BalloonDataConfig", order = 1)]
public class BalloonConfig : ScriptableObject
{
    [SerializeField] private List<BalloonData> _balloonsData;

    public List<BalloonData> BalloonsData => _balloonsData;
}
    
