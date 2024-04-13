using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Turnpturn.Datas.Game
{

    [CreateAssetMenu(menuName = "Data/New PlayerData")]
    public class PlayerData : ScriptableObject
    {
        
        private ChosenPathData _chosenPathData;

        public UnitData ChosenUnitData { get; set; }
       
        public ChosenPathData ChosenPathData
        {
            get => _chosenPathData;
            set => _chosenPathData = value;
        }
        public string PlayerName
        {
            get => ChosenUnitData.UnitName;
            set => ChosenUnitData.UnitName = value;
        }

        public void WinFight()
        {
            _chosenPathData.ChosenScenario.WinFight();
        }
    }
}