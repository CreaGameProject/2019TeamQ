using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

//インベントリのアイテムスロットを作成するスクリプト
public class CreateSlotScript : MonoBehaviour
{
    string itemname;
    int itemQuantity;
    Text Quantity_text;


    //　アイテム情報のスロットプレハブ
    [SerializeField]
    private GameObject slot;
    //　主人公のステータス
    [SerializeField]
    private PlayerPurameter myStatus;




    //　インベントリの表示が呼び出された時
    void OnEnable()
    {
        //　アイテムデータベースに登録されているアイテム用のスロットを全作成
        CreateSlot(myStatus.GetItemDataList());
    }

    //　アイテムスロットの作成
    public void CreateSlot(List<ItemData> itemList)
    {

        int i = 0;

        foreach (var item in itemList)
        {
            //全アイテムを探索し、アイテムを所持している(アイテムフラグが1)ならば、そのアイテムのスロットを作成する。
            if (myStatus.GetItemFlag(item.GetItemName()))
            {
                //　スロットのインスタンス化
                var instanceSlot = Instantiate<GameObject>(slot, transform);
                //　スロットゲームオブジェクトの名前を設定
                instanceSlot.name = "ItemSlot" + i++;
                //　Scaleを設定しないと0になるので設定
                instanceSlot.transform.localScale = new Vector3(1f, 1f, 1f);
                //　アイテム情報をスロットのProcessingSlotに設定する
                instanceSlot.GetComponent<ProcessingSlot>().SetItemData(item);
                //アイテムの個数をテキストに設定し、表示する
                itemname = item.GetItemName();
                Debug.Log(itemname);
                Quantity_text = instanceSlot.transform.GetChild(2).gameObject.GetComponent<Text>();
                itemQuantity = myStatus.itemDictionary[itemname];
                Quantity_text.text = "" + itemQuantity;
            }
        }
    }
}

