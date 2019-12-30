using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class EquipSlot : MonoBehaviour

{

    public ItemData WeaponState;//ItemDataで定義
    public GameObject EquipText; //equipテキストを取得
    [SerializeField]
     private ProcessingSlot processingSlot;//ProcessingSlotスクリプトを取得
    GameObject ObjectB;//オブジェクト変数を定義
    PlayerPurameter codeB;//PlayerPuramaterスクリプトを取得

    int WAtk;//武器攻撃力


    void Start()
    {
        ObjectB = GameObject.Find("GameManager");//Gamemanagerオブジェクトを取得
        codeB = ObjectB.GetComponent< PlayerPurameter >();//PlayerPurameterを取得

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //マウスがクリックされたら
        {
 
                     WeaponState = GetComponent<ProcessingSlot>().myItemData; //スロットにあるアイテムの名前を取得し変数に代入

            if (codeB.CurrentWeaponState == WeaponState)//currentweaponにスロットにあるアイテムが装備してある場合
            {
                codeB.PAtkDown(WAtk);
                codeB.CurrentWeaponState = codeB.itemDataList[0];//素手をcurrentweaponに装備
                EquipText.SetActive(false);//装備テキストを解除

            }else if(codeB.CurrentWeaponState != WeaponState)//currentweaponにスロットにあるアイテムが装備してある場合

            {
            
                codeB.CurrentWeaponState = WeaponState; //currentweaponにスロットにあるアイテムを装備する
                EquipText.SetActive(true);    //装備テキストを挿入
                WAtk = codeB.CurrentWeaponState.GetItemPower();
                codeB.PAtkUp(WAtk);
                Debug.Log("攻撃力"+codeB.PAtk);
            }
               
        }
         
    }
        
}

