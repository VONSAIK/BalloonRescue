namespace CustomEventBus.Signals
{
    public class InteractableActivatedSignal
    {
        public readonly Interactable Interactable;

        public InteractableActivatedSignal(Interactable interactable)
        {
            Interactable = interactable;
        }
    }
}