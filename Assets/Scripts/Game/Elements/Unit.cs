using System;
using System.Collections;
using System.Collections.Generic;
using Turnpturn.Datas;
using Turnpturn.Effects;
using Turnpturn.Game.Actions;
using Turnpturn.Interfaces.Effects;
using Turnpturn.Interfaces.Game;
using Turnpturn.Interfaces.Game.IA;
using UnityEngine;

namespace Turnpturn.Game.Elements
{

    public class Unit : MonoBehaviour, IDamageable
    {

        [Header("Description")]
        [SerializeField]
        protected string _unitName;
        [SerializeField]
        protected UnitData _unitData;
        [SerializeField]
        protected UnitTypePrefabsData.UnitType _unitType;
        [SerializeField]
        protected GameObject _actionPickerGameObject;

        [Header("Actions")]
        [SerializeField]
        protected List<Attack> _actions;

        [SerializeField]
        protected FightersManager _fighterManager;

        protected IPlayAnim _animPlayer;
        protected IChooseAction _actionPicker;

        public event Action<ActionType> OnAction;
        public static event Action<Unit> OnDeath;

        public List<Attack> Actions { get { return _actions; } }
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
        public UnitData UnitCarac => _unitData;

        public UnitTypePrefabsData.UnitType UnitType { get => _unitType; }
        // Start is called before the first frame update
        void Start()
        {
            InitUnit();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void InitUnit()
        {
            _animPlayer = GetComponent<IPlayAnim>();
            if (_actionPickerGameObject != null)
            {
                _actionPicker = _actionPickerGameObject.GetComponent<IChooseAction>();
            }
            CurrentHP = _unitData.MaxHealth;
        }

        public void StartTurn()
        {
            IsFinished = false;
            if (CurrentHP > 0)
            {
                _actionPicker.ChooseAction(this);
            }
            else
            {
                EndTurn();
            }
        }

        public void EndTurn()
        {
            IsFinished = true;
        }

        public void Wait(float waitingDuration, Action action)
        {
            StartCoroutine(WaitBeforeAction(waitingDuration, action));
        }
        protected IEnumerator WaitBeforeAction(float waitingDuration, Action action)
        {
            yield return new WaitForSeconds(waitingDuration);
            action();
        }

        public void TakeDamage(int amount)
        {
            //Animation
            if (_animPlayer != null)
            {
                _animPlayer.PlayAnim("TakeDmg");
            }
            //FX


            Debug.Log($"{_unitName}(HP :{CurrentHP}) take {amount} damages");
            CurrentHP -= amount;
            if (CurrentHP <= 0)
            {
                OnDeath?.Invoke(this);
            }
        }
        public void Attack(Attack attack)
        {
            Unit target = _fighterManager.GetOpponent(this);
            Attack(attack, target);
        }
        public void Attack(Attack attack, Unit target)
        {
            //Animation
            if (_animPlayer != null)
            {
                _animPlayer.PlayAnim("Attack");
            }
            //FX
            

            Debug.Log($"{_unitName}(HP :{CurrentHP}) attack {target.name}(HP :{target.CurrentHP})");
            //_attack.InflictDamge(target, _attack.AttackData.AttackDmg);
            ActionType act = attack.PerformAction(this, target);
            OnAction?.Invoke(act);
        }
    }
}