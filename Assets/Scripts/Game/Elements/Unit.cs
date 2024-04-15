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
        protected UnitData _unitData;
        [SerializeField]
        protected UnitTypePrefabsData.UnitType _unitType;
        [SerializeField]
        protected GameObject _actionPickerGameObject;
        [SerializeField]
        protected GameObject _selectorGameObject;


        [SerializeField]
        protected FightersManager _fighterManager;

        protected List<Attack> _actions;

        protected IPlayAnim _animPlayer;
        protected IChooseAction _actionPicker;

        public event Action<ActionType> OnAction;
        public static event Action<Unit> OnDeath;
        public static event Action OnSelected;

        public List<Attack> Actions { get { return _actions; } }
        public int CurrentHP { 
            get => _unitData.CurrentHealth; 
            private set => _unitData.CurrentHealth = value;
        }
        public bool IsAlive
        {
            get
            {
                return _unitData.CurrentHealth >= 0;
            }
        }
        public bool IsFinished { get; private set; }
        public string UnitName => _unitData.UnitName;
        public UnitData UnitCarac => _unitData;

        public UnitTypePrefabsData.UnitType UnitType { get => _unitType; }

        private void OnEnable()
        {
            Unit.OnSelected += HideSelector;
        }
        private void OnDisable()
        {
            Unit.OnSelected -= HideSelector;
        }
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void InitUnit(UnitData data=null, int hp = -1)
        {
            _selectorGameObject.SetActive(false);
            _animPlayer = GetComponent<IPlayAnim>();
            if (data!=null)
            {
                _unitData = data;
            }
            if (_actionPickerGameObject != null)
            {
                _actionPicker = _actionPickerGameObject.GetComponent<IChooseAction>();
            }
            CurrentHP = (hp <= -1)?_unitData.MaxHealth:hp;

            if (data!=null)
            {
                SetAttack(data.Attacks);
            }
            else
            {
                Debug.LogError($"Missing attack {UnitName}");
            }
        }

        public void SetAttack(List<Attack> attacks)
        {
            if (attacks == null || attacks.Count == 0)
            {
                Debug.LogError($"Missing attack {UnitName}"); 
            }
            _actions = new List<Attack>(attacks);
            for (int i=0; i<attacks.Count;i++)
            {
                attacks[i].Init();
            }
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
               // EndTurn();
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
          
            //FX

            Debug.Log($"{UnitName}(HP :{CurrentHP}) take {amount} damages");
            CurrentHP -= amount;

            //Animation
            if (CurrentHP <= 0)
            {
                if (_animPlayer != null)
                {
                    _animPlayer.PlayAnim("Death");
                }
                StartCoroutine(WaitBeforeDeath(1f));
            }
            else 
            {
                if (_animPlayer != null)
                {
                    _animPlayer.PlayAnim("TakeDmg");
                }
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
            

            Debug.Log($"{UnitName}(HP :{CurrentHP}) attack {target.name}(HP :{target.CurrentHP})");
            //_attack.InflictDamge(target, _attack.AttackData.AttackDmg);
            ActionType act = attack.PerformAction(this, target);
            OnAction?.Invoke(act);
        }
        private IEnumerator WaitBeforeDeath (float waitingTime)
        {
            yield return new WaitForSeconds(waitingTime);
            OnDeath?.Invoke(this);
        }

        public void HideSelector()
        {
            _selectorGameObject.SetActive(false);

        }
        public void ShowSelector()
        {
            OnSelected?.Invoke();
            _selectorGameObject.SetActive(true);
        }

        private void OnMouseEnter()
        {
            ShowSelector();
        }
        private void OnMouseExit()
        {
            HideSelector();   
        }
    }
}