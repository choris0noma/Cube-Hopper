using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace CubeHopper.Audio
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _audio, _music;
        public static AudioManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            SetMuteStateAudio(PlayerPrefs.GetInt("Sound", 1) == 1);
            SetMuteStateMusic(PlayerPrefs.GetInt("Music", 1) == 1);
        }

        public void PlayAudio(AudioClip clip)
        {
            if (_audio.enabled)
            {
                _audio.PlayOneShot(clip);
            }
        }

        public void SetMuteStateMusic(bool _state)
        {
            if (_music.enabled)
            {
                if (_state) _music.Play();
                else _music.Stop();
            }
        }

        public void SetMuteStateAudio(bool _state)
        {
            if (_audio.enabled)
            {
                _audio.mute = !_state;
            }
        }
    }
}
