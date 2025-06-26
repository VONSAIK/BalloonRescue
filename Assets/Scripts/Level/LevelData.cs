using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelData
{
    [SerializeField] private int _levelId;

    [SerializeField] private float _levelLength;

    [SerializeField] private float _startSpeed;

    [SerializeField] private float _endSpeed;

    [SerializeField] private int _coinForPass;

    [SerializeField] private AudioClip _levelMusic;

    [SerializeField] private List<InteractableData> _interactableData;

    public int LevelId => _levelId;
    public float LevelLength => _levelLength;
    public float StartSpeed => _startSpeed;
    public float EndSpeed => _endSpeed;
    public int CoinForPass => _coinForPass;
    public AudioClip LevelMusic => _levelMusic;
    public List<InteractableData> InteractableData => _interactableData;
}
