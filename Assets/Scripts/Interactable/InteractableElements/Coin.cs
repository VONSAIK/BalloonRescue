using CustomEventBus.Signals;
using UnityEngine;

public class Coin : Interactable
{
    [SerializeField] private int _coinValue = 1;
    protected override void Interact()
    {
        _eventBus.Invoke(new AddCoinSignal(_coinValue));
        _eventBus.Invoke(new AddScoreSignal(_coinValue * 10));
    }
}
