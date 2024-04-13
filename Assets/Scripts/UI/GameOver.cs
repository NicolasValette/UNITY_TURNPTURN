using System.Collections;
using System.Collections.Generic;
using TMPro;
using Turnpturn.Datas.Game;
using UnityEngine;

namespace Turnpturn.UI
{
    public class GameOver : MonoBehaviour
    {
        [SerializeField]
        private PlayerData _playerData;
        [SerializeField]
        private TMP_Text _looseText;
        // Start is called before the first frame update
        void Start()
        {
            _looseText.text = _playerData.ChosenPathData.ChosenScenario.DefeatText;
        }
    }
}