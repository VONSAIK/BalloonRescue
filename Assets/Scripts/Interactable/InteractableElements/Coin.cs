using UnityEngine;

public class Coin : Interactable
{
    protected override void Interact()
    {
        Debug.Log("Take coin");
    }
}
