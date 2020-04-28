using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[SerializeField]
[CreateAssetMenu(fileName  = "Weapon", menuName = "CreateWeapon")]
public class Weapon : ScriptableObject
{
    [SerializeField]
    private Sprite icon;
    public Sprite Icon
    {
        get { return icon; }
    }

    [SerializeField]
    private string weaponName;
    public string WeaponName
    {
        get { return weaponName; }
    }

    [SerializeField]
    private int[] size ;
    public int[] Size
    {
        get { return size; }
    }

    [SerializeField]
    private string explanatory;
    public string Explanatory
    {
        get { return explanatory; }
    }

    [SerializeField]
    private string material;
    public string Material
    {
        get { return material; }
    }

    [SerializeField]
    private GameObject weaponObject;
    public GameObject WeaponObject
    {
        get { return weaponObject; }
    }
}
