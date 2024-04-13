using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Turnpturn.UI
{
    public class UIClickButtonPlayer : MonoBehaviour
    {
        [SerializeField]
        private AudioSource _audioSource;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void PlayClick()
        {
            _audioSource.Play();
        }
    }
}