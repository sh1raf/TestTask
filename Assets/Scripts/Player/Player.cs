using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private int _maxHealths;

    private int _currentHealth;

    private Transform _currentPosition;  

    public event UnityAction TakeDamage;

    private void Start()
    {
        _currentHealth = _maxHealths;

    }
    public void TakingDamage(int damage)
    {
        Debug.Log("Player Take Damage");

        _currentHealth -= damage;
        TakeDamage?.Invoke();

        if (_currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
