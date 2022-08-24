using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    enum State
    {
        WaitingForEnemyTurn,
        TakingTurn,
        Busy,
    }
    State state;
    float timer;
    public static EnemyAI Instance { get; private set; }
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        state = State.WaitingForEnemyTurn;
    }
    void Start()
    {
        TurnSystem.Instance.onTurnChanged += TurnSystem_OnTurnChanged;
    }
    void Update()
    {
        if (TurnSystem.Instance.IsPlayerTurn())
        {
            return;
        }
        switch (state)
        {
            case State.WaitingForEnemyTurn:
                break;
            case State.TakingTurn:
                timer -= Time.deltaTime;
                if (timer <= 0f)
                {
                    if (TryTakeEnemyAIAction(SetStateTakingTurn))
                    {
                        state = State.Busy;
                    }
                    else
                    {
                        TurnSystem.Instance.NextTurn();
                    }
                }
                break;
            case State.Busy:
                break;
            default:
                break;
        }
        // timer -= Time.deltaTime;
        // if (timer <= 0f)
        // {
        //     TurnSystem.Instance.NextTurn();
        // }
    }
    void TurnSystem_OnTurnChanged()
    {
        if (!TurnSystem.Instance.IsPlayerTurn())
        {
            state = State.TakingTurn;
            timer = 1f;
        }
    }
    void SetStateTakingTurn()
    {
        timer = 0.5f;
        state = State.TakingTurn;
    }
    bool TryTakeEnemyAIAction(Action onEnemyAIActionComplete)
    {
        foreach (Unit enemyUnit in UnitManger.Instance.GetEnemyUnitList())
        {
            if (TryTakeEnemyAIAction(enemyUnit, onEnemyAIActionComplete))
            {
                return true;
            }
        }
        return false;
    }
    bool TryTakeEnemyAIAction(Unit enemyUnit, Action onEnemyAIActionComplete)
    {
        EnemyAIAction bestEnemyAIAction = null;
        BaseAction bestBaseAction = null;
        foreach (BaseAction baseAction in enemyUnit.GetBaseActionArray())
        {
            if (!enemyUnit.CanSpeedActionPoint(baseAction))
            {
                continue;
            }
            if(bestEnemyAIAction==null)
            {
                bestEnemyAIAction = baseAction.GetBestEnemyAIAction();
                bestBaseAction = baseAction;
            }
            else
            {
                EnemyAIAction BestEnemyAIAction = baseAction.GetBestEnemyAIAction();
                if(BestEnemyAIAction!=null)
                {
                    bestEnemyAIAction = baseAction.GetBestEnemyAIAction();
                    bestBaseAction = baseAction;
                }
                else
                {
                    EnemyAIAction testEnemyAIAction = baseAction.GetBestEnemyAIAction();
                    if(testEnemyAIAction!=null && testEnemyAIAction.actionValue>bestEnemyAIAction.actionValue)
                    {
                        bestEnemyAIAction = testEnemyAIAction;
                        bestBaseAction = baseAction;
                    }
                }
            }
        }
        if(bestEnemyAIAction!=null && enemyUnit.TrySpeedActionPoints(bestBaseAction))
        {
            bestBaseAction.TakeAction(bestEnemyAIAction.gridPosition, onEnemyAIActionComplete);
            return true;
        }
        else
        {
                return false;
        }
        // SpinAction spinAction = enemyUnit.GetSpinAction();
        // GridPosition actionGridPosition = enemyUnit.GetGridPosition();
        // if (!spinAction.IsValidActionGridPosition(actionGridPosition))
        // {
        //     // Debug.Log("CanSpeedActionPoint return");
        //     return false;
        // }
        // if (!enemyUnit.TrySpeedActionPoints(spinAction))
        // {
        //     // Debug.Log("TrySpeedActionPoints return");
        //     return false;
        // }
        // // SetBusy();
        // spinAction.TakeAction(actionGridPosition, onEnemyAIActionComplete);
        // return true;
    }
}
