using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject
{
    GridPosition gridPosition;
    GridSystem<GridObject> gridSystem;
    GridPositionText gridDebugObj;
    List<Unit> unitList;
    public GridObject(GridSystem<GridObject> gridSystem, GridPosition gridPosition)
    {
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
        unitList = new List<Unit>();
    }
    public void SetGridPositionText(GridPositionText gridDebugObj)
    {
        this.gridDebugObj = gridDebugObj;
    }
    public void SetText()
    {
        string unitString = "";
        foreach (var unit in unitList)
        {
            unitString += unit.name + "\n";
        }
        Debug.Log("gridPosition="+gridPosition);
        Debug.Log("unitString="+unitString);
        this.gridDebugObj.SetText(gridPosition.ToString() + "\n" + unitString);
    }
    public override string ToString()
    {
        string unitString = "";
        foreach (var unit in unitList)
        {
            unitString += unit.name + "\n";
        }
        return gridPosition.ToString() + "\n" + unitString;
    }
    public bool HasAnyUnit()
    {
        return unitList.Count > 0;
    }
    public void AddUnit(Unit unit) => unitList.Add(unit);
    public void RemoveUnit(Unit unit) => unitList.Remove(unit);
    public List<Unit> GetUnitList() => unitList;
    public Unit GetUnit()
    {
        if(HasAnyUnit())
        {
            return unitList[0];
        }
        else
        {
            return null;
        }
    }

}
