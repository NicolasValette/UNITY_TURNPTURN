using System.Collections;
using System.Collections.Generic;
using Turnpturn.Game.Elements;
using Turnpturn.Interfaces.Game.IA;
using UnityEngine;

namespace Turnpturn.Game.IA
{
    public class ActionPickerCPU : MonoBehaviour, IChooseAction
    {
        [SerializeField]
        private float _waitingTime = 2f;
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
            MakeAction(unit);
        }

        private void EndAction(Unit unit)
        {
            unit.Wait(_waitingTime, unit.EndTurn);
        }
        private void MakeAction(Unit unit)
        {
            unit.Attack(unit.Actions[0]);
           // EndAction(unit);
        }

    }
}