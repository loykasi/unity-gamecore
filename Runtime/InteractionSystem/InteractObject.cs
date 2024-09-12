using UnityEngine;
using UnityEngine.Events;

public class InteractObject : InteractableBase
{
    public UnityEvent OnInteract;

    public override void Interact()
    {
        OnInteract?.Invoke();
    }
}