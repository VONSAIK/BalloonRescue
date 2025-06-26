using UnityEngine;
using CustomEventBus;
using CustomEventBus.Signals;

public abstract class Interactable : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";

    protected EventBus _eventBus;
    protected abstract void Interact();

    protected void Start()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals(PLAYER_TAG))
        {
            Interact();
            PlaySound();
            Dispose();
        }
    }

    private void Dispose()
    {
        _eventBus.Invoke(new DisposeInteractableSignal(this));
    }

    private void PlaySound()
    {
        _eventBus.Invoke(new InteractablePlaySoundSignal(this));
    }
}
