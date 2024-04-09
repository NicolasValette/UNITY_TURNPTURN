using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Turnpturn.Interfaces.Game
{
    public interface IInflictDamage
    {
        void InflictDamge (IDamageable target, int damage);
    }
}