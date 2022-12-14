
using System;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    GridPosition gridPosition;

    BaseAction[] baseActionArray;
    int actionPoint = ACTION_POINT_MAX;
    const int ACTION_POINT_MAX = 3;
    public static event Action OnAnyPointsChanged;
    public static event Action<Unit> OnAnyUnitSpawned;
    public static event Action<Unit> OnAnyUnitDead;
    HealthSystem healthSystem;
    [SerializeField] public bool isEnemy = false;
    void Awake()
    {
        baseActionArray = GetComponents<BaseAction>();
        healthSystem = GetComponent<HealthSystem>();
        // Debug.Log(this.name+" "+isEnemy);
    }
    void Start()
    {
        gridPosition = GridManager.Instance.GetGridPosition(transform.position);
        GridManager.Instance.AddUnitAtGridPosition(gridPosition, this);
        // GridManager.Instance.SetGridText(gridPosition);
        TurnSystem.Instance.onTurnChanged += TurnSystem_OnTurnChanged;
        healthSystem.OnDie += HealthSystem_OnDie;
        OnAnyUnitSpawned?.Invoke(this);
    }
    void Update()
    {
        GridPosition newGridPosition = GridManager.Instance.GetGridPosition(transform.position);
        if (newGridPosition != gridPosition)
        {
            GridPosition oldGridPosition = gridPosition;
            // Debug.Log("not equal");
            gridPosition = newGridPosition;
            GridManager.Instance.UnitMoveGridPosition(this, oldGridPosition, newGridPosition);
        }
    }
    public T GetAction<T>() where T : BaseAction
    {
        foreach (BaseAction baseAction in baseActionArray)
        {
            if (baseAction is T)
            {
                return (T)baseAction;
            }
        }
        return null;
    }
    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }
    public Vector3 GetWorldPosition()
    {
        return transform.position;
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
            // Debug.Log("Player Turn");
        }
    }
    public bool IsEnemy()
    {
        // Debug.Log(this.name+" "+isEnemy);
        return isEnemy;
    }
    public void Damage(int damageAmount)
    {
        // Debug.Log(transform+"damaged!");
        // Destroy(gameObject);
        healthSystem.Damage(damageAmount);

    }
    void HealthSystem_OnDie()
    {
        GridManager.Instance.RemoveUnitAtGridPosition(gridPosition, this);
        // Debug.Log("a");
        Destroy(gameObject);
        OnAnyUnitDead?.Invoke(this);
    }
    public float GetHealthNormalized()
    {
        return healthSystem.GetHealthNormalized();
    }
}
