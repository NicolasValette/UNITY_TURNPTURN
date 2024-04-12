using System;
using System.Collections;
using System.Collections.Generic;
using Turnpturn.Game.Elements;
using UnityEngine;

namespace Turnpturn.Game.System
{
    public class RoundManager : MonoBehaviour
    {
        [SerializeField]
        private PlayerSorter _playerSorter;

        private Unit _currentUnit;
        private bool _isFightFinished;
        private bool _isInitState;

        public static event Action OnFightWin;
        public static event Action OnNewTurn;
        public static event Action<List<Unit>> OnFightStart;

        public List<Unit> UnitList { get { return _playerSorter.UnitList; } }

        private void OnEnable()
        {
            Unit.OnDeath += UnitDeath;
        }
        private void OnDisable()
        {
            Unit.OnDeath -= UnitDeath;
        }
        // Start is called before the first frame update
        void Start()
        {
            _isInitState = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (_isInitState)
            {
                _isInitState = false;
                InitRoundManager();
            }
            else if (_isFightFinished)
            {
                OnFightWin?.Invoke();
            }
            else if (IsTurnFinish())
            {
                Debug.Log($"End of {_currentUnit.UnitName}'s turn.");
                NextTurn();
            }
        }

        private void InitRoundManager()
        {
            _isFightFinished = false;
            OnFightStart?.Invoke(UnitList);
            NextTurn();
        }

        private void NextTurn()
        {
            _currentUnit =  _playerSorter.GetNextPlayer();
            Debug.Log($"{_currentUnit.UnitName}'s turn.");
            OnNewTurn?.Invoke();
            _currentUnit.StartTurn();
        }
        private bool IsTurnFinish()
        {
            return _currentUnit.IsFinished;
        }

        // TODO A CHANGER
        private void UnitDeath(Unit deadUnit)
        {
            if (deadUnit.UnitName.Equals("Sephiroth"))
            {
                _isFightFinished = true;
            }
        }
        
    }
}