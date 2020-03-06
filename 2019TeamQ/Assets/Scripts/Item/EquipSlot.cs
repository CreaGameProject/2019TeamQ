using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class EquipSlot : MonoBehaviour

{
    GameObject EquipText; //equipテキストを取得
    ItemData Itemdata;//クリックされたアイテムのデータ用変数
    [SerializeField]
    private ProcessingSlot processingSlot;//ProcessingSlotスクリプトを取得

    PlayerPurameter codeB; //PlayerPuramaterスクリプトを取得

    int WAtk;//武器攻撃力
    int i;//装備状態変数　0で非装備、1で装備

    PointerEventData pointer;
    GameObject clickedGameObject;//ray用変数

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
                    }else if (Itemdata.GetItemType() == "消費")　//消費アイテムの場合
                    {
                        syouhi();
                    }
                }
            }

        }
        Debug.Log("攻撃力"+codeB.PAtk);
    }

    public void soubi()
    {

        if (i == 1 && EquipText.activeSelf == true)//currentweaponにスロットにあるアイテムが装備してある場合
        {
            codeB.CurrentWeaponState = codeB.itemDataList[0];//currentWeaponStateをnull、装備を外す
            i--;
        }
        else if (i == 0 && EquipText.activeSelf == false)//currentweaponにスロットにあるアイテムが装備していない場合

        {
            codeB.CurrentWeaponState = clickedGameObject.GetComponent<ProcessingSlot>().myItemData; //currentweaponにスロットにあるアイテムを装備する
            WAtk = codeB.CurrentWeaponState.GetItemPower();
            i++;

        }
        else if (i == 1 && EquipText.activeSelf == false)//currentweaponにスロットにあるアイテムが装備していない場合

        {
            codeB.CurrentWeaponState = codeB.itemDataList[0];
            codeB.CurrentWeaponState = Itemdata; //currentweaponにスロットにあるアイテムを装備する
            WAtk = codeB.CurrentWeaponState.GetItemPower();

        }

    }

    public void syouhi()
    {
        if (codeB.itemDictionary[Itemdata.GetItemName()]>1)
        {
            codeB.itemDictionary[Itemdata.GetItemName()] -= 1;
        }
        else if (codeB.itemDictionary[Itemdata.GetItemName()] == 1) {

            codeB.itemDictionary[Itemdata.GetItemName()] -= 1;
            codeB.itemFlags[Itemdata.GetItemName()] = false; 
        }
        codeB.PNowHPUP(Itemdata.GetItemPower());

    }

}

