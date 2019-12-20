using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class add_to_inventory : MonoBehaviour
{
   public Type type; 
    public PlayerPurameter script; 
    //アイテムの方のスクリプトもここで後で宣言 
 
    public enum Type
    { 
        equipment, item 
    } 
 
    void Start()
    { 
        script = GameObject.Find("EventSystem ").GetComponent<PlayerPurameter>();
    } 
     
    void OnTriggerEnter2D(Collider2D other)
    {
        
        string layerName = LayerMask.LayerToName(other.gameObject.layer);
        if (layerName == "FPlayer") 
        {
            if (type == Type.equipment) 
            {
                script.itemFlags["BroadSword"] = true; 
                this.gameObject.SetActive(false); 
            } 
            else 
            { 
                //アイテムの場合 
            } 
        } 
    }
}
