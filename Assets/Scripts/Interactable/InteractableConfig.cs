using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InteractableConfig", menuName = "ScriptableObjects/InteractableConfig", order = 1)]
public class InteractableConfig : ScriptableObject
{
    [SerializeField] private List<InteractableData> _interactableData;
    public List<InteractableData> InteractableData => _interactableData;
}
