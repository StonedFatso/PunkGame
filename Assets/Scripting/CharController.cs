using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CharController : MonoBehaviour
{

    [SerializeField]
    private float speed = 4;
    [SerializeField]
    private float gravity = 8;
    [SerializeField]
    private float jumpForce = 3f;
    //[SerializeField]
    //float weapon = 0;

    Vector3 moveDir = Vector3.zero;

    private CharacterController controller;
    private Animator anim;
    private Rigidbody _rigidbody;
    //CapsuleCollider coll;
    private BoxCollider coll;
    private AudioSource[] audioSources;
    private float pitch = 1;
    bool isGrounded = false;
    private NavMeshAgent _agent;
    LayerMask mask;
    private bool isDead = false;
    private bool isWalking = false;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        //coll = GetComponent<CapsuleCollider>();
        coll = GetComponent<BoxCollider>();
        audioSources = GetComponents<AudioSource>();
        mask = LayerMask.GetMask("StaticLevel");
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetBool("attacking") && CheckAttack())
        {
            anim.SetBool("attacking", false);
        }

        if (anim.GetBool("damage") && CheckDamage())
        {
            anim.SetBool("damage", false);
        }

        if (isWalking)
        {
            audioSources[0].pitch = pitch;
            if (!audioSources[0].isPlaying)
                audioSources[0].Play();
        }
        else
        {
            pitch = 1;
            audioSources[0].Stop();
        }
    }

    public void Movement(bool run, float v)
    {
        if (controller.isGrounded)
        {
            if (anim.GetBool("jump"))
                anim.SetBool("jump", false);
            moveDir = new Vector3(0, 0, v);
            isWalking = v != 0;
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
                    pitch = 1.5f;
                }
                else
                {
                    anim.SetFloat("Forward", v);
                    pitch = 1;
                }
                controller.Move(moveDir * Time.deltaTime);
            }
        }
    }

    /*private void OnCollisionStay(Collision other)
    {
        if (!isGrounded) //&& (mask.value & (1 << other.gameObject.layer)) > 0)
        {
            Debug.Log("GROUNDED");
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
    }*/

    /*public void Movement(bool run, float v)
    {
        if (!isDead && !CheckAttack())
        {

            moveDir = new Vector3(0, 0, v);
            moveDir = transform.TransformDirection(moveDir);
            isWalking = v != 0;
            if (!moveDir.Equals(new Vector3(0, 0, 0)))
            {
                //moveDir.y -= gravity * Time.deltaTime;
                if (isGrounded)
                    if (run)
                    {
                        pitch = 1.5f;
                        anim.SetFloat("Forward", v * 2);
                        transform.position = transform.position + moveDir.normalized / 10;
                    }
                    else
                    {
                        pitch = 1;
                        anim.SetFloat("Forward", v);
                        transform.position = transform.position + moveDir.normalized / 20;
                    }
                //if (run && isGrounded)
                    //transform.position = transform.position + moveDir.normalized/15;
                //else
                    //transform.position = transform.position + moveDir.normalized / 30;
            }
        }
    }*/

    public void AnimMovement(bool run, float v)
    {
        if (!CheckAttack() && _agent != null)
        {
            //moveDir.y -= gravity * Time.deltaTime;
            //if (isGrounded)
            if (_agent.isOnNavMesh)
            {
                isWalking = v != 0;
                if (run)
                {
                    pitch = 1.5f;
                    anim.SetFloat("Forward", v * 2);

                }
                else
                {
                    pitch = 1;
                    anim.SetFloat("Forward", v);
                }
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
        if (!isDead)
        {
            anim.SetFloat("method", weapon);
            anim.SetBool("attacking", true);
        }

    }

    public void Damage()
    {
        if (!isDead)
            anim.SetBool("damage", true);

    }

    public void Death()
    {
        anim.SetBool("death", true);
        isDead = true;

    }

    bool CheckAttack()
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName("AttackTree");
    }

    bool CheckDamage()
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName("Damage");
    }
}
