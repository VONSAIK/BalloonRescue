using UnityEngine;

public class Box : Interactable
{
    protected override void Interact()
    {
        Debug.Log("Take damage");
    }
}
