using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractableIndicator : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _textField;

    public void SetData(InteractableDataSO interactableData)
    {
        _image.sprite = interactableData.Image;
        _textField.SetText(interactableData.Description);
    }
}