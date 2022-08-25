using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : BaseAction
{
    // [SerializeField] Animator animator;
    int runningAnimationID;
    List<Vector3> positionList;
    int currentPositionIndex;
    float moveSpeed = 4f;
    float rotateSpeed = 10f;
    [SerializeField] int MaxMoveDistance = 4;
    public event Action OnStartMoving;
    public event Action OnStopMoving;
    protected override void Awake()
    {
        base.Awake();
        // runningAnimationID = Animator.StringToHash("running");
        // targetPosition = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        if (!isActive) return;
        Vector3 targetPosition = positionList[currentPositionIndex];
        Vector3 moveDirection = (targetPosition - transform.position).normalized;
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, rotateSpeed * Time.deltaTime);
        float stoppingDistance = 0.1f;
        if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
        {
            // animator.SetBool(runningAnimationID, true);
            // transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            // transform.forward = Vector3.Lerp(transform.forward, moveDirection, rotateSpeed * Time.deltaTime);
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }
        else
        {
            currentPositionIndex++;
            if (currentPositionIndex >= positionList.Count)
            {
                OnStopMoving?.Invoke();
                // animator.SetBool(runningAnimationID, false);
                ActionComplete();
            }
        }
    }
    // public void SetTarget(GridPosition targetPosition, Action onActionComplete)//Move in lecture
    // {
    //     this.onActionComplete = onActionComplete;
    //     isActive = true;
    //     this.targetPosition = GridManager.Instance.GetWorldPosition(targetPosition);
    // }
    public override List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validActionGridPositionList = new List<GridPosition>();
        GridPosition unitGridPosition = unit.GetGridPosition();
        for (int x = -MaxMoveDistance; x <= MaxMoveDistance; x++)
        {
            for (int z = -MaxMoveDistance; z <= MaxMoveDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;
                if (!GridManager.Instance.IsValidGridPosition(testGridPosition)) continue;
                if (unitGridPosition == testGridPosition) continue;
                if (GridManager.Instance.HasAnyUnitOnGridPosition(testGridPosition)) continue;
                // Debug.Log(testGridPosition);
                validActionGridPositionList.Add(testGridPosition);
            }
        }
        return validActionGridPositionList;
    }

    public override string GetActionName()
    {
        return "Move";
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        List<GridPosition> pathGridPositionList = PathFinding.Instance.FindPath(unit.GetGridPosition(), gridPosition);
        currentPositionIndex = 0;
        positionList = new List<Vector3>();

        foreach (GridPosition pathGridPosition in pathGridPositionList)
        {
            positionList.Add(GridManager.Instance.GetWorldPosition(pathGridPosition));
        }
        OnStartMoving?.Invoke();
        ActionStart(onActionComplete);
    }

    public override EnemyAIAction GetEnemyAIAction(GridPosition gridPosition)
    {
        int targetCountAtGidPosition = unit.GetAction<ShootAction>().GetTargetCountAtPosition(gridPosition);
        return new EnemyAIAction
        {
            gridPosition = gridPosition,
            actionValue = targetCountAtGidPosition * 10,
        };
    }
}
