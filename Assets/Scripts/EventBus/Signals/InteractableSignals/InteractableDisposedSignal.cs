namespace CustomEventBus.Signals
{
    public class InteractableDisposedSignal
    {
        public readonly Interactable Interactable;

        public InteractableDisposedSignal(Interactable interactable)
        {
            Interactable = interactable;
        }
    }
}