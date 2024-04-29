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
    public GameObject SpawnEnnemy(UnitData unitData, int positionSlot = 0)
    {
        if (positionSlot < _ennemySpawnPosList.Count)
        {
            Quaternion lookRotation = Quaternion.LookRotation(_heroSpawnPosList[0].position - _ennemySpawnPosList[0].position);
            unitData.CurrentHealth = unitData.MaxHealth;
            GameObject go = Instantiate(unitData.PrefabUnit, _ennemySpawnPosList[positionSlot].position, lookRotation);
            go.name = unitData.UnitName;
            go.GetComponent<Unit>().InitUnit(unitData, unitData.CurrentHealth);
            return go;
        }
        else return null;
    }
    public GameObject SpawnHero()
    {
        return Instantiate(_unitPrefabs.HeroPrefab, _heroSpawnPosList[0].position, Quaternion.identity);
        
    }
    public GameObject SpawnUnit(UnitData unitData)
    {
        Quaternion lookRotation = Quaternion.LookRotation(_ennemySpawnPosList[0].position - _heroSpawnPosList[0].position);
        GameObject go = Instantiate(unitData.PrefabUnit, _heroSpawnPosList[0].position, lookRotation);
        go.name = unitData.UnitName;
        go.GetComponent<Unit>().InitUnit(unitData, unitData.CurrentHealth);
        UnitPlayer unitp = go.GetComponent<UnitPlayer>();
        if (unitp != null)
        {
            unitp.SetActionPicker(_actionPickerForPlayer);
        }
        return go;
    }
}
