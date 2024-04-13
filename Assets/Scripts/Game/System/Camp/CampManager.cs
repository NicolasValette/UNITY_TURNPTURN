using System.Collections;
using System.Collections.Generic;
using TMPro;
using Turnpturn.Audio;
using Turnpturn.Datas;
using Turnpturn.Datas.Enums;
using Turnpturn.Datas.Game;
using UnityEngine;

namespace Turnpturn.Game.System.Camp
{
    public class CampManager : MonoBehaviour
    {
       
        [SerializeField]
        private PlayerData _playerData;
        [SerializeField]
        private GameObject _pathPanel;
        [SerializeField]
        private TMP_Text _nameText;
        [SerializeField]
        private TMP_Text _placeholderNameText;
        [SerializeField]
        private FightScenarioData _scenario;
        [SerializeField]
        private AudioMusicPlayer _audioPlayer;
        [Space(1)]
        [Header("BetweenPanel Informations")]
        [SerializeField]
        private GameObject _betweenPanel;
        [Space(1)]
        [Header("WinPanel Informations")]
        [SerializeField]
        private GameObject _winPanel;
        [SerializeField]
        private TMP_Text _winText;
        [SerializeField]
        private AudioClip _victoryFanfare;
        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("startcamp");
            if (_playerData.ChosenPathData == null)
            {
                Debug.Log("choisir path1");
                _pathPanel.SetActive(true);
                _betweenPanel.SetActive(false);
                _winPanel.SetActive(false);
            }
            else
            {
                Debug.Log("path choisit 2");
                if (!_playerData.ChosenPathData.IsPathComplete)
                {
                    _pathPanel.SetActive(false);
                    _winPanel.SetActive(false);
                    _betweenPanel.SetActive(true);
                }
                else
                {
                    /*WIN*/
                    _pathPanel.SetActive(false);
                    _betweenPanel.SetActive(false);
                    _winPanel.SetActive(true);
                    _audioPlayer.Play(_victoryFanfare);
                    _winText.text = _playerData.ChosenPathData.ChosenScenario.WinText;
                }
            }
            
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ChoosePath(UnitData chosenUnit)
        {
            Debug.Log("InstHero");
            _scenario.Init();
            _playerData.ChosenPathData = ScriptableObject.CreateInstance<ChosenPathData>();
            _playerData.ChosenPathData.Init(chosenUnit, _scenario);
            //_playerData.ChosenPathData.ChosenScenario = _scenario;
            //_playerData.ChosenPathData.IsPathChosen = true;
            _playerData.ChosenUnitData = chosenUnit;
            _playerData.ChosenUnitData.Init();


            _playerData.PlayerName = _placeholderNameText.enabled?_placeholderNameText.text:_nameText.text;
            
        }

        public void NextFight()
        {
            Debug.Log("Next Fight");
        }
    }
}