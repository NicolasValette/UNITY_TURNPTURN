using System.Collections;
using System.Collections.Generic;
using TMPro;
using Turnpturn.Game.Actions;
using Turnpturn.Game.Elements;
using Turnpturn.Interfaces.Game.IA;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

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
     
        [Header("ActionsPanel")]
        [SerializeField]
        private GameObject _actionsPanel;
        [Header("TargetPanel")]
        [SerializeField]
        private GameObject _targetPanel;
        [SerializeField]
        private UIClickButtonPlayer _buttonClickPlayer;
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
            BuildButtonList(unit.Actions);
        }

        private void MakeAction(Unit target)
        {
            CleanButtonList();
            _currentUnit.Attack(_selectedActions, target);

            // _currentUnit.Wait(0.25f, _currentUnit.EndTurn);
            _selectedActions = null;
        }

        private void BuildButtonList(List<Attack> actions)
        {
            _selectedActions = null;
            _actionsPanel.SetActive(true);
            _buttonList = new List<GameObject>();
            for (int i = 0; i < actions.Count; i++)
            {
                Attack actionSel = actions[i];
                if (actionSel.IsAvailable)
                {
                    GameObject newButton = Instantiate(_buttonPrefab);
                    newButton.transform.SetParent(_actionsPanel.transform, false);

                    //delegate test Test(1);
                    //newButton.GetComponent<Button>().onClick.AddListener(delegate { Test(1); });

                    newButton.GetComponent<Button>().onClick.AddListener(() => _selectedActions = actionSel);
                    newButton.GetComponent<Button>().onClick.AddListener(() => _actionsPanel.SetActive(false));
                    newButton.GetComponent<Button>().onClick.AddListener(() => _buttonClickPlayer.PlayClick());
                    newButton.GetComponent<Button>().onClick.AddListener(BuildTargetList);


                    //delegate { SomeMethodName(SomeObject); })


                    newButton.GetComponentInChildren<TMP_Text>().text = $"{actions[i].AttackData.AttackName}({actions[i].AttackData.AttackElement.ElementName})";
                    _buttonList.Add(newButton);
                }
            }
        }
        private void CleanButtonList()
        {
            for (int i = 0; i < _buttonList.Count; i++)
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
                newButton.GetComponent<Button>().onClick.AddListener(() => _buttonClickPlayer.PlayClick());
                newButton.GetComponent<Button>().onClick.AddListener(() => target.HideSelector());
                newButton.GetComponent<OnMouseOverButton>().SetOverTarget(target);
                //delegate { SomeMethodName(SomeObject); })
                
                newButton.GetComponentInChildren<TMP_Text>().text = opponentList[i].UnitName;
                _buttonList.Add(newButton);
                if (i == 0)
                {
                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(newButton);
                    target.ShowSelector();
                }
            }
        }
       public void OnSelect(InputValue inputValue)
       {
            if (_selectedActions != null)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

                if (Physics.Raycast(ray, out hit))
                {
                    Unit selectuedUnit = hit.transform.gameObject.GetComponent<Unit>();
                    if (selectuedUnit != null)
                    {
                        _targetPanel.SetActive(false);
                        _buttonClickPlayer.PlayClick();
                        selectuedUnit.HideSelector();
                        MakeAction(selectuedUnit);
                    }
                    

                }
            }
        }
    }



}