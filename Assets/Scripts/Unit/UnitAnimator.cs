using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Transform bulletProjectilePrefab;
    [SerializeField] Transform bulletSpawnPoint;
    void Awake()
    {
        if (TryGetComponent<MoveAction>(out MoveAction moveAction))
        {
            moveAction.OnStartMoving += MoveAction_OnStartMoving;
            moveAction.OnStopMoving += MoveAction_OnStopMoving;
        }
        if (TryGetComponent<ShootAction>(out ShootAction shootAction))
        {
            shootAction.OnStartShooting += shootAction_OnStartShooting;
        }
    }

    // Update is called once per frame
    void MoveAction_OnStartMoving()
    {
        animator.SetBool("running", true);
    }
    void MoveAction_OnStopMoving()
    {
        animator.SetBool("running", false);
    }
    void shootAction_OnStartShooting(ShootAction.OnShootEventArgs args)
    {
        animator.SetTrigger("shooting");
        Transform bulletProjectile = Instantiate(bulletProjectilePrefab, bulletSpawnPoint.position, Quaternion.identity);
        Vector3 targetUnitShootAtPosition = args.targetUnit.GetWorldPosition();
        targetUnitShootAtPosition.y = bulletProjectile.position.y ;
        bulletProjectile.GetComponent<BulletProjectile>().Setup(targetUnitShootAtPosition);
    }
}
