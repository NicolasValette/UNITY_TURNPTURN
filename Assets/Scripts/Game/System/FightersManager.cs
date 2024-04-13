using System.Collections;
using System.Collections.Generic;
using Turnpturn.Datas;
using Turnpturn.Datas.Game;
using Turnpturn.Game.Elements;
using Turnpturn.Game.System;
using UnityEngine;

public class FightersManager : MonoBehaviour
{
    [SerializeField]
    private SpawnManager _spawnManager;
    [SerializeField]
    private FightersData _fighters;
    [SerializeField]
    private PlayerData _playerData;

    private void OnEnable()
    {
        RoundManager.OnFightWin += WinFight;
    }
    private void OnDisable()
    {
        RoundManager.OnFightWin -= WinFight;
    }
    private void Awake()
    {
        _fighters.Init();
        InstantiateFighter(_playerData.ChosenPathData.ChosenScenario);
    }
    public FightersData InstantiateFighter(FightScenarioData scenario)
    {
        _fighters.Ennemies.Add(_spawnManager.SpawnEnnemy(scenario.CurrentEnnemy));
        _fighters.Heroes.Add(_spawnManager.SpawnUnit(_playerData.ChosenUnitData));
        return _fighters;
    }
    
    public List<Unit> GetUnitListFromFighter()
    {
        List<Unit> list = new List<Unit>();

        for (int i = 0; i <  _fighters.Heroes.Count || i < _fighters.Ennemies.Count; i++)
        {
            if (i < _fighters.Heroes.Count)
            {
                list.Add(_fighters.Heroes[i].GetComponent<Unit>());
            }

            if (i < _fighters.Ennemies.Count)
            {
                list.Add(_fighters.Ennemies[i].GetComponent<Unit>());
            }
        }

        return list;
    }
    public List<Unit> GetOpponentList(Unit fromUnit)
    {
        List<Unit> list = new List<Unit>();
        if (fromUnit.UnitType == UnitTypePrefabsData.UnitType.Hero)
        {
            for (int i=0; i<_fighters.Ennemies.Count;i++)
            {
                list.Add(_fighters.Ennemies[i].GetComponent<Unit>());
            }
        }
        else if (fromUnit.UnitType == UnitTypePrefabsData.UnitType.Ennemy)
        {
            for (int i = 0; i < _fighters.Heroes.Count; i++)
            {
                list.Add(_fighters.Heroes[i].GetComponent<Unit>());
            }
        }
        return list;
    }
    public Unit GetOpponent (Unit fromUnit)
    {
        if (fromUnit.UnitType == UnitTypePrefabsData.UnitType.Hero)
        {
            if (_fighters.Ennemies.Count > 0)
            {
                return _fighters.Ennemies[0].GetComponent<Unit>();
            }
        }
        else if (fromUnit.UnitType == UnitTypePrefabsData.UnitType.Ennemy)
        {
            if (_fighters.Heroes.Count > 0)
            {
                return _fighters.Heroes[0].GetComponent<Unit>();
            }
        }
        
        return null; 
    }

    public List<Unit> GetHeroUnitList()
    {
        List<Unit> list = new List<Unit>();
        for (int i = 0; i < _fighters.Heroes.Count; i++)
        {
            list.Add(_fighters.Heroes[i].GetComponent<Unit>());
        }
        return list;
    }
    public List<Unit> GetEnnemyUnitList()
    {
        List<Unit> list = new List<Unit>();
        for (int i = 0; i < _fighters.Ennemies.Count; i++)
        {
            list.Add(_fighters.Ennemies[i].GetComponent<Unit>());
        }
        return list;
    }
    private void WinFight()
    {
        _playerData.WinFight();
    }
}
