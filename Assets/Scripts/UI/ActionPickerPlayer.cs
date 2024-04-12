using System.Collections;
using System.Collections.Generic;
using TMPro;
using Turnpturn.Game.Actions;
using Turnpturn.Game.Elements;
using Turnpturn.Interfaces.Game.IA;
using UnityEngine;
using UnityEngine.UI;

namespace Turnpturn.UI
{
    public class ActionPickerPlayer : MonoBehaviour, IChooseAction
    {
        [SerializeField]
        private FightersManager _fighterManager;
        [SerializeField]
        private GameObject _panel;
        [SerializeField]
        private GameObject _buttonPrefab;
        [Header("Unit Panel")]
        [SerializeField]
        private TMP_Text _unitNameText;
        [Header("ActionsPanel")]
        [SerializeField]
        private GameObject _actionsPanel;
        [Header("TargetPanel")]
        [SerializeField]
        private GameObject _targetPanel;

        private Unit _currentUnit;
        private Attack _selectedActions;
        private List<GameObject> _buttonList;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ChooseAction(Unit unit)
        {
            
            _panel.SetActive(true);
            _targetPanel.SetActive(false);
            _currentUnit = unit;
            _unitNameText.text = unit.UnitName;
            BuildButtonList(unit.Actions);
        }

        private void MakeAction(Unit target)
        {
            CleanButtonList();
            _currentUnit.Attack(_selectedActions, target);

            _currentUnit.Wait(0.25f, _currentUnit.EndTurn);
        }

        private void BuildButtonList(List<Attack> actions)
        {
            _selectedActions = null;
            _actionsPanel.SetActive(true);
            _buttonList = new List<GameObject>();
            for (int i = 0; i < actions.Count; i++)
            {

                GameObject newButton = Instantiate(_buttonPrefab);
                newButton.transform.SetParent(_actionsPanel.transform, false);

                //delegate test Test(1);
                //newButton.GetComponent<Button>().onClick.AddListener(delegate { Test(1); });
                Attack actionSel = actions[i];
                newButton.GetComponent<Button>().onClick.AddListener(() => _selectedActions = actionSel);
                newButton.GetComponent<Button>().onClick.AddListener(() => _actionsPanel.SetActive(false));
                newButton.GetComponent<Button>().onClick.AddListener(BuildTargetList);


                //delegate { SomeMethodName(SomeObject); })


                newButton.GetComponentInChildren<TMP_Text>().text = actions[i].AttackData.AttackName;
                _buttonList.Add(newButton);
            }
        }
        private void CleanButtonList()
        {
            for (int i=0; i < _buttonList.Count;i++)
            {
                Destroy(_buttonList[i]);
            }
            _buttonList.Clear();
        }
        private void BuildTargetList()
        {
            _targetPanel.SetActive(true);
            CleanButtonList();
            _buttonList = new List<GameObject>();
            List<Unit> opponentList = _fighterManager.GetOpponentList(_currentUnit);

            for (int i = 0; i < opponentList.Count; i++)
            {
                
                GameObject newButton = Instantiate(_buttonPrefab);
                newButton.transform.SetParent(_targetPanel.transform, false);

                newButton.GetComponent<Button>().onClick.AddListener(() => _targetPanel.SetActive(false));
                //to fixinf variable capturing in lambda expression
                Unit target = opponentList[i];
                newButton.GetComponent<Button>().onClick.AddListener(() => MakeAction(target));
                //newButton.GetComponent<Button>().onClick.AddListener(delegate { MakeAction(opponentList[i]); });

                //delegate { SomeMethodName(SomeObject); })


                newButton.GetComponentInChildren<TMP_Text>().text = opponentList[i].UnitName;
                _buttonList.Add(newButton);
            }
        }

        
    }
}