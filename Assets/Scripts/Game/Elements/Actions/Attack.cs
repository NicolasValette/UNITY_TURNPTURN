using System.Collections;
using System.Collections.Generic;
using Turnpturn.Datas;
using Turnpturn.Interfaces.Game;
using UnityEngine;

namespace Turnpturn.Game.Actions
{
    public class Attack : MonoBehaviour, IInflictDamage
    {
        [SerializeField]
        private AttackData _attackData;

        public AttackData AttackData { get => _attackData; }
        public void InflictDamge(IDamageable target, int damage)
        {
            target.TakeDamage(damage);
        }
        


    }
}