using System.Collections;
using System.Collections.Generic;
using TMPro;
using Turnpturn.Game.Elements;
using Turnpturn.Game.System;
using UnityEngine;
using UnityEngine.UI;

namespace Turnpturn.UI
{
    /// <summary>
    /// Script to manage the information of fighters (hp)
    /// 
    /// </summary>
    public class FightersDisplay : MonoBehaviour
    {
        [SerializeField]
        private FightersManager _fightersManager;
        [SerializeField]
        private GameObject _healthBarePrefabGameObject;

        [SerializeField]
        private GameObject _heroHealthPanel;
 
        [SerializeField]
        private GameObject _ennemyHealthPanel;

        

        private void OnEnable()
        {
            RoundManager.OnFightStart += SetupHealthBar;
                
        }
        private void OnDisable()
        {
            RoundManager.OnFightStart -= SetupHealthBar;
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        
 
        private void InstantianteBar (Unit unit, GameObject parentPannel)
        {
            GameObject go = Instantiate(_healthBarePrefabGameObject);
            go.transform.SetParent(parentPannel.transform, false);
            go.GetComponent<HealthBar>().SetupHealthBar(unit);
           
        }
        private void SetupHealthBar(List<Unit> listUnit)
        {
            for (int i=0; i<listUnit.Count; i++)
            {
                if (listUnit[i].UnitType == Datas.UnitTypePrefabsData.UnitType.Ennemy)
                {
                    InstantianteBar(listUnit[i], _ennemyHealthPanel);
                }
                else if (listUnit[i].UnitType == Datas.UnitTypePrefabsData.UnitType.Hero)
                {
                    InstantianteBar(listUnit[i], _heroHealthPanel);
                }
            }
        }
    }
}