using System.Collections;
using System.Collections.Generic;
using TMPro;
using Turnpturn.Datas;
using Turnpturn.Game.Elements;
using Turnpturn.Game.System;
using UnityEngine;

namespace Turnpturn.UI
{
    public class DamageFloatingTextSpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject _floatingTextCanvasPrefab;
        [SerializeField]
        private Camera _camera;


        private List<Unit> _unitList;

        private void OnEnable()
        {
            RoundManager.OnFightStart += SubscribeOnUnits;
        }
        private void OnDisable()
        {
            RoundManager.OnFightStart -= SubscribeOnUnits;
            UnSubscribeOnUnits();
        }
        // Start is called before the first frame update
        void Start()
        {
            
            
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void SpawnText(ActionType action)
        {
            if (action.Type == ActionType.ActType.Attack)
            {
                GameObject go = Instantiate(_floatingTextCanvasPrefab, action.Target.DamageTestSpawnPos);
                go.GetComponent<Canvas>().worldCamera = _camera;
                go.GetComponentInChildren<TMP_Text>().text = action.Amount.ToString();
            }
        }
        private void SubscribeOnUnits(List<Unit> unitList)
        {
            _unitList = new List<Unit>();
            for (int i=0; unitList.Count > i; i++)
            {
                _unitList.Add(unitList[i]);
                unitList[i].OnAction += SpawnText;
            }
        }
        private void UnSubscribeOnUnits()
        {
            for (int i=0; _unitList.Count > i; i++)
            {
                _unitList[i].OnAction -= SpawnText;
            }
        }
    }
}