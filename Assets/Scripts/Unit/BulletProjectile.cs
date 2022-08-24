using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    Vector3 targetPosition;
    [SerializeField] TrailRenderer trailRenderer;
    [SerializeField] Transform bulletExplosionPrefab;
    public void Setup(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }
    void Update()
    {
        Vector3 moveDir = (targetPosition - transform.position).normalized;
        float distanceBeforeMoving = Vector3.Distance(transform.position, targetPosition);
        float moveSpeed = 200f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
        float distanceAfterMoving = Vector3.Distance(transform.position, targetPosition);
        if (distanceBeforeMoving < distanceAfterMoving)
        {
            transform.position = targetPosition;
            trailRenderer.transform.parent = null;
            Destroy(gameObject);
            Instantiate(bulletExplosionPrefab,targetPosition, Quaternion.identity);
        }
    }
}
