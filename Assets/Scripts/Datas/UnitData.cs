using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Turnpturn.Datas
{

    [CreateAssetMenu (menuName = "Data/New Unit")]
    public class UnitData : ScriptableObject
    {
        [SerializeField]
        private int _maxHealth;


        public int MaxHealth {get => _maxHealth;
        }
    }
}