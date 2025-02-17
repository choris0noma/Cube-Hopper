using CubeHopper.SavingData;
using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace CubeHopper
{
    public class DailyReward : MonoBehaviour
    {
        private const string PATH = "/time.json";
        private const int INTERVAL_IN_SECONDS = 3 * 60 * 60;

        [SerializeField] private Button _claimButton;
        [SerializeField] private TextMeshProUGUI _timerText;
        private IDataService _dataService = new JsonDataService();
        public static Action<int> OnRewardClaimed;
        private DateTime _lastTimeRewardClaimed;
        private TimeSpan _timeLeft;

        private bool isEncrypted = true;
        private void Awake()
        {
            RetrieveLastTime();
            if (EnoughTimeHasPassed())
            {
                ActivateButton();
            }
            else
            {
                _timeLeft = TimeSpan.FromSeconds(INTERVAL_IN_SECONDS) - DateTime.Now.Subtract(_lastTimeRewardClaimed);
                DeactivateButton();
            }
        }
        private void ActivateButton()
        {
            _claimButton.image.color = new Color(1, 1, 1, 1);
            _claimButton.interactable = true;
            _timerText.text = "Free";
        }
        private void DeactivateButton()
        {
            _claimButton.image.color = new Color(1, 1, 1, 0.7f);
            _claimButton.interactable = false;
            DisplayTime();
            StartCoroutine(StartTimer());
        }
        public void ClaimReward()
        {
            if (EnoughTimeHasPassed())
            {
                OnRewardClaimed?.Invoke(UnityEngine.Random.Range(20, 50));
                _lastTimeRewardClaimed = DateTime.Now;
                SaveTimeOfLastReward();
                _timeLeft = TimeSpan.FromSeconds(INTERVAL_IN_SECONDS);
                DeactivateButton();
            }
        }

        private IEnumerator StartTimer()
        {
            while (_timeLeft.TotalSeconds > 0)
            {
                yield return new WaitForSecondsRealtime(1f);
                _timeLeft = _timeLeft.Subtract(TimeSpan.FromSeconds(1));
                DisplayTime();
            }
            ActivateButton();
        }

        private void DisplayTime()
        {
            _timerText.text = _timeLeft.ToString(@"hh\:mm\:ss");
        }
        private bool EnoughTimeHasPassed()
        {
            return DateTime.Now.Subtract(_lastTimeRewardClaimed).TotalSeconds >= INTERVAL_IN_SECONDS;
        }
        private void SaveTimeOfLastReward()
        {
            if (!_dataService.SaveData(PATH, _lastTimeRewardClaimed, isEncrypted))
            {
                Debug.LogError("Couldn't save time of last reward");
            }
        }
        private void RetrieveLastTime()
        {
            if (_dataService.CheckPathExistence(PATH))
            {
                _lastTimeRewardClaimed = _dataService.LoadData<DateTime>(PATH, isEncrypted);
            }
            else
            {
                ActivateButton();
            }

        }
    }
}
