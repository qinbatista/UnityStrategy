using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAction : MonoBehaviour
{
    public static event Action<BaseAction> OnAnyActionStarted;
    public static event Action<BaseAction> OnAnyActionComplete;
    protected Unit unit;
    protected bool isActive;
    protected Action onActionComplete;
    // Start is called before the first frame update
    protected virtual void Awake()
    {
        unit = GetComponent<Unit>();
    }
    public abstract string GetActionName();
    public abstract void TakeAction(GridPosition gridPosition, Action onActionComplete);
    public virtual bool IsValidActionGridPosition(GridPosition gridPosition)
    {
        List<GridPosition> validGridPositionList = GetValidActionGridPositionList();
        // Debug.Log("1:"+gridPosition);
        // Debug.Log("2:"+validGridPositionList);
        // Debug.Log("3:"+validGridPositionList.Contains(gridPosition));
        return validGridPositionList.Contains(gridPosition);
    }
    public abstract List<GridPosition> GetValidActionGridPositionList();
    public virtual int GetActionPointCost()
    {
        return 1;
    }
    protected void ActionStart(Action onActionComplete)
    {
        this.onActionComplete = onActionComplete;
        isActive = true;
        OnAnyActionStarted?.Invoke(this);
    }
    protected void ActionComplete()
    {
        isActive = false;
        onActionComplete();
        OnAnyActionComplete?.Invoke(this);
    }
    public Unit GetUnit()
    {
        return unit;
    }
    public EnemyAIAction GetBestEnemyAIAction()
    {
        List<EnemyAIAction> enemyAIActionsList = new List<EnemyAIAction>();
        List<GridPosition> ValidActionGridPositionList = GetValidActionGridPositionList();
        foreach (GridPosition gridPosition in ValidActionGridPositionList)
        {
            EnemyAIAction enemyAIAction = GetEnemyAIAction(gridPosition);
            enemyAIActionsList.Add(enemyAIAction);
        }
        if (enemyAIActionsList.Count > 0)
        {
            enemyAIActionsList.Sort((EnemyAIAction a, EnemyAIAction b) => b.actionValue - a.actionValue);
            return enemyAIActionsList[0];
        }
        else
        {
            //no possible IA
            return null;
        }
    }
    public abstract EnemyAIAction GetEnemyAIAction(GridPosition gridPosition);
}
