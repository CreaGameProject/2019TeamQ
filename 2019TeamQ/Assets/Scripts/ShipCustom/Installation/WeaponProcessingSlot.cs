using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponProcessingSlot : MonoBehaviour
{
    private Text informationText;

    [SerializeField]
    private GameObject weapontSlotTitleUI;

    private GameObject weaponSlotTitleUIInstance;

    private WeaponStatus weaponStatus;

    private WeaponInstallation weaponInstallation;

    private void OnDisable()
    {

        if(weaponSlotTitleUIInstance != null)
        {
            Destroy(weaponSlotTitleUIInstance);
        }
        Destroy(gameObject);
    }

    public void SetWeaponData(WeaponStatus weaponData)
    {
        weaponStatus = weaponData;
        transform.GetChild(0).GetComponent<Image>().sprite = weaponStatus.weapon.Icon;
        transform.GetChild(1).GetComponent<Text>().text = weaponStatus.weapon.WeaponName;
    }

    public void OnClick()
    {
        weaponInstallation = GameObject.Find("InstallationMenu").GetComponent<WeaponInstallation>();
        weaponInstallation.InstalledWeapon(weaponStatus);
        this.gameObject.SetActive(false);
    }
}
