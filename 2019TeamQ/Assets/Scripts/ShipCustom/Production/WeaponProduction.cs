using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponProduction : MonoBehaviour
{
    public Image weaponImage;
    public Text nameText;
    public Text sizeText;
    public Text explanatoryText;
    public Text materialText;
    
    [SerializeField]
    private WeaponDataBase weaponDataBase;

    private ShipCustomManager shipCustomManager;

    private Weapon showWeapon;
    private int weaponNumber;

    private GameObject goInstallation;

    private void Awake()
    {
        goInstallation = this.transform.Find("GoInstallation").gameObject;
    }
    // Start is called before the first frame update
    void Start()
    {
        shipCustomManager = GameObject.Find("ShipCustomManager").GetComponent<ShipCustomManager>();
    }

    private void OnEnable()
    {
        weaponNumber = 0;
        showWeapon = weaponDataBase.WeaponLists[weaponNumber];
        weaponImage.sprite = showWeapon.Icon;
        nameText.text = showWeapon.WeaponName;
        sizeText.text = showWeapon.Size[0].ToString() + " × " + showWeapon.Size[1].ToString();
        explanatoryText.text = showWeapon.Explanatory;
        materialText.text = showWeapon.Material;
    }

    public void NextWeaponButton()
    {
        weaponNumber++;
           
       if(weaponNumber == weaponDataBase.WeaponLists.Count)
        {
            weaponNumber = 0;
        }

        showWeapon = weaponDataBase.WeaponLists[weaponNumber];
        weaponImage.sprite = showWeapon.Icon;
        nameText.text = showWeapon.WeaponName;
        sizeText.text = showWeapon.Size[0].ToString() + " × " + showWeapon.Size[1].ToString();
        explanatoryText.text = showWeapon.Explanatory;
        materialText.text = showWeapon.Material;

    }
    
    public void ReturnWeaponButton()
    {
        weaponNumber--;
        if(weaponNumber == -1)
        {
            weaponNumber = weaponDataBase.WeaponLists.Count - 1;
        }

        showWeapon = weaponDataBase.WeaponLists[weaponNumber];
        weaponImage.sprite = showWeapon.Icon;
        nameText.text = showWeapon.WeaponName;
        sizeText.text = showWeapon.Size[0].ToString() + " × " + showWeapon.Size[1].ToString();
        explanatoryText.text = showWeapon.Explanatory;
        materialText.text = showWeapon.Material;
    }

    public void ProductWeaponButton()
    {
        WeaponStatus weaponStatus = new WeaponStatus
        {
            weapon = showWeapon
        };
        shipCustomManager.warehouseWeapons.Add(weaponStatus);
        Debug.Log(weaponStatus);
        shipCustomManager.Save();
        goInstallation.SetActive(true);
    }

    public void RetrunButton()
    {
        shipCustomManager.StartCoroutine(shipCustomManager.ChangeMenu("ProductionMenu", "MainMenu"));
    }

    public void WhetherGoInstallation(int answer)
    {
        switch (answer)
        {
            case 0: //移動する
                goInstallation.SetActive(false);
                shipCustomManager.StartCoroutine(shipCustomManager.ChangeMenu("ProductionMenu", "InstallationMenu"));
                break;
            case 1: //移動しない
                goInstallation.SetActive(false);
                break;
            default:
                break;
        }
    }
}
