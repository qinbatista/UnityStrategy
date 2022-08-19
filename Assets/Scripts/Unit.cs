
using UnityEngine;

public class Unit : MonoBehaviour
{
    Vector3 targetPosition;
    float moveSpeed = 4f;
    [SerializeField] Animator animator;
    int runningAnimationID;
    float rotateSpeed = 10f;
    void Awake()
    {
        runningAnimationID = Animator.StringToHash("running");
        targetPosition = transform.position;
    }
    void Update()
    {
        // if(!Mathf.Approximately(Vector3.Distance(targetPosition,transform.position),0.01f))
        //     transform.position += (targetPosition - transform.position).normalized * moveSpeed * Time.deltaTime;
        if (transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            transform.forward = Vector3.Lerp(transform.forward, (targetPosition - transform.position).normalized, rotateSpeed*Time.deltaTime);
        }
        else
        {
            animator.SetBool(runningAnimationID, false);
        }

    }
    public void SetTarget(Vector3 targetPosition)
    {
        // transform.forward = (targetPosition - transform.position).normalized;
        animator.SetBool(runningAnimationID, true);
        this.targetPosition = targetPosition;
    }
}
