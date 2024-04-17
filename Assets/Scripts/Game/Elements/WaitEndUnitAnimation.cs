using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Turnpturn.Game.Elements
{
    public class WaitEndUnitAnimation : MonoBehaviour
    {
      
        public void EndAttackEvent()
        {
            gameObject.GetComponentInParent<Unit>().AttackAfterAnimation();
        }
        public void EndDamageTakenEvent()
        {
            gameObject.GetComponentInParent<Unit>().DamageTaken();
        }
    }
}