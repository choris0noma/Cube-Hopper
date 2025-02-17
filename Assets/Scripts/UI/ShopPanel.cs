using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CubeHopper.UI
{
    public class ShopPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI priceText;
        [SerializeField] private Image imageDisplay;
        [SerializeField] private Button panel, priceTag;
        [SerializeField] private Color _equipped, _unequipped;
        [SerializeField] private int _index;
        public bool isPurchased
        {
            set
            {
                priceTag.gameObject.SetActive(!value);
            }
        }
        public bool isEquipped
        {
            set
            {
                panel.image.color = value? _equipped: _unequipped;
            }
        }

        public bool isInteractable
        {
            set
            {
                panel.interactable = value;
                priceTag.interactable = value;
            }
        }
        
        public int Price
        {
            set
            {
                priceText.text = value.ToString();
            }
        }
        public Sprite Image
        {
            set
            {
                imageDisplay.sprite = value;
            }
        }
        public void AddButtonEvent(Shop shopListener,int index)
        {
            _index = index;
            panel.onClick.AddListener(() => shopListener.EquipeSkin(_index));
            priceTag.onClick.AddListener(() => shopListener.PurchaseSkin(_index));

        }
    }
}
