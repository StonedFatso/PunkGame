using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Dictionary<string, int> weaponList = new Dictionary<string, int>()
    { 
        { "unarmed", 0 },
        { "bat", 10 },
        { "bottle", 1 },
        { "mine", 1 },
    };
    private string currentWeapon = "unarmed";
    private int w_Health = 0;
    public float Ammo { get => w_Health; }
    CharController controller;
    [SerializeField]
    private GameObject bottleAmmo;
    [SerializeField]
    private GameObject mineAmmo;
    [SerializeField]
    private Transform cam;
    private Health enemy;
    private LayerMask mask;
    private GameObject canvas;
    public Action<int> AmmoChanged;
    // Start is called before the first frame update
    private void Start()
    {
        controller = GetComponent<CharController>();
        mask = LayerMask.GetMask("People");
        canvas = GameObject.FindWithTag("AimOverlay");
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
            if (canvas != null)
                canvas.GetComponent<Canvas>().enabled = weapon == "bottle";
            currentWeapon = weapon;
            w_Health = weaponList[weapon];
        }
        else
        {
            switch (weapon)
            {
                case "bat":
                    w_Health = weaponList["bat"];
                    break;
                case "bottle":
                    w_Health++;
                    break;
                case "mine":
                    w_Health++;
                    break;
            }
        }
        if (weapon != "bottle")
            AmmoChanged?.Invoke(0);
        else
            AmmoChanged?.Invoke(w_Health);
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((mask.value & (1 << other.gameObject.layer)) > 0)
        {
            enemy = other.gameObject.GetComponent<Health>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((mask.value & (1 << other.gameObject.layer)) > 0)
        {
            enemy = null;
        }
    }

    public void Attack()
    {
        switch(currentWeapon)
        {
            case "unarmed":
                controller.Attack(0);
                if (enemy != null)
                    enemy.Injury(5);
                break;
            case "bat":
                controller.Attack(1);
                if (enemy != null)
                {
                    w_Health--;
                    enemy.Injury(20);
                }
                if (w_Health < 1)
                    SetWeapon("unarmed");
                break;
            case "bottle":
                controller.Attack(2);
                w_Health--;
                if (w_Health < 1)
                    SetWeapon("unarmed");
                AmmoChanged?.Invoke(w_Health);
                GameObject throwable = Instantiate(bottleAmmo, transform.position + transform.forward + transform.up * 2f, transform.rotation);
                BottleController thrower = throwable.GetComponent<BottleController>();
                if (cam != null)
                {
                    thrower.Throw(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1f, gameObject.transform.position.z), cam);
                }
                else
                {
                    thrower.Throw(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1f, gameObject.transform.position.z));
                }                
                break;
            case "mine":
                w_Health--;
                if (w_Health < 1)
                    SetWeapon("unarmed");
                GameObject mine = Instantiate(mineAmmo, transform.position + transform.forward, Quaternion.Euler(0,0,0));
                break;
        }
    }
}
