using System.Collections;
using System.Collections.Generic;
using Turnpturn.Datas;
using Turnpturn.Game.Elements;
using Turnpturn.Game.System;
using Turnpturn.Interfaces.Game;
using UnityEngine;

namespace Turnpturn.Game.Actions
{
    public class Attack : MonoBehaviour, IInflictDamage
    {
        [SerializeField]
        private AttackData _attackData;

        private int _numberOfRoundBeforeAvailable = 0;
        public bool IsAvailable
        {
            get
            {
                return (_numberOfRoundBeforeAvailable <= 0);
            }
        }
        
      
        private void OnDisable()
        {
            PlayerSorter.OnNewRound -= Cooldown;
        }

        public AttackData AttackData { get => _attackData; }

        public void Init()
        {
            _numberOfRoundBeforeAvailable = 0;
            PlayerSorter.OnNewRound -= Cooldown; //if we are already subscribe
            PlayerSorter.OnNewRound += Cooldown;
        }
        public int InflictDamge(IDamageable target, int damage)
        {
            int damageOutput = 0;
            if (_attackData.AttackElement.IsStrong(target.UnitElement))
            {
                Debug.Log("attaque efficace");
                damageOutput = damage * 2;
            }
            else if ((_attackData.AttackElement.IsWeak(target.UnitElement)))
            {
                Debug.Log("attaque peu efficace");
                damageOutput = Mathf.RoundToInt(damage / 2f);
            }
            else
            {
                Debug.Log("attaque neutre");
                damageOutput = damage;
            }
            target.TakeDamage(damageOutput);
            return damageOutput;
        }
        
        public ActionType PerformAction(Unit owner, Unit target)
        {
            int damageDealt = InflictDamge(target, _attackData.AttackDmg);
            ActionType act = new ActionType(ActionType.ActType.Attack, owner, target, damageDealt);
            _numberOfRoundBeforeAvailable = _attackData.AttackCDR;
            return act;
        }
        private void Cooldown(int roundNumber)
        {
            Debug.Log($"In round {roundNumber} Action({_attackData.AttackName}) reduce 1 cooldown");
            _numberOfRoundBeforeAvailable--;
        }

    }
}