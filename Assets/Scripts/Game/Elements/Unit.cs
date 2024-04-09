using System;
using System.Collections;
using System.Collections.Generic;
using Turnpturn.Datas;
using Turnpturn.Game.Actions;
using Turnpturn.Interfaces.Game;
using UnityEngine;

namespace Turnpturn.Game.Elements
{

    public class Unit : MonoBehaviour, IDamageable
    {

        [Header("Description")]
        [SerializeField]
        private string _unitName;
        [SerializeField]
        private UnitData _unitData;

        [Header("Actions")]
        [SerializeField]
        private Attack _attack;

        [SerializeField]
        private Unit _testtarget;

        public event Action <ActionType> OnAction;
        public static event Action<Unit> OnDeath;
        public int CurrentHP { get; private set; }
        public bool IsAlive
        {
            get
            {
                return CurrentHP >= 0;
            }
        }
        public bool IsFinished { get; private set; }
        public string UnitName => _unitName;

        // Start is called before the first frame update
        void Start()
        {
            InitUnit();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void InitUnit()
        {
            CurrentHP = _unitData.MaxHealth;
        }

        public void StartTurn()
        {
            IsFinished = false;
            if (CurrentHP > 0) 
            {
                StartCoroutine(WaitTurn());
            }
            else
            {
                EndTurn();
            }
        }
        private IEnumerator WaitTurn()
        {
            Attack();
            yield return new WaitForSeconds(2f);
            EndTurn();
        }
        private void EndTurn()
        {
            IsFinished = true;
        }

        public void TakeDamage(int amount)
        {
            Debug.Log($"{_unitName}(HP :{CurrentHP}) take {amount} damages");
            CurrentHP -= amount;
            if (CurrentHP <= 0)
            {
                OnDeath?.Invoke(this);
            }
        }
        public void Attack()
        {
            Debug.Log($"{_unitName}(HP :{CurrentHP}) attack {_testtarget.name}(HP :{_testtarget.CurrentHP})");
            _attack.InflictDamge(_testtarget, _attack.AttackData.AttackDmg);
            ActionType act = new ActionType(ActionType.ActType.Attack, this, _testtarget, _attack.AttackData.AttackDmg);
            OnAction?.Invoke(act);
        }
    }
}