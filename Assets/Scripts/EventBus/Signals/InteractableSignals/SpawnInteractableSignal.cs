namespace CustomEventBus.Signals
{
    public class SpawnInteractableSignal
    {
        public Interactable InteractablePrefab;
        public SpawnInteractableSignal(Interactable interactablePrefab)
        {
            InteractablePrefab = interactablePrefab;
        }
    }
}