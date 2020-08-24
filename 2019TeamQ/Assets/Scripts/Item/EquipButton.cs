using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//装備/使用ボタンを押したときに実行されるスクリプト。インベントリのアイテムを使用した時の処理をする。処理動作自体は、EquipSlotスクリプト(以下、codeE)にある。
public class EquipButton : MonoBehaviour
{

    ItemData Itemdata;//クリックされたアイテムのデータ用変数



    GameObject GameManager; //Gamemanagerそのものが入る変数

    EquipSlot codeE; //Equipslotスクリプトが入る変数


    void Start()
    {
        GameManager = GameObject.Find("GameManager");
        codeE = GameManager.GetComponent<EquipSlot>();//EquipSlotスクリプト(以下、codeE)を取得
        Itemdata = null;

    }

    public void Onclick()
    {
        Itemdata = codeE.Itemdata;//選択したそのスロットに格納されたアイテム情報をcodeEから取り出し、Itemdata変数に格納する。
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
        //ボタン(使用、投げる、捨てるの各種ボタン)を非表示にし、ボタンの表示状態を非表示に設定する
        codeE.b--;
        codeE.clickedGameObject.transform.GetChild(3).gameObject.SetActive(false);
    }
}

