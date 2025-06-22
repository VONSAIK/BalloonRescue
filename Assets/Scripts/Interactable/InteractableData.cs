using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InteractableData 
{
    [SerializeField] private Interactable _interactablePrefab;

    [SerializeField] private float _startCooldown;

    [SerializeField] private float _endCooldown;

    [SerializeField] private float _prewarmTime;

    public Interactable Prefab => _interactablePrefab;
    public float StartCooldown => _startCooldown;
    public float EndCooldown => _endCooldown;
    public float PrewarmTime => _prewarmTime;
}
