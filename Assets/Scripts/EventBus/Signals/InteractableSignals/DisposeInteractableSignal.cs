namespace CustomEventBus.Signals
{
    public class DisposeInteractableSignal
    {
        public readonly Interactable Interactable;

        public DisposeInteractableSignal(Interactable interactable)
        {
            Interactable = interactable;
        }
    }
}