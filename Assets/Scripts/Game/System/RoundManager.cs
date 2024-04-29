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
        private bool _isEnnemyDead;
        private bool _isInitState;
        private bool _isGameOver = false;

        public static event Action OnFightWin;
        public static event Action OnFightLoose;
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

            if (!_isFightFinished)
            {
                if (_isGameOver)
                {
                    _isFightFinished = true;
                    OnFightLoose?.Invoke();
                }
                else if (_isEnnemyDead)
                {
                    _isFightFinished = true;
                    OnFightWin?.Invoke();
                }
                else if (IsTurnFinish())
                {
                    Debug.Log($"End of {_currentUnit.UnitName}'s turn.");

                    NextTurn();
                }
            }
        }

        private void InitRoundManager()
        {
            _isFightFinished = false;
            _isEnnemyDead = false;
            OnFightStart?.Invoke(UnitList);
            NextTurn();
        }

        private void NextTurn()
        {
            
          
            _currentUnit = _playerSorter.GetNextPlayer();
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
            if (deadUnit.UnitType == Datas.UnitTypePrefabsData.UnitType.Ennemy)
            {
                _isEnnemyDead = true;
            }
            else
            {
                _isGameOver = true;
            }
        }
      

    }
}