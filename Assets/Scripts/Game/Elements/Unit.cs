using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Turnpturn.Game.Elements
{
    public class Unit : MonoBehaviour
    {
        [SerializeField]
        private string _unitName;

        private bool _isFinished;
        public bool IsFinished => _isFinished;
        public string UnitName => _unitName;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void StartTurn()
        {
            _isFinished = false;
            StartCoroutine(WaitTurn());
        }
        private IEnumerator WaitTurn()
        {
            yield return new WaitForSeconds(2f);
            EndTurn();
        }
        private void EndTurn()
        {
            _isFinished = true;
        }
    }
}