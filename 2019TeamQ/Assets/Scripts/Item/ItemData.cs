using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData
{

    //　アイテムのImage画像
    private Sprite itemSprite;
    //　アイテムの名前
    private string itemName;
    //　アイテムの情報
    private string itemInformation;
    //アイテムのパラメータ
    private int itemPower;
    //アイテムの種類
    private string itemType;


    public ItemData(Sprite image, string itemName, string information,int itemPower,string itemType)
    {
        this.itemSprite = image;
        this.itemName = itemName;
        this.itemInformation = information;
        this.itemPower = itemPower;
        this.itemType = itemType;

    }

    public Sprite GetItemSprite()
    {
        return itemSprite;
    }


    public string GetItemName()
    {
        
        return itemName;
        
    }

    public string GetItemInformation()
    {
        return itemInformation;
    }

    public int GetItemPower()
    {
        return itemPower;
    }

    public string GetItemType()
    {
        return itemType;
    }
}

