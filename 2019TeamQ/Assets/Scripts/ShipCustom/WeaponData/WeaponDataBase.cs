using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "WeaponDataBase", menuName =  "CreateWeaponDataBase")]
public class WeaponDataBase : ScriptableObject
{
    [SerializeField]
    private List<Weapon> weaponLists = new List<Weapon>();
    public List<Weapon> WeaponLists
    {
        get { return weaponLists; }
    }
}
