using System.Collections;
using System.Collections.Generic;
using TMPro;
using Turnpturn.Datas.Game;
using Turnpturn.Game.Elements;
using UnityEngine;

namespace Turnpturn.Game.System
{
    public class RoundOver : MonoBehaviour
    {
        [SerializeField]
        private PlayerData _playerData;
        [SerializeField]
        private GameObject _winGameObject;
        [SerializeField]
        private TMP_Text _wintext;
        
        [SerializeField]
        private LevelLoader _levelLoader;
        [SerializeField]
        private string _gameOverSceneName;
        [SerializeField]
        private string _campSceneName;
        [SerializeField]
        private string _ennemyName;

        private void OnEnable()
        {
            RoundManager.OnFightWin += WinFight;
            RoundManager.OnFightLoose += LooseFight;
            RoundManager.OnFightStart += RememberEnnemyName;
        }
        private void OnDisable()
        {
            RoundManager.OnFightWin -= WinFight;
            RoundManager.OnFightLoose -= LooseFight;
            RoundManager.OnFightStart -= RememberEnnemyName;
        }
        private void Start()
        {
            _winGameObject.SetActive(false);
        }
        // Update is called once per frame
        void Update()
        {

        }
        private void RememberEnnemyName (List<Unit> unitList)
        {
            _ennemyName = _playerData.ChosenPathData.ChosenScenario.CurrentEnnemy.UnitName;
        }
        private void LooseFight()
        {
            _winGameObject.SetActive(true);
            //string name = _playerData.ChosenPathData.ChosenScenario.GetSpecificEnnemy(_playerData.ChosenPathData.ChosenScenario.CurrentFight)?.UnitName;
            _wintext.text = $"{_ennemyName} defeat you !";
            _levelLoader.LoadSpecificLevel(_gameOverSceneName);
        }
        private void WinFight()
        {
            _winGameObject.SetActive(true);
            //string name = _playerData.ChosenPathData.ChosenScenario.GetSpecificEnnemy(_playerData.ChosenPathData.ChosenScenario.CurrentFight - 1)?.UnitName;
            _wintext.text = $"You defeat {_ennemyName} !";
            _levelLoader.LoadSpecificLevel(1);
        }
    }
}