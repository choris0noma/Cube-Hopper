using CubeHopper.Audio;
using CubeHopper.Game;
using CubeHopper.SavingData;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CubeHopper.UI
{
    public class Shop : MonoBehaviour
    {
        private const string FILE_PATH = "/s_data.json";
        [SerializeField] private AudioClip _ClickSound;
        [SerializeField] private GameObject shopPanel;
        [SerializeField] private PlayerDataManager _playerData; 
        [SerializeField] private SkinItem[] skinList;
        [SerializeField] private ShopPanel[] skinPanels;

        private Vector3 _startPosition = new Vector3(0, -1920, 0);
        private Vector3 _endPosition = Vector3.zero;
        private IDataService _dataService = new JsonDataService();
        private List<int> _purchasedItems = new List<int> { 0 };
        private Player _player;
        private bool isEncrypted = true;
        private void OnEnable()
        {
            CoinRewarded.OnMoneyRewardGiven += CheckAvailableSkins;
        }
        private void OnDisable()
        {
            CoinRewarded.OnMoneyRewardGiven -= CheckAvailableSkins;
        }

        private void Awake()
        {
            shopPanel.transform.localPosition = _startPosition;
            _player = FindObjectOfType<Player>();
        }
        private void Start()
        {
            LoadPurchasedSkinsData();
            LoadSkins();
            AddButtonEvents();
            LoadPurchasedSkins();
            LoadCurrentSkin();
            CheckAvailableSkins();
        }
        private void AddButtonEvents()
        {
            for (int i =0; i< skinList.Length; i++)
            {
                skinPanels[i].AddButtonEvent(this, i);
            }
        }
        private void LoadCurrentSkin()
        {
            int currentSkin = PlayerPrefs.GetInt("CurrentSkin", 0);
            
            if (!_purchasedItems.Contains(currentSkin))
            {
                currentSkin = 0;
            }

            skinPanels[currentSkin].isEquipped = true;
            _player.SetSkin(skinList[currentSkin].IdleSprite, skinList[currentSkin].FlySprite, skinList[currentSkin].TraceColor);
        }
        private void LoadSkins()
        {
            skinList = skinList.OrderBy(skin => skin.Price).ToArray();
            for (int i = 0; i < skinList.Length; i++) 
            {
                skinPanels[i].Price = skinList[i].Price;
                skinPanels[i].Image = skinList[i].IdleSprite;
            }
        }
        private void LoadPurchasedSkins()
        {
            foreach (int index in _purchasedItems)
            {
                skinPanels[index].isPurchased = true;
            }
        }
        public void CheckAvailableSkins(int m = 0)
        {
            for (int i = 0; i< skinList.Length; i++)
            {
                if (!_purchasedItems.Contains(i))
                {
                    skinPanels[i].isInteractable = skinList[i].Price <= _playerData.Money;
                }
            }
        }
        public void PurchaseSkin(int index)
        {
            int price = skinList[index].Price;
            if (_playerData.Money >= price)
            {
                _playerData.ExecutePurchase(price);
                EquipeSkin(index);
                skinPanels[index].isPurchased = true;
                _purchasedItems.Add(index);
                SavePurchasedSkinsData();
                CheckAvailableSkins();
            }
        }
        public void EquipeSkin(int index)
        {
            if (_purchasedItems.Contains(index))
            {
                AudioManager.Instance.PlayAudio(_ClickSound);
                PlayerPrefs.SetInt("CurrentSkin", index);
                _player.SetSkin(skinList[index].IdleSprite, skinList[index].FlySprite, skinList[index].TraceColor);
                foreach (ShopPanel skin in skinPanels) skin.isEquipped = false; 
                skinPanels[index].isEquipped = true;
            }
        }
        
        public void Open()
        {
            shopPanel.SetActive(true);
            CheckAvailableSkins();
            AudioManager.Instance.PlayAudio(_ClickSound);
            shopPanel.transform.LeanMoveLocal(_endPosition, 0.5f).setEaseOutQuad();
        }
        public void Close()
        {
            AudioManager.Instance.PlayAudio(_ClickSound);
            shopPanel.transform.LeanMoveLocal(_startPosition, 0.5f).setEaseOutQuad().setOnComplete(() =>
            {
                shopPanel.SetActive(false);
            });
        }
        private void SavePurchasedSkinsData()
        {
            if (!_dataService.SaveData(FILE_PATH, _purchasedItems, isEncrypted))
            {
                Debug.LogError("Cccc");
            }
        }

        private void LoadPurchasedSkinsData()
        {
            if (_dataService.CheckPathExistence(FILE_PATH))
            {
                _purchasedItems = _dataService.LoadData<List<int>>(FILE_PATH, isEncrypted);
            }
            else 
            {
                SavePurchasedSkinsData();
            }
        }
    }
}
