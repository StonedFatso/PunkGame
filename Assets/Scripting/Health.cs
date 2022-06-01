using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField] private float maxHealth = 100;
    private float _health;
    // Start is called before the first frame update
    CharController controller;
    private bool _isDead = false;
    public Action<float> HealthChanged;

    public bool IsDead { get => _isDead; }
    public float CurrentHealth { get => _health; }
    private void Awake()
    {
        controller = gameObject.GetComponent<CharController>();
        _health = maxHealth;
    }

    public void Injury(int amount)
    {
        _health -= amount;
        if (CurrentHealth <= 0)
        {
            _health = 0;
            HealthChanged?.Invoke(_health);
            controller.Death();
            _isDead = true;
        }
        else
        {
            HealthChanged?.Invoke(_health);
            controller.Damage();
        }
        Debug.Log(CurrentHealth);
    }
}
