using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    float v = 0f;
    bool run = false;

    CharController controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharController>();
    }

    // Update is called once per frame
    void Update()
    {
        controller.Movement(run, v);
    }
}
