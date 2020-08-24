using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponInstallation : MonoBehaviour
{
    [SerializeField]
    private GameObject weaponSlot;

    [SerializeField]
    private ShipCustomManager shipCustomManager;

    private Transform Content;
    private GameObject whetherToSave;

    private bool noSave = false;

    private GameObject choosingWeapon;
    private WeaponData choosingWeaponData;
    private Vector2 moveTo;

    private void Awake()
    {
        Content = this.transform.Find("SideMenu/HasWeaponLilst/Content");
        whetherToSave = this.transform.Find("WhetherToSave").gameObject;
    }
    // Start is called before the first frame update
    void Start()
    {
        shipCustomManager = GameObject.Find("ShipCustomManager").GetComponent<ShipCustomManager>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RayCheck();
        }else if (Input.GetMouseButton(0) && choosingWeapon != null)
        {
            MovePosition();
        }
    }

    private void RayCheck()
    {
        
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        if (hit)
        {
            if (hit.collider.tag == "Weapon")
            {
                if(choosingWeapon != null && hit.collider.gameObject != choosingWeapon)
                {
                    choosingWeapon.GetComponent<Renderer>().sortingOrder = 0;
                    choosingWeapon.GetComponent<WeaponMoveController>().Choosing = false;
                }
                if (hit.collider.gameObject != choosingWeapon)
                {
                    choosingWeapon = hit.collider.gameObject;
                    choosingWeaponData = choosingWeapon.GetComponent<WeaponData>();
                    choosingWeapon.GetComponent<Renderer>().sortingOrder = 1;
                    choosingWeapon.GetComponent<WeaponMoveController>().Choosing = true;
                    noSave = true;
                    Debug.Log(choosingWeapon);
                }
            }
              
        }
        
    }

    private void MovePosition()
    {

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;

        moveTo = Camera.main.ScreenToWorldPoint(mousePos);
        moveTo.x = Mathf.RoundToInt(moveTo.x);
        moveTo.y = Mathf.RoundToInt(moveTo.y);
        
        if(0 <= moveTo.x && moveTo.x < 9 && 0 <= moveTo.y && moveTo.y < 5)
        {
            
            if(9 - choosingWeaponData.weaponData.weapon.Size[0] < moveTo.x && moveTo.x < 9)
            {
                moveTo.x -= (choosingWeaponData.weaponData.weapon.Size[0] - 1);
            }
            if(5 - choosingWeaponData.weaponData.weapon.Size[1] < moveTo.y && moveTo.y < 5)
            {
                moveTo.y -= (choosingWeaponData.weaponData.weapon.Size[1] - 1);
            }
            choosingWeapon.transform.position = moveTo;
        }
        

    }

    public void InstalledWeapon( WeaponStatus createWeapon)
    {
        if(choosingWeapon != null)
        {
            choosingWeapon.GetComponent<Renderer>().sortingOrder = 0;
            choosingWeapon.GetComponent<WeaponMoveController>().Choosing = false;
        }
        GameObject weapon = Instantiate<GameObject>(createWeapon.weapon.WeaponObject, new Vector3(4, 2, 0), Quaternion.identity);
        choosingWeapon = weapon;
        choosingWeaponData = choosingWeapon.GetComponent<WeaponData>();
        choosingWeaponData.InstalledShip(createWeapon);
        choosingWeapon.GetComponent<Renderer>().sortingOrder = 1;
        choosingWeapon.GetComponent<WeaponMoveController>().Choosing = true;
        shipCustomManager.warehouseWeapons.Remove(createWeapon);
        noSave = true;
    }

   

    private void OnEnable()
    {
        WeaponUpdate();
    }
    public void WeaponUpdate()
    {
        Content.gameObject.SetActive(false);
        Content.gameObject.SetActive(true);
        int i = 0;
        foreach(var weapon in shipCustomManager.warehouseWeapons)
        {
            GameObject weaponInstance = Instantiate<GameObject>(weaponSlot, Content);
            weaponInstance.name = "WeaponSlot" + i++;
            weaponInstance.GetComponent<WeaponProcessingSlot>().SetWeaponData(weapon);
        }
    }

    public void ReturnButton()
    {
        if (noSave)
        {
            whetherToSave.SetActive(true);
        }
        else
        {
            shipCustomManager.StartCoroutine(shipCustomManager.ChangeMenu("InstallationMenu", "MainMenu"));
        }
    }

    public void SettingButton()
    {
        shipCustomManager.Save();
        if (choosingWeapon != null)
        {
            choosingWeapon.GetComponent<Renderer>().sortingOrder = 0;
            choosingWeapon.GetComponent<WeaponMoveController>().Choosing = false;
            choosingWeapon = null;
        }
        noSave = false;
    }

    public void ToWarehouseButton()
    {
        shipCustomManager.warehouseWeapons.Add(choosingWeaponData.weaponData);
        WeaponUpdate();
        Destroy(choosingWeapon);
    }

    
    public void WhetherSave(int answer)
    {
        switch (answer)
        {
            case 0: //保存する
                SettingButton();
                whetherToSave.SetActive(false);
                ReturnButton();
                break;
            case 1: //保存しない
                shipCustomManager.Load();
                choosingWeapon.GetComponent<Renderer>().sortingOrder = 0;
                choosingWeapon.GetComponent<WeaponMoveController>().Choosing = false;
                choosingWeapon = null;
                noSave = false;
                whetherToSave.SetActive(false);
                ReturnButton();
                break;
            default:
                break;
        }
    }



   
}
