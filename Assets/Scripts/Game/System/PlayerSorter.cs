using System;
using System.Collections;
using System.Collections.Generic;
using Turnpturn.Game.Elements;
using UnityEngine;

namespace Turnpturn.Game.System
{
    public class PlayerSorter : MonoBehaviour
    {

        [SerializeField]
        FightersManager _fightersManager;


        private int _current = -1;
        private int _roundNumber = -1;

        private Queue<Unit> _sortedQueue;
       

        public List<Unit> UnitList { get; private set; }
        public bool IsRoundOver
        {
            get
            {
                return _sortedQueue.Count <= 0;
            }
        }

        public static event Action<int> OnNewRound;
        private void OnEnable()
        {
        }
        // Start is called before the first frame update
        void Start()
        {
            Init();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void Init()
        {
            UnitList = _fightersManager.GetUnitListFromFighter();
            _sortedQueue = new Queue<Unit>();
            _current = -1;
            _roundNumber = 0;
            PrepareNextRound();
        }
        public void Play()
        {

        }
        private void PrepareNextRound()
        {
            _roundNumber++;
            Debug.Log($"!!!!! New Round {_roundNumber} !!!!!");
            OnNewRound?.Invoke(_roundNumber++);
            for (int i = 0; i < UnitList.Count; i++) 
            {
                _sortedQueue.Enqueue(UnitList[i]);
            }
        }
        public Unit GetNextPlayer()
        {

            if (_sortedQueue.TryDequeue(out Unit unitTMP))
            {
                return unitTMP;
            }
            else
            {
                PrepareNextRound();
                return _sortedQueue.Dequeue();
            }
            
        }
    }
}