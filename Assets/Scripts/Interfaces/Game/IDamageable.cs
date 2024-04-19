using System.Collections;
using System.Collections.Generic;
using Turnpturn.Datas;
using UnityEngine;

namespace Turnpturn.Interfaces.Game
{
    public interface IDamageable
    {
        void TakeDamage(int amount);
        ElementalTypeData UnitElement { get; }
    }
}