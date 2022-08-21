
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    GridPosition gridPosition;
    MoveAction moveAction;

    void Start()
    {
        gridPosition = GridManager.Instance.GetGridPosition(transform.position);
        GridManager.Instance.AddUnitAtGridPosition(gridPosition, this);
        GridManager.Instance.SetGridText(gridPosition);
        moveAction = GetComponent<MoveAction>();

    }
    void Update()
    {
        GridPosition newGridPosition = GridManager.Instance.GetGridPosition(transform.position);
        if(newGridPosition!=gridPosition)
        {
            Debug.Log("not equal");
            GridManager.Instance.UnitMoveGridPosition(this, gridPosition, newGridPosition);
            gridPosition = newGridPosition;
        }
    }
    public MoveAction GetMoveAction()
    {
        return moveAction;
    }
    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }

}
