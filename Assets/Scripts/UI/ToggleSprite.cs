using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace CubeHopper.UI
{
    public class ToggleSprite : MonoBehaviour
    {
        [SerializeField] private string _toggleName;
        [SerializeField] private Sprite _spriteON, _spriteOFF;
        [SerializeField] private Image _image;
        [SerializeField] private UnityEvent<bool, string> _onToggleSwitch;
        public void ChangeSprite(bool value)
        {
            _image.sprite = value ? _spriteON : _spriteOFF;
            _onToggleSwitch.Invoke(value, _toggleName);
        }
    }
}
