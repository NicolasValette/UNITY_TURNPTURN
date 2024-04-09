using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Turnpturn.Game.System
{
    public class GameOver : MonoBehaviour
    {
        [SerializeField]
        private GameObject _winGameObject;
        [SerializeField]
        private TMP_Text _wintext;

        private void OnEnable()
        {
            TurnManager.OnFightWin += DisplayWinText;
        }
        private void OnDisable()
        {
            TurnManager.OnFightWin -= DisplayWinText;
        }
        private void Start()
        {
            _winGameObject.SetActive(false);
        }
        // Update is called once per frame
        void Update()
        {

        }

        private void DisplayWinText()
        {
            _winGameObject.SetActive(true);
            _wintext.text = "You defeat Sephiroth !";
        }
    }
}