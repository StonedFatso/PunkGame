using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Dictionary<string, int> weaponList = new Dictionary<string, int>()
    { 
        { "unarmed", 0 },
        { "bat", 3 },
        { "bottle", 1 },
    };
    private string currentWeapon = "unarmed";
    private int w_Health = 0;
    CharController controller;
    // Start is called before the first frame update
    private void Start()
    {
        controller = GetComponent<CharController>();
    }

    private GameObject GetChildByName(GameObject entObj, string name)
    {
        var children = entObj.GetComponentsInChildren<Transform>();
        foreach (var child in children)
        {
            if (child.name == name)
            {
                GameObject obj = child.gameObject;
                return obj;
            }
        }
        return null;
    }

    public void SetWeapon(string weapon)
    {
        if (currentWeapon != weapon)
        {
            if (currentWeapon != "unarmed")
            {
                GameObject w_obj = GetChildByName(gameObject, currentWeapon);
                w_obj.GetComponent<MeshRenderer>().enabled = false;
            }
            if (weapon != "unarmed")
            {
                GameObject w_obj = GetChildByName(gameObject, weapon);
                w_obj.GetComponent<MeshRenderer>().enabled = true;
            }
        }

        currentWeapon = weapon;
        w_Health = weaponList[weapon];
    }    

    public void Attack()
    {
        switch(currentWeapon)
        {
            case "unarmed":
                controller.Attack(0);
                break;
            case "bat":
                controller.Attack(1);
                w_Health--;
                if (w_Health < 1)
                    SetWeapon("unarmed");
                break;
            case "bottle":
                controller.Attack(2);
                w_Health--;
                if (w_Health < 1)
                    SetWeapon("unarmed");
                break;
        }
    }
}
