using System.Collections;
using System.Collections.Generic;
using Turnpturn.Datas;
using Turnpturn.Game.Elements;
using Turnpturn.UI;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private UnitTypePrefabsData _unitPrefabs;
    [SerializeField]
    private List<Transform> _heroSpawnPosList;
    [SerializeField]
    private List<Transform> _ennemySpawnPosList;
    [SerializeField]
    private ActionPickerPlayer _actionPickerForPlayer;

    public GameObject SpawnPlayableHero()
    {
        GameObject hero = Instantiate(_unitPrefabs.PlayableHeroPrefab, _heroSpawnPosList[0].position, Quaternion.identity);
        hero.GetComponent<UnitPlayer>().SetActionPicker(_actionPickerForPlayer);
        
        return hero;
    }
    public GameObject SpawnEnnemy()
    {
        return Instantiate(_unitPrefabs.EnnemyPrefab, _ennemySpawnPosList[0].position, Quaternion.identity);
    }
    public GameObject SpawnHero()
    {
        return Instantiate(_unitPrefabs.HeroPrefab, _heroSpawnPosList[0].position, Quaternion.identity);
        
    }
}
