using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Turnpturn.Inputs
{
    public class AnimatorTest : MonoBehaviour
    {
        [SerializeField]
        private Renderer _renderer;
        private Animator _animator;
        private int _maxFlash = 5;

        // Start is called before the first frame update
        void Start()
        {
            _animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Keyboard.current.aKey.wasPressedThisFrame)
            {
                Debug.Log("a pressé : Attack");
                _animator.SetTrigger("Attack");
            }
            else if (Keyboard.current.zKey.wasPressedThisFrame)
            {
                Debug.Log("z pressé : Damage");
                _animator.SetTrigger("TakeDmg");
            }
            else if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                Debug.Log("e pressé : death");
                _animator.SetTrigger("Death");
            }
            else if (Keyboard.current.rKey.wasPressedThisFrame)
            {
                Debug.Log("r pressé : AttackStab");
                _animator.SetTrigger("AttackStab");
            }
            else if (Keyboard.current.tKey.wasPressedThisFrame)
            {
                Debug.Log("t pressé : AttackStab");
                StartCoroutine(FlashRed());
            }
            else if (Keyboard.current.yKey.wasPressedThisFrame)
            {
                Debug.Log("y pressé : AttackHeavy");
                _animator.SetTrigger("AttackHeavy");
            }
        }

        private IEnumerator FlashRed()
        {
            int numberOfFlash = 0;
            while (numberOfFlash <= _maxFlash)
            {
                _renderer.material.color = Color.red;
                yield return new WaitForSeconds(0.1f);
                _renderer.material.color = Color.white;
                yield return new WaitForSeconds(0.1f);
                numberOfFlash++;
            }
            _renderer.material.color = Color.white;
        }
    }
}