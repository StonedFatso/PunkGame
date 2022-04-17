using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float speed = 4;
    float rotSpeed = 1000;//80;
    float rotx = 0f;
    float roty = 0f;
    float gravity = 8;
    float v = 0f;
    bool run = false;

    //private Vector3 direction;

    Vector3 moveDir = Vector3.zero;

    CharacterController controller;
    Animator anim;
    Transform m_Cam;
    Transform m_Pl;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        m_Cam = GameObject.FindGameObjectWithTag("PlayerCamera").transform;
        //m_Pl = GameObject.transform;
    }

    private void Update()
    {
        GetInput();
        rotx += /*Input.GetAxis("Horizontal")*/Input.GetAxis("Mouse X") * rotSpeed * Time.deltaTime;
        //roty += /*Input.GetAxis("Horizontal")*/Input.GetAxis("Mouse Y") * rotSpeed * Time.deltaTime;
        if (anim.GetBool("attacking") && CheckAttack())
        {
            anim.SetBool("attacking", false);
        }
        transform.eulerAngles = new Vector3(0, rotx, 0);
        Movement();

    }

    private void FixedUpdate()
    {
        //Movement();
    }

        void Movement()
    {
        if (controller.isGrounded) //&& !anim.GetBool("attacking"))// && !attack)
        {
            v = Input.GetAxis("Vertical");
            //rot += /*Input.GetAxis("Horizontal")*/Input.GetAxis("Mouse X") * rotSpeed * Time.deltaTime;
            moveDir = new Vector3(0, 0, v);
            //moveDir = Vector3.Scale(m_Cam.forward, new Vector3(0, 0, v)).normalized;
            if (run)
            {
                moveDir *= speed*2;
            }
            else
            {
                moveDir *= speed;
            }
            moveDir = transform.TransformDirection(moveDir);
            
            /*direction = m_Cam.forward;
            direction.x = direction.x - 7.87f;
            transform.forward = direction;*/
            if (!moveDir.Equals(new Vector3(0, 0, 0)))
            {
                moveDir.y -= gravity * Time.deltaTime;
                if (run)
                {
                    anim.SetFloat("Forward", 2);
                }
                else
                {
                    anim.SetFloat("Forward", v);
                }
                controller.Move(moveDir * Time.deltaTime);
            }
        }
    }

    void GetInput()
    {
        /*if (Input.GetAxis("Mouse X") != 0)
        {
            m_Cam.RotateAround(transform.position, new Vector3(0.0f, 1.0f, 0.0f), 5 * Input.GetAxis("Mouse X"));
        }*/
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }

        bool shift = Input.GetKeyDown(KeyCode.LeftShift);
        if (Input.GetKeyDown(KeyCode.LeftShift))
            run = true;
        else if(Input.GetKeyUp(KeyCode.LeftShift))
            run = false;
    }

    void Attack()
    {
        anim.SetBool("attacking", true);
    }
    bool CheckAttack()
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName("m_fight_attack_A");
    }
}
