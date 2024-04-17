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
            //var s = _animator.GetNextAnimatorClipInfo(0);


            //float l = _animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
            //string name = _animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
            //Debug.Log($"l = {l} / name {name}");
            
            
        }
    }
}