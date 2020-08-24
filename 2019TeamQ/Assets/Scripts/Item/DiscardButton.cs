using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//捨てるボタンを押したときに実行されるスクリプト。
public class DiscardButton : MonoBehaviour
{
    ItemData Itemdata;//クリックされたアイテムのデータ用変数



    GameObject GameManager; //Gamemanagerそのものが入る変数

    EquipSlot codeE; //Equipslotスクリプトが入る変数

    PlayerPurameter codeB; //PlayerPuramaterスクリプトが入る変数

    Text Quantity_text;//アイテム数テキスト


    void Start()
    {
        GameManager = GameObject.Find("GameManager");
        codeE = GameManager.GetComponent<EquipSlot>();//EquipSlotスクリプト(以下、codeE)を取得
        codeB = GameManager.GetComponent<PlayerPurameter>();//PlayerPurameter(以下、codeB)を取得。
        Itemdata = null;

    }

    public void Onclick()
    {
        Itemdata = codeE.Itemdata;

        if (codeB.itemDictionary[Itemdata.GetItemName()] > 1)//個数が2つ以上の場合
        {
            codeB.itemDictionary[Itemdata.GetItemName()] -= 1;//個数-1

            //アイテム数テキストの数字を1減らす
            Quantity_text = codeE.clickedGameObject.transform.GetChild(2).gameObject.GetComponent<Text>();
            string itemkazu = Quantity_text.text;
            int a = int.Parse(itemkazu);
            a--;
            Quantity_text.text = "" + a;

            //選択したそのスロットに格納されたアイテム情報をcodeEから取り出し、Itemdata変数に格納する。
            //ボタン(使用、投げる、捨てるの各種ボタン)を非表示にし、ボタンの表示状態を非表示に設定する
            codeE.b--;
            codeE.clickedGameObject.transform.GetChild(3).gameObject.SetActive(false);
        }
        else if (codeB.itemDictionary[Itemdata.GetItemName()] == 1&&Itemdata!= codeB.CurrentshieldState&&Itemdata!= codeB.CurrentWeaponState)
        {//個数が1つかつ、現在装備していない場合 
            codeB.itemDictionary[Itemdata.GetItemName()] -= 1;//個数-1
            codeB.itemFlags[Itemdata.GetItemName()] = false;//アイテムを所持していないことにする
            codeE.clickedGameObject.SetActive(false);//アイテムスロットを非表示にする

            //アイテム数テキストの数字を1減らす
            Quantity_text = codeE.clickedGameObject.transform.GetChild(2).gameObject.GetComponent<Text>();
            string itemkazu = Quantity_text.text;
            int a = int.Parse(itemkazu);
            a--;
            Quantity_text.text = "" + a;

            //選択したそのスロットに格納されたアイテム情報をcodeEから取り出し、Itemdata変数に格納する。
            //ボタン(使用、投げる、捨てるの各種ボタン)を非表示にし、ボタンの表示状態を非表示に設定する
            codeE.b--;
            codeE.clickedGameObject.transform.GetChild(3).gameObject.SetActive(false);
        }
        else if(codeB.itemDictionary[Itemdata.GetItemName()] == 1 && (Itemdata == codeB.CurrentshieldState || Itemdata == codeB.CurrentWeaponState))
        {//個数が1つかつ、現在装備している場合

            Debug.Log("装備しているため捨てられないよ!");
        }


    }
}
