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
using static UnityEngine.GraphicsBuffer;

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
        protected Transform _damageTextSpawnPos;


        [SerializeField]
        protected FightersManager _fighterManager;
        [SerializeField]
        protected float _waitingTimeBetweenTurn;
        
        private Renderer _renderer;
        private int _maxFlash = 3;

        protected List<Attack> _actions;

        protected IPlayAnim _animPlayer;
        protected IChooseAction _actionPicker;
        private Unit _currentTarget;

        private Attack _currentAction;
        private bool _isActionPickFInishFinished = false;
        private bool _isActionComplete = false;

        public event Action<ActionType> OnAction;
        public event Action OnHealthModified;
        public event Action OnTargetActionComplete;
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
        public bool IsFinished { get { return _isActionComplete && _isActionComplete; } }
        public string UnitName => _unitData.UnitName;
        public UnitData UnitCarac => _unitData;
        public Transform DamageTestSpawnPos { get => _damageTextSpawnPos; }

        public UnitTypePrefabsData.UnitType UnitType { get => _unitType; }
        public ElementalTypeData UnitElement { get => UnitCarac.Element; }

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
            _renderer = GetComponentInChildren<Renderer>();
            if (_renderer == null)
            {
                Debug.LogError("Render missing on " + name + " object");
            }
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
            _isActionPickFInishFinished = false;
            _isActionComplete = false;
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
            _isActionPickFInishFinished = true;
        }
        public void ActionComplete()
        {
            _isActionComplete = true;
            _currentTarget.OnTargetActionComplete -= ActionComplete;
        }
        public void TargetActionComplete()
        {
            OnTargetActionComplete?.Invoke();
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
            StartCoroutine(FlashRed());
            //if (CurrentHP <= 0)
            //{
            //    if (_animPlayer != null)
            //    {
            //        _animPlayer.PlayAnim("Death");
            //    }
            //    StartCoroutine(WaitBeforeDeath(1f));
            //}
            //else 
            //{
               
             
            //}
        }
        public void DamageTaken()
        {
            OnHealthModified?.Invoke();
        }
        public void Attack(Attack attack)
        {
            Unit target = _fighterManager.GetOpponent(this);
            Attack(attack, target);
        }
        public void Attack(Attack attack, Unit target)
        {
            _currentAction = attack;
            //Animation
            if (_animPlayer != null)
            {
                _animPlayer.PlayAnim("Attack");
                
            }
            //FX
            

        }
        public void AttackAfterAnimation()
        {
            Unit target = _fighterManager.GetOpponent(this);
            Debug.Log($"{UnitName}(HP :{CurrentHP}) attack {target.name}(HP :{target.CurrentHP})");
            //_attack.InflictDamge(target, _attack.AttackData.AttackDmg);
            target.OnTargetActionComplete += ActionComplete;
            _currentTarget = target;
            ActionType act = _currentAction.PerformAction(this, target);
            OnAction?.Invoke(act);
            Wait(_waitingTimeBetweenTurn, EndTurn);
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
        private IEnumerator FlashRed()
        {
            int numberOfFlash = 0;
            while (numberOfFlash <= _maxFlash)
            {
                _renderer.material.color = Color.red;
                yield return new WaitForSeconds(0.1f);
                _renderer.material.color = Color.white;
                yield return new WaitForSeconds(0.1f);
                numberOfFlash++;
            }
            _renderer.material.color = Color.white;
            DamageTaken();
            if (CurrentHP <= 0)
            {
                if (_animPlayer != null)
                {
                    _animPlayer.PlayAnim("Death");
                }
                StartCoroutine(WaitBeforeDeath(1f));
            }
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