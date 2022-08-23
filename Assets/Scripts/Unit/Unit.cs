
using System;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    GridPosition gridPosition;
    MoveAction moveAction;
    SpinAction spinAction;
    BaseAction[] baseActionArray;
    int actionPoint = ACTION_POINT_MAX;
    const int ACTION_POINT_MAX = 3;
    public static event Action OnAnyPointsChanged;
    [SerializeField] public bool isEnemy = false;
    void Awake()
    {
        moveAction = GetComponent<MoveAction>();
        spinAction = GetComponent<SpinAction>();
        baseActionArray = GetComponents<BaseAction>();
        // Debug.Log(this.name+" "+isEnemy);
    }
    void Start()
    {
        gridPosition = GridManager.Instance.GetGridPosition(transform.position);
        GridManager.Instance.AddUnitAtGridPosition(gridPosition, this);
        GridManager.Instance.SetGridText(gridPosition);
        TurnSystem.Instance.onTurnChanged += TurnSystem_OnTurnChanged;
    }
    void Update()
    {
        GridPosition newGridPosition = GridManager.Instance.GetGridPosition(transform.position);
        if (newGridPosition != gridPosition)
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
    public bool CanSpeedActionPoint(BaseAction baseAction)
    {
        if (actionPoint >= baseAction.GetActionPointCost())
        {
            return true;
        }
        return false;
    }
    void SpendActionPoint(int amount)
    {
        actionPoint -= amount;
        OnAnyPointsChanged?.Invoke();
    }
    public bool TrySpeedActionPoints(BaseAction baseAction)
    {
        if (CanSpeedActionPoint(baseAction))
        {
            SpendActionPoint(baseAction.GetActionPointCost());
            return true;
        }
        return false;
    }
    public int GetActionPoint()
    {
        return actionPoint;
    }
    void TurnSystem_OnTurnChanged()
    {
        if ((IsEnemy() && !TurnSystem.Instance.IsPlayerTurn()) || (!IsEnemy() && TurnSystem.Instance.IsPlayerTurn()))
        {
            actionPoint = ACTION_POINT_MAX;
            OnAnyPointsChanged?.Invoke();
            Debug.Log("Player Turn");
        }
    }
    public bool IsEnemy()
    {
        // Debug.Log(this.name+" "+isEnemy);
        return isEnemy;
    }


}
