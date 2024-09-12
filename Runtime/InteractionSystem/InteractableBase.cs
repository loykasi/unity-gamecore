using UnityEngine;

public class InteractableBase : MonoBehaviour, IInteractable
{
    public bool CanInteract = true;
    public InteractableDataSO InteractableData;

    public virtual void Interact()
    {
        
    }
}