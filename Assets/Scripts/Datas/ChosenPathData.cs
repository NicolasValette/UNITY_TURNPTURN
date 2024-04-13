using System.Collections;
using System.Collections.Generic;
using Turnpturn.Datas.Game;
using Turnpturn.Game.Elements;
using UnityEngine;


namespace Turnpturn.Datas
{
    [CreateAssetMenu(menuName = "Data/New ChosenPath")]
    public class ChosenPathData : ScriptableObject
    {
        public UnitData ChosenUnit { get; private set; }
        public FightScenarioData ChosenScenario { get; private set; }
        public string PlayerName { get => ChosenUnit.UnitName; }
        private bool _isPathChosen;
        public bool IsPathChosen { get => _isPathChosen; }
        public bool IsPathComplete
        {
            get => ChosenScenario.IsScenarioComplete;
        }
        public void Init(UnitData chosenHero, FightScenarioData scenario)
        {
            _isPathChosen = true;
            ChosenUnit = chosenHero;
            ChosenScenario = scenario;
            
        }
       
    }
}