using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAction : BaseAction
{
    enum State
    {
        Aiming,
        Shooting,
        CoolOff,
    }
    State state;
    float stateTimer;
    int maxShootDistance = 7;
    Unit targetUnit;
    bool canShootBullet = true;
    public event Action<OnShootEventArgs> OnStartShooting;
    public class OnShootEventArgs : EventArgs
    {
        public Unit targetUnit;
        public Unit shootingUnit;
    }
    // public event Action OnStopShooting;
    public override string GetActionName()
    {
        return "Shoot";
    }
    void Update()
    {
        if (!isActive)
        {
            return;
        }
        stateTimer -= Time.deltaTime;
        switch (state)
        {
            case State.Aiming:
                float rotateSpeed = 10f;
                Vector3 aimDir = (targetUnit.GetWorldPosition() - unit.GetWorldPosition()).normalized;
                transform.forward = Vector3.Lerp(transform.forward, aimDir, rotateSpeed * Time.deltaTime);
                break;
            case State.Shooting:
                if (canShootBullet)
                {
                    Shoot();
                    canShootBullet = false;
                }
                break;
            case State.CoolOff:
                break;
        }
        if (stateTimer <= 0f)
        {
            NextState();
        }
    }
    void NextState()
    {
        switch (state)
        {
            case State.Aiming:
                state = State.Shooting;
                float shottingStateTime = 0.1f;
                stateTimer = shottingStateTime;
                break;
            case State.Shooting:
                state = State.CoolOff;
                float coolOffStateTime = 0.5f;
                stateTimer = coolOffStateTime;
                break;
            case State.CoolOff:
                ActionComplete();
                break;
        }
        // Debug.Log(state);
    }
    public override List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validActionGridPositionList = new List<GridPosition>();
        GridPosition unitGridPosition = unit.GetGridPosition();
        for (int x = -maxShootDistance; x <= maxShootDistance; x++)
        {
            for (int z = -maxShootDistance; z <= maxShootDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;
                if (!GridManager.Instance.IsValidGridPosition(testGridPosition)) continue;
                int testDistance = Mathf.Abs(x) + Mathf.Abs(z);
                if (testDistance > maxShootDistance) continue;
                if (!GridManager.Instance.HasAnyUnitOnGridPosition(testGridPosition)) continue;
                // Debug.Log(testGridPosition);
                Unit targetUnit = GridManager.Instance.GetUnitGridPosition(testGridPosition);
                if (targetUnit.IsEnemy() == unit.IsEnemy()) continue;//same team
                validActionGridPositionList.Add(testGridPosition);
            }
        }
        return validActionGridPositionList;
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        ActionStart(onActionComplete);
        targetUnit = GridManager.Instance.GetUnitGridPosition(gridPosition);
        // Debug.Log("Aiming");
        state = State.Aiming;
        float aimingStateTime = 1f;
        stateTimer = aimingStateTime;
        canShootBullet = true;
    }
    void Shoot()
    {
        OnStartShooting?.Invoke(new OnShootEventArgs { targetUnit = targetUnit, shootingUnit = unit });
        targetUnit.Damage();
    }

}
