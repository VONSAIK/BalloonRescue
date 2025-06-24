using CustomEventBus.Signals;
using UnityEngine;

public class Coin : Interactable
{
    [SerializeField] private int _coinValue = 10;
    protected override void Interact()
    {
        _eventBus.Invoke(new AddScoreSignal(_coinValue));
    }
}
