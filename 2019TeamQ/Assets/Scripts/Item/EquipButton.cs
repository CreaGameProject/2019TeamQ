using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class EquipButton : MonoBehaviour
{

    ItemData Itemdata;//クリックされたアイテムのデータ用変数



    GameObject GameManager; //Gamemanagerそのものが入る変数

    EquipSlot codeE; //Equipslotスクリプトが入る変数


    void Start()
    {
        GameManager = GameObject.Find("GameManager");
        codeE = GameManager.GetComponent<EquipSlot>();//EquipSlotを取得
        Itemdata = null;

    }

    public void Onclick()
    {
        Itemdata = codeE.Itemdata;
        if (Itemdata.GetItemType() == "武器")
        { //武器の場合
            codeE.soubi();
        }
        else if (Itemdata.GetItemType() == "盾")
        {//盾の場合
            codeE.tate();
        }
        else if (Itemdata.GetItemType() == "HP消費") //消費アイテムの場合
        {
            codeE.syouhi();
        }
        codeE.b--;
        codeE.clickedGameObject.transform.GetChild(3).gameObject.SetActive(false);
    }
}

