
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    GridPosition gridPosition;
    MoveAction moveAction;
    SpinAction spinAction;
    BaseAction[] baseActionArray;
    void Awake()
    {
        moveAction = GetComponent<MoveAction>();
        spinAction = GetComponent<SpinAction>();
        baseActionArray= GetComponents<BaseAction>();
    }
    void Start()
    {
        gridPosition = GridManager.Instance.GetGridPosition(transform.position);
        GridManager.Instance.AddUnitAtGridPosition(gridPosition, this);
        GridManager.Instance.SetGridText(gridPosition);
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
    public SpinAction GetSpinAction()
    {
        return spinAction;
    }
    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }
    public BaseAction[] GetBaseActionArray()
    {
        return baseActionArray;
    }

}
