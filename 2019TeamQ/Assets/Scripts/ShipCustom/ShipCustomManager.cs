using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    
public class ShipCustomManager : MonoBehaviour
{
    [SerializeField]
    private ShipData shipData;

    private GameObject canvas;
    private FadeController fadeController;

    public List<WeaponStatus> warehouseWeapons { get; set; } = new List<WeaponStatus>();
    
    public int[,] shipMap = new int[9, 5];
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");
        fadeController  = canvas.transform.Find("FadePanel").GetComponent<FadeController>();
        if (PlayerPrefs.HasKey("ShipData"))
        {
            Load();
        }
        fadeController.StartCoroutine("StartFadeIn");
    }

    public IEnumerator ChangeMenu(string before, string after)
    {
        yield return fadeController.StartCoroutine("StartFadeOut");
        canvas.transform.Find(before).gameObject.SetActive(false);
        canvas.transform.Find(after).gameObject.SetActive(true);
        yield return fadeController.StartCoroutine("StartFadeIn");
    }

    public void Save()
    {
        shipData = new ShipData();
        GameObject[] weapon = GameObject.FindGameObjectsWithTag("Weapon");
        foreach(var i in weapon)
        {
            WeaponStatus weaponStatus = i.GetComponent<WeaponData>().weaponData;
            weaponStatus.weaponPos = i.transform.position;
            shipData.OnShipWeapon.Add(weaponStatus);
        }
        shipData.WarehouseWeapons = warehouseWeapons;
        string json = JsonUtility.ToJson(shipData);
        PlayerPrefs.SetString("ShipData", json);
        Debug.Log(json);
    }

    public void Load()
    {
        GameObject[] onshipWeapon = GameObject.FindGameObjectsWithTag("Weapon");
        foreach(var i in onshipWeapon)
        {
            Destroy(i);
        }
        string json = PlayerPrefs.GetString("ShipData");
        shipData = JsonUtility.FromJson<ShipData>(json);
        Debug.Log(json);
        warehouseWeapons = shipData.WarehouseWeapons;
        foreach(var i in shipData.OnShipWeapon)
        {
            GameObject weapon = Instantiate(i.weapon.WeaponObject, i.weaponPos, Quaternion.identity);
            weapon.GetComponent<WeaponData>().InstalledShip(i);
            Debug.Log(weapon);
        }
    }

    public void DataDelete()
    {
        PlayerPrefs.DeleteKey("ShipData");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
