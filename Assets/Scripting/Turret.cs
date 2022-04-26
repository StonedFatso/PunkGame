using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    // Start is called before the first frame update
    CharController controller;
    [SerializeField]
    private GameObject Weapon;
    [SerializeField]
    float throwSpeed = 3;

    private float throwTime = 0;
    private GameObject throwable;

    private void Start()
    {
        controller = GetComponent<CharController>();
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Trigerred");
            GameObject Player = other.gameObject;
            Vector3 lookat = Player.transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(new Vector3(lookat.x, 0, lookat.z));
            Debug.Log(lookat);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject Player = other.gameObject;
            Vector3 lookat = Player.transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(new Vector3(lookat.x, 0, lookat.z));
            if (throwTime >= throwSpeed)
            {
                throwable = Instantiate(Weapon, transform.position + transform.forward * 1 + transform.up * 0.5f, transform.rotation);
                BottleController thrower = throwable.GetComponent<BottleController>();
                controller.Attack(2);
                //thrower.Throw(other.gameObject.transform.position - transform.position);
                thrower.Throw(other.gameObject.transform.position);
                throwTime = 0;
            }
        }
    }

    private void Update()
    {
        throwTime = throwTime + Time.deltaTime;
    }
}
