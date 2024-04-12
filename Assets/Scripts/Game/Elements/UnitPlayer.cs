using System.Collections;
using System.Collections.Generic;
using Turnpturn.Interfaces.Game.IA;
using UnityEngine;

namespace Turnpturn.Game.Elements
{
    public class UnitPlayer : Unit
    {
       public void SetActionPicker(IChooseAction actionPicker)
        {
            _actionPicker = actionPicker;
        }
    }
}