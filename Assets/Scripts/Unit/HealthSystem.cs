using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class HealthSystem : MonoBehaviour
{
    int health = 100;
    public event Action OnDie;
    public void Damage(int damageAmount)
    {
        health -= damageAmount;
        if (health < 0)
        {
            health = 0;
        }
        if (health == 0)
        {
            Die();
        }
        Debug.Log(health);
    }

    void Die()
    {
        Destroy(gameObject);
        OnDie?.Invoke();
    }
}
