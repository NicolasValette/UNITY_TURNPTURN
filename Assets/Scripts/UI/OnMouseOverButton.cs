using System.Collections;
using System.Collections.Generic;
using Turnpturn.Game.Elements;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Turnpturn.UI
{
    public class OnMouseOverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public bool IsMouseOverOn { get; set; } = false;

        public Unit Target { get; set; } = null;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void SetOverTarget(Unit target)
        {
            IsMouseOverOn = true;
            Target = target;
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("over");
            if (IsMouseOverOn)
            {
                Target.ShowSelector();
            }
            
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (IsMouseOverOn)
            {
               // Target.HideSelector();
            }
        }
    }
}