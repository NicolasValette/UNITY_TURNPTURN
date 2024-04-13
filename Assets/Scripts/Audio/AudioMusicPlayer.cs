using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Turnpturn.Audio
{
    public class AudioMusicPlayer : MonoBehaviour
    {
        [SerializeField]
        private AudioSource _audioPlayer;
        [SerializeField]
        private Slider _volumeSlider;
        // Start is called before the first frame update
        void Start()
        {
            _audioPlayer.Play();
            _volumeSlider.value = _audioPlayer.volume;
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void Play(AudioClip clip)
        {
            _audioPlayer.Stop();
            _audioPlayer.clip = clip;
            _audioPlayer.Play();
        }

        public void SetVolume(float volume)
        {
            _audioPlayer.volume = volume;
        }
    }

}