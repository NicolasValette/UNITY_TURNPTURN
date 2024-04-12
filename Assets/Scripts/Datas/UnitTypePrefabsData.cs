using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Turnpturn.Datas
{
    [CreateAssetMenu(menuName ="Data/New Unit Type Prefabs")]
    public class UnitTypePrefabsData : ScriptableObject
    {
        public enum UnitType
        {
            Hero,
            Ennemy
        }
        [SerializeField]
        public GameObject _heroPrefab;
        [SerializeField]
        public GameObject _playableHeroPrefab;
        [SerializeField]
        private GameObject _ennemyPrefab;

        public GameObject HeroPrefab { get => _heroPrefab; }
        public GameObject PlayableHeroPrefab { get => _playableHeroPrefab; }
        public GameObject EnnemyPrefab { get => _ennemyPrefab; }

    }
}