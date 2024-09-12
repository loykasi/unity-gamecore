using UnityEngine;

[CreateAssetMenu(fileName = "InteractableData", menuName = "Interaction/Interactable Data", order = 0)]
public class InteractableDataSO : ScriptableObject
{
    public Sprite Image;
    public string Description;
    public Vector3 Offset;
}