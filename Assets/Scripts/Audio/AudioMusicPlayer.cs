using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Turnpturn.Audio
{
    public class AudioMusicPlayer : MonoBehaviour
    {
        public enum MixerGroup
        {
            Master,
            Music,
            SFX
        }
        [SerializeField]
        private AudioMixer _audioMixer;
        [SerializeField]
        private AudioSource _audioPlayer;
   
        // Start is called before the first frame update

        public float MasterVolume { get; private set; }
        public float SFXVolume { get; private set; }
        public float MusicVolume { get; private set; }
        private void Awake()
        {

           
                MasterVolume = PlayerPrefs.GetFloat("MasterVolume");
                MusicVolume = PlayerPrefs.GetFloat("MusicVolume");
                SFXVolume = PlayerPrefs.GetFloat("SFXVolume");

            SetVolume(MixerGroup.Master, MasterVolume);
            SetVolume(MixerGroup.Music, MusicVolume);
            SetVolume(MixerGroup.SFX, SFXVolume);



        }
        void Start()
        {
            _audioPlayer.Play();
           
           
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



        public void SetMasterVolume(float volume)
        {
            Debug.Log("Mastervolume : " + volume);
            SetVolume(MixerGroup.Master, volume);
        }
        public void SetMusicVolume(float volume)
        {
            Debug.Log("Musicvolume : " + volume);
            SetVolume(MixerGroup.Music, volume);
        }
        public void SetSFXVolume(float volume)
        {
            Debug.Log("SFXvolume : " + volume);
            SetVolume(MixerGroup.SFX, volume);
        }

        public void SetVolume(MixerGroup group, float volume)
        {
            string parameter = "";
            if (group == MixerGroup.Master)
            {
                MasterVolume = volume;
                parameter = "MasterVolume";
            }
            else if (group == MixerGroup.Music)
            {
                MusicVolume = volume;
                parameter = "MusicVolume";
            }
            else if (group == MixerGroup.SFX)
            {
                SFXVolume = volume;
                parameter = "SFXVolume";
            }
            _audioMixer.SetFloat(parameter, volume);
            PlayerPrefs.SetFloat(parameter, volume);
        }
    }

}