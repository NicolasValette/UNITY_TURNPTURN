using System.Collections;
using System.Collections.Generic;
using Turnpturn.Audio;
using UnityEngine;
using UnityEngine.UI;

namespace Turnpturn.UI
{
    public class OptionsMenu : MonoBehaviour
    {
        [SerializeField]
        private AudioMusicPlayer _audioPlayer;
        [SerializeField]
        private Slider _masterSlider;
        [SerializeField]
        private Slider _musicSlider;
        [SerializeField]
        private Slider _sfxSlider;
        // Start is called before the first frame update
        void Start()
        {
            _masterSlider.value = _audioPlayer.MasterVolume;
            _musicSlider.value = _audioPlayer.MusicVolume;
            _sfxSlider.value = _audioPlayer.SFXVolume;
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void UpdateOptions()
        {
            _masterSlider.value = _audioPlayer.MasterVolume;
            _musicSlider.value = _audioPlayer.MusicVolume;
            _sfxSlider.value = _audioPlayer.SFXVolume;
        }
        public void SetMasterVolume(float volume)
        {
            _audioPlayer.SetMasterVolume(volume);
        }
        public void SetMusicVolume(float volume)
        {
            _audioPlayer.SetMusicVolume(volume);
        }
        public void SetSFXVolume(float volume)
        {
            _audioPlayer.SetSFXVolume(volume);
        }
    }
}