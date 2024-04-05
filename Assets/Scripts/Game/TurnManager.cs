using System.Collections;
using System.Collections.Generic;
using Turnpturn.Game.Elements;
using UnityEngine;

namespace Turnpturn.Game
{
    public class TurnManager : MonoBehaviour
    {
        [SerializeField]
        private PlayerSorter _playerSorter;

        private Unit _currentUnit;
        // Start is called before the first frame update
        void Start()
        {
            NextTurn();
        }

        // Update is called once per frame
        void Update()
        {
            if (IsTurnFinish()) NextTurn();    
        }

        private void NextTurn()
        {
            _currentUnit =  _playerSorter.GetNextPlayer();
            _currentUnit.StartTurn();
            Debug.Log($"Turn of player : {_currentUnit.UnitName}");
        }
        private bool IsTurnFinish()
        {
            return _currentUnit.IsFinished;
        }
        
    }
}