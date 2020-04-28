using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ShipData 
{ 
    public List<WeaponStatus> WarehouseWeapons = new List<WeaponStatus>();
    public List<WeaponStatus> OnShipWeapon = new List<WeaponStatus>();
}
