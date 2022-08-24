using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class HealthSystem : MonoBehaviour
{
    [SerializeField]int health = 100;
    int healthMax;
    public event Action OnDie;
    public event Action Damaged;
    void Awake()
    {
        healthMax = health;
    }
    public void Damage(int damageAmount)
    {
        health -= damageAmount;
        if (health < 0)
        {
            health = 0;
        }
        Damaged?.Invoke();
        if (health == 0)
        {
            Die();
        }
        // Debug.Log(health);
    }

    void Die()
    {
        OnDie?.Invoke();
        Destroy(gameObject);
    }
    public float GetHealthNormalized()
    {
        return (float)health/healthMax;
    }
}
