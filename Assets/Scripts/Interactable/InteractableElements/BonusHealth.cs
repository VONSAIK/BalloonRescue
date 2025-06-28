using CustomEventBus.Signals;
using UnityEngine;

public class BonusHealth : Interactable
{
    [SerializeField] private int _healthValue = 1;
    protected override void Interact()
    {
        _eventBus.Invoke(new AddHealthSignal(_healthValue));
    }
}
