using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject
{
    GridPosition gridPosition;
    GridSystem gridSystem;
    public GridObject(GridSystem gridSystem, GridPosition gridPosition)
    {
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
    }
    public override string ToString()
    {
        return gridPosition.ToString();
    }
}
