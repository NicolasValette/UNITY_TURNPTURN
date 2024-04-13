using System.Collections;
using System.Collections.Generic;
using Turnpturn.Datas.Enums;
using UnityEngine;

namespace Turnpturn.Datas.Game
{
    [CreateAssetMenu (menuName = "Scenario/New Scenario")]
    public class FightScenarioData : ScriptableObject
    {
        [SerializeField]
        private string _scenarioName;
        [SerializeField]
        [TextArea(3,10)]
        private string _description;
        [SerializeField]
        [TextArea(3, 10)]
        private string _winText;
        [SerializeField]
        [TextArea(3, 10)]
        private string _defeatText;
        [SerializeField]
        private DifficultyEnum _difficulty;
        [SerializeField]
        private List<UnitData> _ennemyList;

        public string ScenarioName { get => _scenarioName; }
        public string WinText { get => _winText; }
        public string DefeatText { get => _defeatText; }
        public int CurrentFight { get; private set; } = 0;
        public bool IsScenarioComplete
        {
            get
            {
                return CurrentFight>= _ennemyList.Count;
            }
        }
        public DifficultyEnum Difficulty { get => _difficulty; }
        public UnitData CurrentEnnemy
        {
            get
            {
                return CurrentFight < _ennemyList.Count ? _ennemyList[CurrentFight] : null;
            }
        }
        
        
        public void Init()
        {
            CurrentFight = 0;
        }
        public void WinFight()
        {
            CurrentFight++;
        }
        public UnitData GetSpecificEnnemy(int ind)
        {
            return (ind >= 0 && ind < _ennemyList.Count) ? _ennemyList[ind] : null;
        }

    }
}