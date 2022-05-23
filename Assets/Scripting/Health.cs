using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 100;
    // Start is called before the first frame update
    CharController controller;
    public bool isDead = false;
    private void Start()
    {
        controller = gameObject.GetComponent<CharController>();
    }

    public void Injury(int amount)
    {
        int newH = health - amount;
        if (newH < 0)
        {
            newH = 0;
            health = newH;
            controller.Death();
            isDead = true;
        }
        else
        {
            health = newH;
            controller.Damage();
        }
        Debug.Log(health);
    }
}
