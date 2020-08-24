using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerPurameter : MonoBehaviour
{
    void Awake()
    {
        //　アイテムの全情報を作成
        itemDataList.Add(new ItemData(Resources.Load("None", typeof(Sprite)) as Sprite, "None", "素手", 0, "武器"));
        itemDataList.Add(new ItemData(Resources.Load("Images/kenn", typeof(Sprite)) as Sprite, "MysteriousSword", "不思議な剣", 5,"武器"));
        itemDataList.Add(new ItemData(Resources.Load("Images/ono", typeof(Sprite)) as Sprite, "MysteriousAx", "不思議な斧", 4,"武器"));
        itemDataList.Add(new ItemData(Resources.Load("Images/yari", typeof(Sprite)) as Sprite, "MysteriousSpear", "不思議な槍", 7,"武器"));
        itemDataList.Add(new ItemData(Resources.Load("Images/tate", typeof(Sprite)) as Sprite, "MysteriousShield", ";不思議な盾", 3,"盾"));
        itemDataList.Add(new ItemData(Resources.Load("Images/ya", typeof(Sprite)) as Sprite, "Arrow", "矢", 3, "矢"));
        itemDataList.Add(new ItemData(Resources.Load("Images/heel", typeof(Sprite)) as Sprite, "HeelPotion", "ヒールポーション", 30, "HP消費"));
        itemDataList.Add(new ItemData(Resources.Load("Images/Exheel", typeof(Sprite)) as Sprite, "Ex_HeelPotion", "エクスヒールポーション", 90, "HP消費"));
        itemDataList.Add(new ItemData(Resources.Load("Images/Fullheel", typeof(Sprite)) as Sprite, "Full_HeelPotion", "フルヒールポーション", 10000, "HP消費"));
        itemDataList.Add(new ItemData(Resources.Load("Images/power", typeof(Sprite)) as Sprite, "PowerPotion", "パワーポーション", 10, "PA消費"));
        itemDataList.Add(new ItemData(Resources.Load("Images/defence", typeof(Sprite)) as Sprite, "DefencePotion", "ディフェンスポーション", 10, "PD消費"));
        itemDataList.Add(new ItemData(Resources.Load("Images/lucky", typeof(Sprite)) as Sprite, "LuckyPotion", "ラッキーポーション", 0, "LC消費"));
        itemDataList.Add(new ItemData(Resources.Load("Images/poison", typeof(Sprite)) as Sprite, "PoisonPotion", "ポイズンポーション", 5, "PO消費"));
        itemDataList.Add(new ItemData(Resources.Load("Images/dispoison", typeof(Sprite)) as Sprite, "DispoisonPotion", "ディスポイズンポーション", 0, "PD消費"));
        itemDataList.Add(new ItemData(Resources.Load("Images/paralyze", typeof(Sprite)) as Sprite, "ParalyzePotion", "パラライズポーション", 4, "PR消費"));
        itemDataList.Add(new ItemData(Resources.Load("Images/wind", typeof(Sprite)) as Sprite, "WindPotion", "ウィンドポーション", 3, "WD消費"));
        itemDataList.Add(new ItemData(Resources.Load("Images/stome", typeof(Sprite)) as Sprite, "StomePotion", "ストームポーション", 8, "ST消費"));
        itemDataList.Add(new ItemData(Resources.Load("Images/anger", typeof(Sprite)) as Sprite, "AngerPotion", "アンガーポーション", 2, "AG消費"));
        itemDataList.Add(new ItemData(Resources.Load("Images/throw", typeof(Sprite)) as Sprite, "ThrowPotion", "スローポーション", 2, "SP消費"));
        itemDataList.Add(new ItemData(Resources.Load("Images/quick", typeof(Sprite)) as Sprite, "QuickPotion", "クイックポーション", 2, "SP消費"));

        NameList = new List<string>(itemDictionary.Keys); //個数表の初期化

    }
    //個数を一つ一つ初期化
    public Dictionary<string, int> itemDictionary = new Dictionary<string, int>() {
  {"MysteriousSword", 0}, {"MysteriousAx", 0} ,{"MysteriousSpear", 0},{"MysteriousShield",0},{"Arrow",0},
        {"HeelPotion",0},{"Ex_HeelPotion",0},{"Full_HeelPotion",0},{"PowerPotion",0},{"DefencePotion",0},
        {"LuckyPotion",0},{"PoisonPotion",0},{"DispoisonPotion",0},{"ParalyzePotion",0},
        {"WindPotion",0},{"StomePotion",0},{"AngerPotion",0},{"ThrowPotion",0},{"QuickPotion",0}
};
    public List<string> NameList;          //アイテムの個数表

    public ItemData CurrentWeaponState ; //装備中の武器
    public ItemData CurrentshieldState; //装備中の盾
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
        foreach (var item in GetItemDataList())
        {
            itemFlags[item.GetItemName()] = false;
        }

        CurrentWeaponState = itemDataList[0];
        CurrentshieldState = itemDataList[0];
    }

    //　アイテムを所持しているかどうか
    public bool GetItemFlag(string itemName)
    {
        return itemFlags[itemName];
    }

    //ここまでアイテム管理

    //ステータス上昇・下降
    public void PAtkUp() //攻撃力上昇
    {
        PAtk = 10;
        //装備の攻撃力分プレイヤーの攻撃力を上昇
        PAtk =CurrentWeaponState.GetItemPower() + PAtk;
    }
    public void PDefUp() //防御力上昇
    {
        PDef = 10;
        //装備の攻撃力分プレイヤーの攻撃力を上昇
        PDef = CurrentshieldState.GetItemPower() + PDef;
    }
    public void PNowHPUP(int itemHP) 
    {
        PNowHP += itemHP;
    }

}
