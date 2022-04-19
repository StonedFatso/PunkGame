using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{

    [SerializeField]
    float speed = 4;
    [SerializeField]
    float gravity = 8;

    Vector3 moveDir = Vector3.zero;

    CharacterController controller;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetBool("attacking") && CheckAttack())
        {
            anim.SetBool("attacking", false);
        }
    }

    public void Movement(bool run, float v)
    {
        if (controller.isGrounded)
        {
            moveDir = new Vector3(0, 0, v);
            if (run)
            {
                moveDir *= speed * 2;
            }
            else
            {
                moveDir *= speed;
            }
            moveDir = transform.TransformDirection(moveDir);

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

    public void Attack()
    {
        anim.SetBool("attacking", true);
    }
    bool CheckAttack()
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName("m_fight_attack_A");
    }
}
