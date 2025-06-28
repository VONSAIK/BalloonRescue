using CustomEventBus.Signals;
using UnityEngine;

public class Diamon : Interactable
{
    [SerializeField] private int _diamonValue = 30;
    protected override void Interact()
    {
        _eventBus.Invoke(new AddScoreSignal(_diamonValue));
    }
}
