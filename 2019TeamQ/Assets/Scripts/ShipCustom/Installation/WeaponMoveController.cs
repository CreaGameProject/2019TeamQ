using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponMoveController : MonoBehaviour
{
    private bool choosing = false;
    public bool Choosing { get
        {
            return choosing;
        }
        set
        {
            choosing = value;
            if (choosing)
            {
                backGraound.color = new Color(0, 255, 0, 60);
            }else if (!choosing)
            {
                backGraound.color = new Color(0, 0, 0, 0);
            }
        }
    }

    Button saveButton;
    SpriteRenderer backGraound;

    private void Awake()
    {
        backGraound = gameObject.transform.Find("BackGraound").GetComponent<SpriteRenderer>();
    }
    //// Start is called before the first frame update
    void Start()
    {
       
        saveButton = GameObject.Find("Canvas").transform.Find("InstallationMenu/SideMenu/Save").gameObject.GetComponent<Button>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Weapon")
        {
            if (saveButton != null &&  choosing)
            {
                saveButton.interactable = false;
                backGraound.color = new Color(255, 0, 0, 60);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Weapon")
        {
            if (saveButton != null && choosing)
            {
                saveButton.interactable = true;
                backGraound.color = new Color(0, 255, 0, 60);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
