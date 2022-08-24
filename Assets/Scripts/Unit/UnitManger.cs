using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManger : MonoBehaviour
{
    List<Unit> unitList;
    List<Unit> friendlyUnitList;
    List<Unit> enemyUnitList;
    public static UnitManger Instance { get; private set; }
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        unitList = new List<Unit>();
        friendlyUnitList = new List<Unit>();
        enemyUnitList = new List<Unit>();
    }
    void Start()
    {
        Unit.OnAnyUnitSpawned += Unit_OnAnyUnitSpawned;
        Unit.OnAnyUnitDead += Unit_OnAnyUnitDead;
    }
    void Unit_OnAnyUnitSpawned(Unit unit)
    {
        Debug.Log(unit+" spawned");
        unitList.Add(unit);
        if (unit.IsEnemy())
        {
            enemyUnitList.Add(unit);
        }
        else
        {
            friendlyUnitList.Add(unit);
        }
    }
    void Unit_OnAnyUnitDead(Unit unit)
    {
        Debug.Log(unit+" dead");
        unitList.Remove(unit);
        if (unit.IsEnemy())
        {
            enemyUnitList.Remove(unit);
        }
        else
        {
            friendlyUnitList.Remove(unit);
        }
    }
    public List<Unit> GetUnitList()
    {
        return unitList;
    }
    public List<Unit> GetFriendlyUnitList()
    {
        return friendlyUnitList;
    }
    public List<Unit> GetEnemyUnitList()
    {
        return enemyUnitList;
    }
}
