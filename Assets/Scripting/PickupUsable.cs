using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupUsable : MonoBehaviour
{
    [SerializeField]
    string weapon;
    [SerializeField]
    float speed = 10f;
    // Start is called before the first frame update
    private float eulery = 0;
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Weapon>().SetWeapon(weapon);
            Object.Destroy(gameObject);
        }
    }

    // Update is called once per frame
    /*void Update()
    {
        //transform.rotation = Quaternion.Euler(0f, , 0f);
        if (eulery < 360)
            eulery = eulery + speed * Time.deltaTime;
        else
            eulery = 0;

        transform.eulerAngles = new Vector3(0, eulery, 0);
    }*/
}
