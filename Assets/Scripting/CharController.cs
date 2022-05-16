using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{

    [SerializeField]
    float speed = 4;
    [SerializeField]
    float gravity = 8;
    [SerializeField]
    float jumpForce = 3f;
    //[SerializeField]
    //float weapon = 0;

    Vector3 moveDir = Vector3.zero;

    CharacterController controller;
    Animator anim;
    Rigidbody _rigidbody;
    //CapsuleCollider coll;
    BoxCollider coll;
    bool isGrounded = false;
    LayerMask mask;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        //coll = GetComponent<CapsuleCollider>();
        coll = GetComponent<BoxCollider>();
        mask = LayerMask.GetMask("StaticLevel");
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetBool("attacking") && CheckAttack())
        {
            anim.SetBool("attacking", false);
        }
    }

    /*public void Movement(bool run, float v)
    {
        if (controller.isGrounded)
        {
            if (anim.GetBool("jump"))
                anim.SetBool("jump", false);
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
                    anim.SetFloat("Forward", v*2);
                }
                else
                {
                    anim.SetFloat("Forward", v);
                }
                controller.Move(moveDir * Time.deltaTime);
            }
        }
    }*/

    private void OnCollisionStay(Collision other)
    {
        if (!isGrounded) //&& (mask.value & (1 << other.gameObject.layer)) > 0)
        {
            isGrounded = true;
            if (anim.GetBool("jump"))
                anim.SetBool("jump", false);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        //if ((mask.value & (1 << other.gameObject.layer)) > 0)
        //{
            isGrounded = false;
        //}
    }

    public void Movement(bool run, float v)
    {
        if (!CheckAttack())
        {

            moveDir = new Vector3(0, 0, v);
            moveDir = transform.TransformDirection(moveDir);

            if (!moveDir.Equals(new Vector3(0, 0, 0)))
            {
                //moveDir.y -= gravity * Time.deltaTime;
                if (isGrounded)
                    if (run)
                    {
                        anim.SetFloat("Forward", v * 2);
                        transform.position = transform.position + moveDir.normalized / 15;
                    }
                    else
                    {
                        anim.SetFloat("Forward", v);
                        transform.position = transform.position + moveDir.normalized / 30;
                    }
                //if (run && isGrounded)
                    //transform.position = transform.position + moveDir.normalized/15;
                //else
                    //transform.position = transform.position + moveDir.normalized / 30;
            }
        }
    }

    public void AnimMovement(bool run, float v)
    {
        if (!CheckAttack())
        {
            //moveDir.y -= gravity * Time.deltaTime;
            if (isGrounded)
                if (run)
                    {
                        anim.SetFloat("Forward", v * 2);
                    }
                    else
                    {
                        anim.SetFloat("Forward", v);
                    }
        }
    }

    public void Jump(float v)
    {
        if (isGrounded)
        {
            anim.SetBool("jump", true);
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    public void Attack(float weapon)
    {
        anim.SetFloat("method", weapon);
        anim.SetBool("attacking", true);

    }
    bool CheckAttack()
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName("AttackTree");
    }
}
