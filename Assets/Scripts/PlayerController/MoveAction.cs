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
    public void SetTarget(Vector3 targetPosition)//Move in lecture
    {
        // transform.forward = (targetPosition - transform.position).normalized;
        GridPosition gridPosition = GridManager.Instance.GetGridPosition(transform.position);
        // GridManager.Instance.SetGridText(gridPosition);
        this.targetPosition = targetPosition;
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
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;
                Debug.Log(testGridPosition);
                // if (gridPosition.GetDistance() <= MaxMoveDistance)
                // {
                //     validActionGridPositionList.Add(gridPosition);
                // }
            }
        }
        return validActionGridPositionList;
    }
}
