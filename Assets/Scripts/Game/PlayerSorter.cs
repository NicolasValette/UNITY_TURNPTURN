using System.Collections;
using System.Collections.Generic;
using Turnpturn.Game.Elements;
using UnityEngine;

namespace Turnpturn.Game
{
    public class PlayerSorter : MonoBehaviour
    {
        [SerializeField]
        private List<Unit> _unitList;

        private int _current = -1;
        // Start is called before the first frame update
        void Start()
        {
            _current = -1;
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void Play()
        {

        }
        public Unit GetNextPlayer()
        {
            _current = (_current + 1)% _unitList.Count;
            return _unitList[_current];
        }
    }
}