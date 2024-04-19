using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Turnpturn.Datas
{
    [CreateAssetMenu(menuName ="Data/New Element")]
    public class ElementalTypeData : ScriptableObject
    {
        [SerializeField]
        private string _name;
        [Tooltip("Element against this one make double damage")]
        [SerializeField]
        private List<ElementalTypeData> _strongerElementList;
        [Tooltip("Element against this one make half damage")]
        [SerializeField]
        private List<ElementalTypeData> _weaknessElementList;

        public string ElementName { get => _name; }
        public bool IsWeak(ElementalTypeData elementalType)
        {
            return _weaknessElementList.Contains(elementalType);
        }
        public bool IsStrong(ElementalTypeData elementalType)
        {
            return _strongerElementList.Contains(elementalType);
        }
    }
}