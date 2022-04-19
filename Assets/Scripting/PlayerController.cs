using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float rotSpeed = 1000;//80;
    float rotx = 0f;
    float v = 0f;
    bool run = false;

    CharController controller;

    private void Start()
    {
        controller = GetComponent<CharController>();
    }

    private void Update()
    {
        GetInput();
        rotx += Input.GetAxis("Mouse X") * rotSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, rotx, 0);
        v = Input.GetAxis("Vertical");
        controller.Movement(run, v);

    }

    void GetInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            controller.Attack();
        }

        bool shift = Input.GetKeyDown(KeyCode.LeftShift);
        if (Input.GetKeyDown(KeyCode.LeftShift))
            run = true;
        else if(Input.GetKeyUp(KeyCode.LeftShift))
            run = false;
    }
}
