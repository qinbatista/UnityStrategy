using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : MonoBehaviour
{
    [SerializeField] Animator animator;
    Unit unit;
    int runningAnimationID;
    Vector3 targetPosition;
    float moveSpeed = 4f;
    float rotateSpeed = 10f;
    [SerializeField] int MaxMoveDistance = 4;
    // Start is called before the first frame update
    void Awake()
    {
        unit = GetComponent<Unit>();
        runningAnimationID = Animator.StringToHash("running");
        targetPosition = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        if (transform.position != targetPosition)
        {
            animator.SetBool(runningAnimationID, true);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            transform.forward = Vector3.Lerp(transform.forward, (targetPosition - transform.position).normalized, rotateSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool(runningAnimationID, false);
        }
    }
    public void SetTarget(GridPosition targetPosition)//Move in lecture
    {
        this.targetPosition = GridManager.Instance.GetWorldPosition(targetPosition);
    }
    public bool IsValidActionGridPosition(GridPosition gridPosition)
    {
        List<GridPosition> validGridPositionList = GetValidActionGridPositionList();
        // Debug.Log("1:"+gridPosition);
        // Debug.Log("2:"+validGridPositionList.Contains(gridPosition));
        return validGridPositionList.Contains(gridPosition);
    }
    public List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validActionGridPositionList = new List<GridPosition>();
        GridPosition unitGridPosition = unit.GetGridPosition();
        for (int x = -MaxMoveDistance; x <= MaxMoveDistance; x++)
        {
            for (int z = -MaxMoveDistance; z <= MaxMoveDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition  + offsetGridPosition;
                if(!GridManager.Instance.IsValidGridPosition(testGridPosition)) continue;
                if(unitGridPosition==testGridPosition)  continue;
                if (GridManager.Instance.HasAnyUnitOnGridPosition(testGridPosition)) continue;
                // Debug.Log(testGridPosition);
                validActionGridPositionList.Add(testGridPosition);
            }
        }
        return validActionGridPositionList;
    }
}
