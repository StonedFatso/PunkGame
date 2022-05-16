using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private float v = 0f;
    bool run = false;
    private NavMeshAgent _agent;
    private float attackSpeed = 1;

    private float attackTime = 0;
    GameObject Player;

    CharController controller;
    // Start is called before the first frame update
    void Awake()
    {
        controller = GetComponent<CharController>();
        _agent = GetComponent<NavMeshAgent>();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player = other.gameObject;
            //Vector3 lookat = Player.transform.position - transform.position;
            //transform.rotation = Quaternion.LookRotation(new Vector3(lookat.x, 0, lookat.z));
            _agent.destination = Player.transform.position;
            if (v == 0)
                v = 2f;
        }
    }

    /*void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (attackTime >= attackSpeed)
            {
                Debug.Log("attack");
                controller.Attack(0);
                attackTime = 0;
            }
        }
    }*/

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (v == 2)
                v = 0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!_agent.pathPending && _agent.remainingDistance > _agent.stoppingDistance + 0.5f)
        {
            if (v == 0)
                v = 2f;
        }
        else if (!_agent.pathPending && _agent.remainingDistance < _agent.stoppingDistance + 0.5f)
        {
            if (v == 2)
                v = 0f;
        }

        if (Player != null && Vector3.Distance(transform.position, Player.transform.position) <= 1.0)
        {
            if (attackTime >= attackSpeed)
            {
                controller.Attack(0);
                attackTime = 0;
            }
        }


        controller.AnimMovement(false, v); 
        if (attackTime < attackSpeed)
            attackTime = attackTime + Time.deltaTime;
    }
}
