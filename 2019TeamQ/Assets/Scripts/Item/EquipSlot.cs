using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class EquipSlot : MonoBehaviour

{
    GameObject EquipText; //equipテキストを取得
  public ItemData Itemdata;//クリックされたアイテムのデータ用変数


    PlayerPurameter codeB; //PlayerPuramaterスクリプトを取得

    int i;//武器装備状態変数　0で非装備、1で装備
    int t;//盾装備状態変数　0で非装備、1で装備
   public int b;//botton表示状態変数　0で非表示、1で表示

    PointerEventData pointer;
    GameObject targetGameObject;//ray用変数

   public GameObject clickedGameObject;//アイテム用変数

    Text Quantity_text;//アイテム数テキスト

    void Start()
    {
        codeB = GetComponent<PlayerPurameter>();//PlayerPurameterを取得
        i = 0;　//非装備状態
        t = 0;  //非装備状態
        b = 0; //botton非表示
        pointer = new PointerEventData(EventSystem.current);

        targetGameObject = null;
    }

    void Update()
    {
        codeB.PAtkUp();
        codeB.PDefUp();
        if (Input.GetMouseButtonDown(0))
        {


            List<RaycastResult> results = new List<RaycastResult>();
            pointer.position = Input.mousePosition;
            EventSystem.current.RaycastAll(pointer, results);



            foreach (RaycastResult target in results)
            {
                targetGameObject = target.gameObject;

                if (targetGameObject.tag == "itemslot")
                {
                    if (b == 0)
                    {
                        clickedGameObject = target.gameObject;
                        EquipText = clickedGameObject.transform.GetChild(1).gameObject; //クリックされたスロットのEテキストを指定
                        Itemdata = clickedGameObject.GetComponent<ProcessingSlot>().myItemData; //クリックされたスロットのアイテム情報を代入
                        clickedGameObject.transform.GetChild(3).gameObject.SetActive(true);
                        b++;
                    }
                    else if(b == 1){
                        clickedGameObject.transform.GetChild(3).gameObject.SetActive(false);
                        clickedGameObject = target.gameObject;
                        EquipText = clickedGameObject.transform.GetChild(1).gameObject; //クリックされたスロットのEテキストを指定
                        Itemdata = clickedGameObject.GetComponent<ProcessingSlot>().myItemData; //クリックされたスロットのアイテム情報を代入
                        clickedGameObject.transform.GetChild(3).gameObject.SetActive(true);
                    }

                }
            }

        }
        Debug.Log("攻撃力"+codeB.PAtk);
        Debug.Log("体力" + codeB.PNowHP);
        Debug.Log("防御力" + codeB.PDef);


    }

    //public void Onclick()
    //{
    //    if (Itemdata.GetItemType() == "武器")
    //    { //武器の場合
    //        soubi();
    //    }
    //    else if (Itemdata.GetItemType() == "盾")
    //    {//盾の場合
    //        tate();
    //    }
    //    else if (Itemdata.GetItemType() == "HP消費") //消費アイテムの場合
    //    {
    //        syouhi();
    //    }
    //    b--;
    //    clickedGameObject.transform.GetChild(3).gameObject.SetActive(false);
    //}

    public void soubi()
    {

        if (i == 1 && EquipText.activeSelf == true)//currentweaponにスロットにあるアイテムが装備してある場合
        {
            codeB.CurrentWeaponState = codeB.itemDataList[0];//currentWeaponStateを素手に、装備を外す
            i--;
        }
        else if (i == 0 && EquipText.activeSelf == false)//currentweaponにスロットにあるアイテムが装備していない場合

        {
            codeB.CurrentWeaponState = Itemdata; //currentweaponにスロットにあるアイテムを装備する
            i++;

        }
        else if (i == 1 && EquipText.activeSelf == false)//currentweaponにスロットにあるアイテムが装備していない場合

        {
            codeB.CurrentWeaponState = codeB.itemDataList[0];
            codeB.CurrentWeaponState = Itemdata; //currentweaponにスロットにあるアイテムを装備する

        }

    }

    public void tate()
    {

        if (t == 1 && EquipText.activeSelf == true)//CurrentshieldStateにスロットにあるアイテムが装備してある場合
        {
            codeB.CurrentshieldState = codeB.itemDataList[0];//CurrentshieldStateを素手に、装備を外す
            t--;
        }
        else if (t == 0 && EquipText.activeSelf == false)//CurrentshieldStateにスロットにあるアイテムが装備していない場合

        {
            codeB.CurrentshieldState = Itemdata; //CurrentshieldStateにスロットにあるアイテムを装備する
            t++;

        }
        else if (t == 1 && EquipText.activeSelf == false)//CurrentshieldStateにスロットにあるアイテムが装備していない場合

        {
            codeB.CurrentshieldState = codeB.itemDataList[0];
            codeB.CurrentshieldState = Itemdata; //CurrentshieldStateにスロットにあるアイテムを装備する

        }

    }

    public void syouhi()
    {
        if (codeB.itemDictionary[Itemdata.GetItemName()]>1)//個数が2つ以上の場合
        {
            codeB.itemDictionary[Itemdata.GetItemName()] -= 1;//個数-1

            //アイテム数テキストの数字を1減らす
            Quantity_text= clickedGameObject.transform.GetChild(2).gameObject.GetComponent<Text>();
            string itemkazu = Quantity_text.text;
            int a = int.Parse(itemkazu);
            a--;
            Quantity_text.text = "" +a;
            //ここまで
        }
        else if (codeB.itemDictionary[Itemdata.GetItemName()] == 1) {//個数が1つの場合

            codeB.itemDictionary[Itemdata.GetItemName()] -= 1;//個数-1
            codeB.itemFlags[Itemdata.GetItemName()] = false;//アイテムを所持していないことにする
            clickedGameObject.SetActive(false);//アイテムスロットを非表示にする

            //アイテム数テキストの数字を1減らす
            Quantity_text = clickedGameObject.transform.GetChild(2).gameObject.GetComponent<Text>();
            string itemkazu = Quantity_text.text;
            int a = int.Parse(itemkazu);
            a--;
            Quantity_text.text = "" + a;
            //ここまで
        }
        codeB.PNowHPUP(Itemdata.GetItemPower());//hp回復

    }

}

