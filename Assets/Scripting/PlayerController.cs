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

    private CharController controller;
    private Weapon weapon;
    [SerializeField]
    private Transform cam;
    private Health health;
    private float attackSpeed = 2;

    private float attackTime = 0;

    private void Start()
    {
        controller = GetComponent<CharController>();
        weapon = GetComponent<Weapon>();
        health = GetComponent<Health>();

    }

    private void Update()
    {
        if (!health.IsDead)
        {
            GetInput();
            //rotx += Input.GetAxis("Mouse X") * rotSpeed * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, cam.eulerAngles.y, 0);
            controller.Movement(run, v);

            if (attackTime < attackSpeed)
                attackTime = attackTime + Time.deltaTime;
        }
    }

    void GetInput()
    {
        v = Input.GetAxis("Vertical");
        if (Input.GetMouseButtonDown(0))
        {
            if (attackTime >= attackSpeed)
            {
                weapon.Attack(); attackTime = 0;
            }
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
