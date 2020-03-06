﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerPurameter : MonoBehaviour
{
    void Awake()
    {
        //　アイテムの全情報を作成
        itemDataList.Add(new ItemData(Resources.Load("None", typeof(Sprite)) as Sprite, "None", "素手", 0, "武器"));
        itemDataList.Add(new ItemData(Resources.Load("Images/kenn", typeof(Sprite)) as Sprite, "BroadSword", "よくある剣", 5,"武器"));
        itemDataList.Add(new ItemData(Resources.Load("spear", typeof(Sprite)) as Sprite, "LongSpear", "超長い槍", 4,"武器"));
        itemDataList.Add(new ItemData(Resources.Load("Ax", typeof(Sprite)) as Sprite, "LegendAx", "伝説の斧", 7,"武器"));
        itemDataList.Add(new ItemData(Resources.Load("Images/yumi", typeof(Sprite)) as Sprite, "LongBow", "木の弓", 3,"武器"));

        NameList = new List<string>(itemDictionary.Keys); //個数表の初期化

    }

    public Dictionary<string, int> itemDictionary = new Dictionary<string, int>() {
  {"BroadSword", 0}, {"LongSpear", 0} ,{"LegendAx", 0},{"LongBow",0}
};
    public List<string> NameList;          //アイテムの個数表

    public ItemData CurrentWeaponState ; //装備中の武器
    public bool Shield { get; set; } = false;
    public int PLevel { get; set; } = 1;            //レベル
    [SerializeField]
    public int PMaxHP { get; set; } = 10;           //最大HP
    [SerializeField]
    public int PNowHP { get; set; } = 10;           //現在のHP
    public int PAtk { get; set; } = 10;             //攻撃力
    public int PDef { get; set; } = 10;　　　　　　 //防御力　

    private int PEXP_rui = 0;                       //現在の経験値
    public int PEXP_nextrui { get; set; } = 10;     //次のレベルまで

    float hungry;
    public int Pdirection_x { get; set; }
    public int Pdirection_y { get; set; }

    //現在の経験値の取得と読み取り
    public int Experience
    {
        get { return PEXP_rui; }
        set
        {
            PEXP_rui += value;
            if (PEXP_rui >= PEXP_nextrui)
            {
                PEXP_rui -= PEXP_nextrui;
                PLevel += 1;
                PEXP_nextrui = Mathf.RoundToInt(PEXP_nextrui * 1.2f);
                PMaxHP = Mathf.RoundToInt(PMaxHP * 1.2f);
                PAtk = Mathf.RoundToInt(PAtk * 1.2f);
                PDef = Mathf.RoundToInt(PDef * 1.2f);

            }
        }
    }
    //　アイテムを持っているかどうかのフラグ
    public Dictionary<string, bool> itemFlags = new Dictionary<string, bool>();
    //　アイテムデータのリスト
    public List<ItemData> itemDataList = new List<ItemData>();



    //　全アイテムデータを返す
    public List<ItemData> GetItemDataList()
    {

        return itemDataList;
    }
    //　個々のアイテムデータを返す
    public ItemData GetItemData(string itemName)
    {
        foreach (var item in itemDataList)
        {
            if (item.GetItemName() == itemName)
            {
                return item;
            }
        }
        return null;
    }

    //ここからアイテム管理導入

    private void Start()
    {
        DontDestroyOnLoad(this);
        //　とりあえず全てのアイテムのフラグを作成
        foreach (var item in GetItemDataList())
        {
            itemFlags.Add(item.GetItemName(), false);
        }
        //　とりあえずアイテムを持っていることにしない
        itemFlags["BroadSword"] = false;
        itemFlags["LongSpear"] = false;
        itemFlags["LegendAx"] = false;
        itemFlags["LongBow"] = false;

        CurrentWeaponState = itemDataList[0];
    }

    //　アイテムを所持しているかどうか
    public bool GetItemFlag(string itemName)
    {
        return itemFlags[itemName];
    }

    //ここまでアイテム管理

    //ステータス上昇・下降
    public void PAtkUp() //装備
    {
        PAtk = 10;
        //装備の攻撃力分プレイヤーの攻撃力を上昇
        PAtk =CurrentWeaponState.GetItemPower() + PAtk;
    }
    public void PNowHPUP(int itemHP) 
    {
        PNowHP += itemHP;
    }

}
