using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    int level;
    int MaxHP;
    int NowHP;
    int ATK;
    int DFE;
    float hungry;
    int EXP_rui;
    int EXP_nextrui;
    
    
    int floorlevel;



 //ここからアイテム管理導入

//　アイテムを持っているかどうかのフラグ
public Dictionary<string, bool> itemFlags = new Dictionary<string, bool>();
    //　アイテムデータベース
    [SerializeField]
    private ItemDataBase itemDataBase;

    private void Start()
    {
        //　とりあえず全てのアイテムのフラグを作成
        foreach (var item in itemDataBase.GetItemDataList())
        {
            itemFlags.Add(item.GetItemName(), false);
        }
        //　とりあえず適当にアイテムを持っていることにしない
        itemFlags["FlashLight"] = false;
        itemFlags["BroadSword"] = false;
        itemFlags["HandGun"] = false;
    }

    //　アイテムを所持しているかどうか
    public bool GetItemFlag(string itemName)
    {
        return itemFlags[itemName];
    }

    //ここまでアイテム管理




    //経験値を得たとき
    public void GetExperience(int exp)
    {
        EXP_rui += exp;
        if (EXP_rui >= EXP_nextrui)
        {
            EXP_rui -= EXP_nextrui;
            level += 1;
            EXP_nextrui = Mathf.RoundToInt(EXP_nextrui * 1.2f);
            MaxHP = Mathf.RoundToInt(MaxHP * 1.2f);
            ATK = Mathf.RoundToInt(ATK * 1.2f);
            DFE = Mathf.RoundToInt(DFE * 1.2f);

        }
    }
}
