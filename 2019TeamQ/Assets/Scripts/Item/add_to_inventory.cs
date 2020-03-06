using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class add_to_inventory : MonoBehaviour
{
    public PlayerPurameter script;
    int i;




    void Start()
    {

        //オブジェクトの(clone)をなくす
        this.gameObject.name = this.gameObject.name.Replace("(Clone)", "");
        //スクリプトGmameManagerを取得
        script = GameObject.Find("GameManager").GetComponent<PlayerPurameter>();
    }


     void OnTriggerEnter2D(Collider2D other)
    {

        string layerName = LayerMask.LayerToName(other.gameObject.layer);
        if (layerName == "FPlayer") 
        {
            if (script.itemFlags[this.gameObject.name] == false) {
                //このアイテムのフラグをtrueに変更し、アイテムを消す
                script.itemFlags[this.gameObject.name] = true;
                this.gameObject.SetActive(false);
                //個数＋１

                foreach (string Itemname in script.NameList)
                {
                    if (this.gameObject.name==Itemname) {
                        script.itemDictionary[Itemname] += 1;
                        Debug.Log(script.itemDictionary[Itemname]);
                    }
                }

            }
            else if (script.itemFlags[this.gameObject.name] == true)
            {
                //個数＋１

                foreach (string Itemname in script.NameList)
                {
                    if (this.gameObject.name == Itemname)
                    {
                        Debug.Log(script.itemDictionary[Itemname]);
                        script.itemDictionary[Itemname] += 1;

                        Debug.Log(script.itemDictionary[Itemname]);
                    }
                }
                //アイテム消す
                this.gameObject.SetActive(false);
            }
        } 
    }
}
