using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    //float rotSpeed = 1000;//80;
    //float rotx = 0f;
    float v = 0f;
    bool run = false;

    CharController controller;
    Weapon weapon;
    [SerializeField]
    Transform cam;
    Health health;

    private void Start()
    {
        controller = GetComponent<CharController>();
        weapon = GetComponent<Weapon>();
        health = GetComponent<Health>();

    }

    private void Update()
    {
        if (!health.isDead)
        {
            GetInput();
            //rotx += Input.GetAxis("Mouse X") * rotSpeed * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, cam.eulerAngles.y, 0);
            controller.Movement(run, v);
        }
    }

    void GetInput()
    {
        v = Input.GetAxis("Vertical");
        if (Input.GetMouseButtonDown(0))
        {
            weapon.Attack();
        }

        bool shift = Input.GetKeyDown(KeyCode.LeftShift);
        if (Input.GetKeyDown(KeyCode.LeftShift))
            run = true;
        else if(Input.GetKeyUp(KeyCode.LeftShift))
            run = false;
        if (Input.GetKeyDown(KeyCode.Space))
            controller.Jump(v);
    }
}
