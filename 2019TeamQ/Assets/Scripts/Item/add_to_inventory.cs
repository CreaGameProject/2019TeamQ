using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class add_to_inventory : MonoBehaviour
{
   public Type type; 
    public PlayerStatus script; 
    //アイテムの方のスクリプトもここで後で宣言 
 
    public enum Type
    { 
        equipment, item 
    } 
 
    void Start()
    { 
        script = GameObject.Find("FPlayer ").GetComponent<PlayerStatus>(); 
    } 
     
    void OnCollisionEnter(Collision col)
    { 
        if(col.gameObject.tag == "Player") 
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
