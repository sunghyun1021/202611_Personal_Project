using UnityEngine;
using UnityEngine.UI;

public class CellView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _activeSprite;
    [SerializeField] private Sprite _inactiveSprite;

    public void SetActive(bool active)
    {
        _image.sprite = active ? _activeSprite : _inactiveSprite;
    }
}
