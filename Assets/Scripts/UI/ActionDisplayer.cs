using System.Collections;
using System.Collections.Generic;
using TMPro;
using Turnpturn.Datas;
using Turnpturn.Game.Elements;
using Turnpturn.Game.System;
using UnityEngine;

namespace Turnpturn.UI
{
    public class ActionDisplayer : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _text;
        [SerializeField]
        private TurnManager _turnManager;

        private void OnEnable()
        {
            TurnManager.OnFightStart += InitUnitList;
        }
        private void OnDisable()
        {
            for (int i = 0; i < _turnManager.UnitList.Count; i++)
            {
                _turnManager.UnitList[i].OnAction -= DisplayAction;
            }
            TurnManager.OnFightStart -= InitUnitList;
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        private void InitUnitList(List<Unit> _unitList)
        {
            for (int i=0; i<_unitList.Count; i++)
            {
                _unitList[i].OnAction += DisplayAction;
            }
        }
        private void DisplayAction(ActionType act)
        {
            _text.text = act.ToString();
        }

    }
}