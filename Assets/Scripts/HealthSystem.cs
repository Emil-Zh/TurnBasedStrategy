using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int health = 100;

    public event EventHandler OnDead;

    public void TakeDamage(int damage)
    {
       health = Mathf.Clamp(health - damage, 0, health);
        Debug.Log(health);
        if(health == 0)
        {
            Die();
           
        }
    }

    private void Die()
    {
        OnDead?.Invoke(this, EventArgs.Empty);   
    }
}
