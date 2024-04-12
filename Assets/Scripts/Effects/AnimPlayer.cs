using System.Collections;
using System.Collections.Generic;
using Turnpturn.Interfaces.Effects;
using UnityEngine;

namespace Turnpturn.Effects
{
    public class AnimPlayer : MonoBehaviour, IPlayAnim
    {
        [SerializeField]
        private Animator _animator;


        public void PlayAnim(string trigger)
        {
            _animator.SetTrigger(trigger);
        }
    }
}