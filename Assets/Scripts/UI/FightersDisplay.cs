using System.Collections;
using System.Collections.Generic;
using TMPro;
using Turnpturn.Game.Elements;
using Turnpturn.Game.System;
using UnityEngine;

namespace Turnpturn.UI
{
    public class FightersDisplay : MonoBehaviour
    {
        [SerializeField]
        private FightersManager _fightersManager;
        [SerializeField]
        private List<TMP_Text> _heroTextList;
        [SerializeField]
        private List<TMP_Text> _ennemyTextList;


        private void OnEnable()
        {
            RoundManager.OnNewTurn += UpdateDisplay;
                
        }
        private void OnDisable()
        {
            RoundManager.OnNewTurn -= UpdateDisplay;
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        
        private void UpdateDisplay()
        {
            List<Unit> listH = _fightersManager.GetHeroUnitList();
            for (int i = 0; i < listH.Count; i++)
            {
                _heroTextList[i].text = $"{listH[i].UnitName} - {listH[i].CurrentHP} / {listH[i].UnitCarac.MaxHealth}";
            }
            List<Unit> listE = _fightersManager.GetEnnemyUnitList();
            for (int i = 0; i < listE.Count; i++)
            {
                _ennemyTextList[i].text = $"{listE[i].UnitName} - {listE[i].CurrentHP} / {listE[i].UnitCarac.MaxHealth}";
            }
        }
    }
}