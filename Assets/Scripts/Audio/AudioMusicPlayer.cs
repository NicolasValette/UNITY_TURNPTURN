using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Turnpturn.Audio
{
    public class AudioMusicPlayer : MonoBehaviour
    {
        [SerializeField]
        private AudioSource _audioPlayer;
        // Start is called before the first frame update
        void Start()
        {
            _audioPlayer.Play();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}