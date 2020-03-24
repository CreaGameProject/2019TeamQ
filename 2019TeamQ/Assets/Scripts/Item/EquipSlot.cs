using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class EquipSlot : MonoBehaviour

{
    GameObject EquipText; //equipテキストを取得
    ItemData Itemdata;//クリックされたアイテムのデータ用変数


    PlayerPurameter codeB; //PlayerPuramaterスクリプトを取得

    int i;//装備状態変数　0で非装備、1で装備

    PointerEventData pointer;
    GameObject clickedGameObject;//ray用変数

    Text Quantity_text;//アイテム数テキスト

    void Start()
    {
        codeB = GetComponent<PlayerPurameter>();//PlayerPurameterを取得
        i = 0;  //非装備状態
        pointer = new PointerEventData(EventSystem.current);


    }

    void Update()
    {
        codeB.PAtkUp();
        if (Input.GetMouseButtonDown(0))
        {


            List<RaycastResult> results = new List<RaycastResult>();
            pointer.position = Input.mousePosition;
            EventSystem.current.RaycastAll(pointer, results);



            foreach (RaycastResult target in results)
            {
                clickedGameObject = target.gameObject;

                if (clickedGameObject.tag == "itemslot")
                {
                    EquipText = clickedGameObject.transform.GetChild(1).gameObject;　//クリックされたスロットのEテキストを指定
                    Itemdata = clickedGameObject.GetComponent<ProcessingSlot>().myItemData; //クリックされたスロットのアイテム情報を代入
                    if (Itemdata.GetItemType()=="武器") {　//武器の場合
                        soubi();
                    }else if (Itemdata.GetItemType() == "HP消費")　//消費アイテムの場合
                    {
                        syouhi();
                    }
                }
            }

        }
        Debug.Log("攻撃力"+codeB.PAtk);
        Debug.Log("体力" + codeB.PNowHP);
    }

    public void soubi()
    {

        if (i == 1 && EquipText.activeSelf == true)//currentweaponにスロットにあるアイテムが装備してある場合
        {
            codeB.CurrentWeaponState = codeB.itemDataList[0];//currentWeaponStateを素手に、装備を外す
            i--;
        }
        else if (i == 0 && EquipText.activeSelf == false)//currentweaponにスロットにあるアイテムが装備していない場合

        {
            codeB.CurrentWeaponState = clickedGameObject.GetComponent<ProcessingSlot>().myItemData; //currentweaponにスロットにあるアイテムを装備する
            i++;

        }
        else if (i == 1 && EquipText.activeSelf == false)//currentweaponにスロットにあるアイテムが装備していない場合

        {
            codeB.CurrentWeaponState = codeB.itemDataList[0];
            codeB.CurrentWeaponState = Itemdata; //currentweaponにスロットにあるアイテムを装備する

        }

    }

    public void syouhi()
    {
        if (codeB.itemDictionary[Itemdata.GetItemName()]>1)//個数が2つ以上の場合
        {
            codeB.itemDictionary[Itemdata.GetItemName()] -= 1;//個数-1

            //アイテム数テキストの数字を1減らす
            Quantity_text= clickedGameObject.transform.GetChild(2).gameObject.GetComponent<Text>();
            string itemkazu = Quantity_text.text;
            int a = int.Parse(itemkazu);
            a--;
            Quantity_text.text = "" +a;
            //ここまで
        }
        else if (codeB.itemDictionary[Itemdata.GetItemName()] == 1) {//個数が1つの場合

            codeB.itemDictionary[Itemdata.GetItemName()] -= 1;//個数-1
            codeB.itemFlags[Itemdata.GetItemName()] = false;//アイテムを所持していないことにする
            clickedGameObject.SetActive(false);//アイテムスロットを非表示にする

            //アイテム数テキストの数字を1減らす
            Quantity_text = clickedGameObject.transform.GetChild(2).gameObject.GetComponent<Text>();
            string itemkazu = Quantity_text.text;
            int a = int.Parse(itemkazu);
            a--;
            Quantity_text.text = "" + a;
            //ここまで
        }
        codeB.PNowHPUP(Itemdata.GetItemPower());//hp回復

    }

}

