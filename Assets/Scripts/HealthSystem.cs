using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth = 100;

    public event EventHandler OnHealthChange;
    public event EventHandler OnDead;
    private void Awake()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, currentHealth);
        OnHealthChange(this, EventArgs.Empty);
        Debug.Log(currentHealth);
        if(currentHealth == 0)
        {
            Die();
           
        }
    }

    private void Die()
    {
        OnDead?.Invoke(this, EventArgs.Empty);   
    }
    public float GetNormalisedHealth()
    {
        return (float)currentHealth / maxHealth;
    }
}
