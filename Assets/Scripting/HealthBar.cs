using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Health Health;

    [SerializeField] Text Text;

    // Start is called before the first frame update
    void Start()
    {
        Text.text = "♥" + Health.CurrentHealth.ToString();
        Health.HealthChanged += OnHealthChanged;
    }

    void OnHealthChanged(float health)
    {
        Text.text = "♥" + health.ToString();
    }
}
