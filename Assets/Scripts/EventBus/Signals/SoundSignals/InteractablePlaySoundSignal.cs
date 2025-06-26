using UnityEngine;

namespace CustomEventBus.Signals
{
    public class InteractablePlaySoundSignal
    {
        public readonly Interactable Interactable;
        
        public InteractablePlaySoundSignal(Interactable interactable)
        {
            Interactable = interactable;
        }
    }
}