using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    ShipCustomManager shipCustomManager;

    // Start is called before the first frame update
    void Start()
    {
        shipCustomManager = GameObject.Find("ShipCustomManager").GetComponent<ShipCustomManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnProduction()
    {
        shipCustomManager.StartCoroutine(shipCustomManager.ChangeMenu("MainMenu", "ProductionMenu"));
    }

    public void OnInstallation()
    {
        shipCustomManager.StartCoroutine(shipCustomManager.ChangeMenu("MainMenu", "InstallationMenu"));
    }
}
