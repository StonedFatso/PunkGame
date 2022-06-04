using System.Timers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupUsable : MonoBehaviour
{
    [SerializeField]
    private string weapon;
    [SerializeField]
    private string item;
    [SerializeField]
    private int cureAmount = 30;
    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private bool restore = false;
    [SerializeField]
    private double restoreSeconds = 30;


    private float eulery = 0;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (weapon.Length > 0)
            {
                Debug.Log("WEAPON PICKUP");
                other.gameObject.GetComponent<Weapon>().SetWeapon(weapon);
                Object.Destroy(gameObject);
            }
            else if (item.Length > 0)
            {
                Debug.Log("ITEM PICKUP");
                switch (item)
                {
                    case "health":
                        other.gameObject.GetComponent<Health>().Cure(cureAmount);
                        break;
                    case "booze":

                        break;
                }
                Object.Destroy(gameObject);
            }
            
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
