using System.Collections;
using System.Collections.Generic;
using Turnpturn.Game.Elements;
using UnityEngine;

namespace Turnpturn.Datas
{
    
    public class ActionType
    {
        public enum ActType
        {
            Idle,
            Attack
        }
        
        public ActType Type {get; private set;}
        public Unit Owner { get; private set; }
        public int Amount { get; private set;}
        public Unit Target { get; private set;}

        public ActionType(ActType type, Unit owner, Unit target = null, int amount = 0)
        {
            Owner = owner;
            Type = type;
            Amount = amount;
            Target = target;

        }
        public override string ToString()
        {
            return $"{Owner.UnitName} use {Type.ToString()} on {Target?.UnitName} for {Amount}";
        }
    }
}