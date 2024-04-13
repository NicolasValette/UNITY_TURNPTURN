using System.Collections;
using System.Collections.Generic;
using Turnpturn.Game.Actions;
using Turnpturn.Game.Elements;
using UnityEngine;

namespace Turnpturn.Datas
{

    [CreateAssetMenu(menuName = "Data/New Unit")]
    public class UnitData : ScriptableObject
    {
        [SerializeField]
        protected string _unitName;
        [SerializeField]
        private int _maxHealth;
        [SerializeField]
        protected List<Attack> _attacks;
        [SerializeField]
        protected GameObject _prefabUnit;


        public int MaxHealth { get => _maxHealth; }
        public string UnitName 
        { 
            get => _unitName; 
            set => _unitName = value;
        }
        public List<Attack> Attacks { get => _attacks; }

        public GameObject PrefabUnit { get => _prefabUnit; }
        public int CurrentHealth { get; set; } = -1;

        public Unit GetUnitGameObject()
        {
            GameObject go = Instantiate(_prefabUnit, Vector3.zero, Quaternion.identity);
            Unit unit = go.GetComponent<Unit>();
            unit.SetAttack(_attacks);
            return unit;
        }
        public void Init()
        {
            CurrentHealth = MaxHealth;
        }
        public void SetPrefab(GameObject prefab)
        {
            _prefabUnit = prefab;   
        }

    }
}
