using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData : MonoBehaviour
{
    [SerializeField]
    public WeaponStatus weaponData;
    
    public void InstalledShip(WeaponStatus myweapon)
    {
        weaponData = myweapon;
    }
    
}
