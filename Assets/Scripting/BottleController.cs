using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleController : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    private bool throwed = false;
    private Vector3 target;
    [SerializeField]
    public float speed = 10f;
    [SerializeField]
    private float lifetime = 20;
    private float timer = 0;

    private Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        timer = timer + Time.deltaTime;
        /*if (throwed && transform != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            if (transform.position == target)
                throwed = false;
        }*/

        if (timer >= lifetime)
            Object.Destroy(gameObject);
    }

    public void Throw(Vector3 t)
    {
        throwed = true;
        target = t;
        Vector3 dir = (target - transform.position).normalized;
        rigidbody.AddForce(dir * speed, ForceMode.Impulse);
        //transform.rotation = Quaternion.AngleAxis(93f, new Vector3(0f, 0f, 1f));
    }

    public void Throw(Vector3 t, Transform cam)
    {
        throwed = true;
        target = t;
        Vector3 dir = cam.forward;
        rigidbody.AddForce(dir * speed, ForceMode.Impulse);
        //transform.rotation = Quaternion.AngleAxis(93f, new Vector3(0f, 0f, 1f));
    }
}
