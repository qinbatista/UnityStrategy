using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAction : BaseAction
{
    float totalSpinAmount;
    Action onSpinComplete;
    void Update()
    {
        if (!isActive)
        {
            return;
        }
        float spinAddAmount = 360f * Time.deltaTime;
        transform.eulerAngles += new Vector3(0, spinAddAmount, 0);
        totalSpinAmount += spinAddAmount;
        if (totalSpinAmount >= 360f)
        {
            isActive = false;
            onActionComplete();
        }
    }
    // public void Spin(Action onActionComplete)
    // {
    //     this.onActionComplete = onActionComplete;
    //     totalSpinAmount = 0f;
    //     isActive = true;
    // }
    public void StopSpin()
    {
        isActive = false;
    }

    public override string GetActionName()
    {
        return "Spin";
    }
    public class BaseParameters
    {

    }
    public class SpinBaseParameters : BaseParameters
    {

    }
    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        this.onActionComplete = onActionComplete;
        totalSpinAmount = 0f;
        isActive = true;
    }
    public override List<GridPosition> GetValidActionGridPositionList()
    {
        GridPosition unitGridPosition = unit.GetGridPosition();
        return new List<GridPosition>
        {
            unitGridPosition
        };
    }
}
