
using UnityEngine;

public class Unit : MonoBehaviour
{
    Vector3 targetPosition;
    float moveSpeed = 4f;
    void Update()
    {
        // if(!Mathf.Approximately(Vector3.Distance(targetPosition,transform.position),0.01f))
        //     transform.position += (targetPosition - transform.position).normalized * moveSpeed * Time.deltaTime;
        if (transform.position != targetPosition)
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.T) || Input.GetMouseButtonDown(0))
        {
            SetTarget(Controller.Instance.GetWorldPosition());
        }
    }
    void SetTarget(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }
}
