using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Turnpturn.Datas
{
    [CreateAssetMenu (menuName = "Data/Attack/New Attack")]
    public class AttackData : ScriptableObject
    {
        [SerializeField]
        private string _attackName;
        [SerializeField]
        private int _attackDmg;
        [SerializeField]
        private int _attackPrice;
        [SerializeField]
        private int _attackCDR;

        public string AttackName { get => _attackName; }
        public int AttackDmg { get => _attackDmg; }
        public int AttackPrice { get => _attackPrice; }
        public int AttackCDR { get => _attackCDR; }

    }
}