using System;
using System.Timers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{

    [SerializeField] private float maxHealth = 100;
    private float _health;
    // Start is called before the first frame update
    CharController controller;
    private bool _isDead = false;
    public Action<float> HealthChanged;
    private bool restartGame = false;
    public bool IsDead { get => _isDead; }
    public float CurrentHealth { get => _health; }
    private static Timer aTimer;
    private AudioSource[] audioSources;

    private void Awake()
    {
        audioSources = GetComponents<AudioSource>();
        controller = gameObject.GetComponent<CharController>();
        _health = maxHealth;
    }

    public void Injury(float amount)
    {
        if (!_isDead)
        {
            _health -= amount;
            if (CurrentHealth <= 0)
            {
                Death();
            }
            else
            {
                HealthChanged?.Invoke(_health);
                controller.Damage();
            }
            Debug.Log(CurrentHealth);
            audioSources[1].Play();
        }
    }

    public void Cure(float amount)
    {
        if (!_isDead)
        {
            
            if (CurrentHealth < 100)
            {
                if (amount < 100 - _health)
                {
                    _health += amount;
                }
                else
                {
                    _health = 100;
                }
            }
            HealthChanged?.Invoke(_health);
            Debug.Log(CurrentHealth);
        }
    }

    private void Death()
    {
        _health = 0;
        HealthChanged?.Invoke(_health);
        controller.Death();
        _isDead = true;
        if (gameObject.CompareTag("Player"))
        {
            Debug.Log("Start timer");
            aTimer = new Timer(3000);
            aTimer.Elapsed += LoadStartScene;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }
        
    }

    private void LoadStartScene(System.Object source, ElapsedEventArgs e)
    {
        Debug.Log("End timer");
        aTimer.Stop();
        aTimer.Dispose();
        Debug.Log("LoadScene");
        restartGame = true;
    }

    private void Update()
    {
        if (restartGame)
        {
            SceneManager.LoadScene(0);
        }
    }
}
