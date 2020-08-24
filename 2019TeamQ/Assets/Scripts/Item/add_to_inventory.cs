using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class add_to_inventory : MonoBehaviour
{
    public PlayerPurameter script;
    int i;


    //

    void Start()
    {

        //オブジェクト名の(clone)をなくす
        this.gameObject.name = this.gameObject.name.Replace("(Clone)", "");
        //スクリプトGmameManagerを取得
        script = GameObject.Find("GameManager").GetComponent<PlayerPurameter>();
    }


     void OnTriggerEnter2D(Collider2D other)
    {

        string layerName = LayerMask.LayerToName(other.gameObject.layer);
        if (layerName == "FPlayer") 
        {
            //拾った(触れた)アイテムがインベントリーにない場合
            if (script.itemFlags[this.gameObject.name] == false) {
                //このアイテムのフラグをtrueに変更し、アイテムを消す
                script.itemFlags[this.gameObject.name] = true;
                this.gameObject.SetActive(false);
                
                //アイテムの所持個数を＋１する
                foreach (string Itemname in script.NameList)
                {
                    if (this.gameObject.name==Itemname) {
                        //itemDictionaryは各アイテムの個数を動的に保持
                        script.itemDictionary[Itemname] += 1;
                        //デバックでそのアイテムの個数を表示しています。確認のためなので消してもいいです。
                        Debug.Log(script.itemDictionary[Itemname]);
                    }
                }

            }
            //拾った(触れた)アイテムが既にインベントリーにある場合
            else if (script.itemFlags[this.gameObject.name] == true)
            {
                //アイテムの所持個数を＋１する
                foreach (string Itemname in script.NameList)
                {
                    if (this.gameObject.name == Itemname)
                    {
                        //同じく、＋1する前後でデバックで個数表示しています。消してもいいです。
                        Debug.Log(script.itemDictionary[Itemname]);
                        script.itemDictionary[Itemname] += 1;

                        Debug.Log(script.itemDictionary[Itemname]);
                    }
                }
                //拾ったので、その触れたアイテムを消す
                this.gameObject.SetActive(false);
            }
        } 
    }
}
