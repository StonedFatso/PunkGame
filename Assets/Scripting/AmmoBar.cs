using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoBar : MonoBehaviour
{
    [SerializeField] Weapon WeaponController;

    [SerializeField] Text Text;

    // Start is called before the first frame update
    void Start()
    {
        Text.text = "➽" + WeaponController.Ammo.ToString();
        WeaponController.AmmoChanged += OnAmmoChanged;
    }

    void OnAmmoChanged(int ammo)
    {
        Text.text = "➽" + ammo.ToString();
    }
}
