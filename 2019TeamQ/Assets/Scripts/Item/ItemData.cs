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

    public ItemData(Sprite image, string itemName, string information)
    {
        this.itemSprite = image;
        this.itemName = itemName;
        this.itemInformation = information;
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
}

