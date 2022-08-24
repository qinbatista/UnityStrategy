using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitRagdollSpawner : MonoBehaviour
{
    [SerializeField] Transform ragdollPrefab;
    [SerializeField] Transform originalPrefab;
    HealthSystem healthSystem;
    void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnDie += HealthSystem_OnDead;
    }
    void HealthSystem_OnDead()
    {
       Transform ragdollTransform =  Instantiate(ragdollPrefab, transform.position, transform.rotation);
       UnitRagdoll unitRagdoll = ragdollTransform.GetComponent<UnitRagdoll>();
       unitRagdoll.Setup(originalPrefab);
    }
}
