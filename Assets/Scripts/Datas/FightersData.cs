using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Turnpturn.Datas
{
    [CreateAssetMenu(menuName = "Data/FightersData")]
    public class FightersData : ScriptableObject
    {
        
        public List<GameObject> Heroes = new List<GameObject>();
        public List<GameObject> Ennemies = new List<GameObject>();

        public void Init()
        {
            Heroes = new List<GameObject> ();
            Ennemies = new List<GameObject>();
        }
    }
}