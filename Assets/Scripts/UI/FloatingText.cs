using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Turnpturn.UI
{
    public class FloatingText : MonoBehaviour
    {
        [SerializeField]
        private float _lifetime = 5f;
        // Start is called before the first frame update
        void Start()
        {
            Destroy(gameObject, _lifetime);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}