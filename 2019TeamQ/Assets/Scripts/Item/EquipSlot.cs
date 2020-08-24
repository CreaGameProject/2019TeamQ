using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//ボタン(装備、投げる、捨てるの各種ボタン)表示と各種アイテムを使用した時の動作内容を記述したスクリプト
public class EquipSlot : MonoBehaviour

{
    GameObject EquipText; //equipテキストを取得
 
    public ItemData Itemdata;//クリックされたアイテムのデータ用変数


    PlayerPurameter codeB; //PlayerPuramaterスクリプトが入る変数

    int i;//武器装備状態変数　0で非装備、1で装備
    int t;//盾装備状態変数　0で非装備、1で装備
   public int b;//botton（装備、投げる、捨てるの各種ボタン）表示状態変数　0で非表示、1で表示

    PointerEventData pointer;
    GameObject targetGameObject;//ray用変数

   public GameObject clickedGameObject;//アイテム用変数

    Text Quantity_text;//アイテム数テキスト

    void Start()
    {
        codeB = GetComponent<PlayerPurameter>();//PlayerPurameter(以下、codeB)を取得。
        i = 0;　//武器の非装備状態
        t = 0;  //盾の非装備状態
        b = 0; //botton非表示
        pointer = new PointerEventData(EventSystem.current);

        targetGameObject = null;
    }

    void Update()
    {
        codeB.PAtkUp();//毎回主人公のステータスを更新。もうちょっとうまいやり方ありそうだけどわからないです...
        codeB.PDefUp();
        if (Input.GetMouseButtonDown(0))
        {


            List<RaycastResult> results = new List<RaycastResult>();
            pointer.position = Input.mousePosition;
            EventSystem.current.RaycastAll(pointer, results);


            //ボタン(装備、投げる、捨てるの各種ボタン)表示の管理。
            foreach (RaycastResult target in results)
            {
                targetGameObject = target.gameObject;
                //選択したオブジェクトがアイテムスロットの場合
                if (targetGameObject.tag == "itemslot")
                {
                    //ボタンが表示されていない場合。この時、選択したアイテムスロットのボタンだけではなくて、その前に他のアイテムスロットを選択してボタンを表示させたまま、
                    //このアイテムスロットを選択していないことも条件。表示させたままであれば下のif文。
                    if (b == 0)
                    {
                        clickedGameObject = target.gameObject;//選択したアイテムスロットを格納。
                        EquipText = clickedGameObject.transform.GetChild(1).gameObject; //選択したアイテムスロットのEテキスト(装備しているかを表すテキスト)を変数に代入。//下の装備してるかしてないかの判断で使います。
                        Itemdata = clickedGameObject.GetComponent<ProcessingSlot>().myItemData; //選択したアイテムスロットのアイテム情報を格納
                        clickedGameObject.transform.GetChild(3).gameObject.SetActive(true);//選択したアイテムスロットのボタンを表示する
                        b = 1;//ボタンの表示状態を表示しているに変更
                    }
                    //インベントリ全体で、どれかのアイテムスロットのボタンが表示されている場合。
                    else if(b == 1){
                        clickedGameObject.transform.GetChild(3).gameObject.SetActive(false);//前に表示させていたボタンを非表示にする。
                        b = 0;//ボタンの表示状態を表示していないに変更
                        if (clickedGameObject != target.gameObject)//もし、選択したアイテムが前に選択したアイテムと同じでないならば、(同じならばボタンを消す。つまり、間違えてボタンを表示させてしまった場合、もう一度選択することで消せる)
                        {

                            clickedGameObject = target.gameObject;//選択したアイテムスロットを格納。
                            EquipText = clickedGameObject.transform.GetChild(1).gameObject;  //選択したアイテムスロットのEテキスト(装備しているかを表すテキスト)を指定
                            Itemdata = clickedGameObject.GetComponent<ProcessingSlot>().myItemData;  //選択したアイテムスロットのアイテム情報を格納
                            clickedGameObject.transform.GetChild(3).gameObject.SetActive(true);//選択したアイテムスロットのボタンを表示する
                            b = 1;//ボタンの表示状態を表示しているに変更
                        }
                    }

                }
            }

        }
        //Debug.Log("攻撃力"+codeB.PAtk);
        //Debug.Log("体力" + codeB.PNowHP);
        //Debug.Log("防御力" + codeB.PDef);


    }
    //装備ボタンを追加したので、EquipButtonに移動させました。
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


　　//以下各種アイテムを使用した時の動作内容

    //インベントリから武器を使用した時の動作内容
    public void soubi()
    {
        //選択したアイテムが既に装備してある場合。
        if (i == 1 && Itemdata == codeB.CurrentWeaponState)
        {
            codeB.CurrentWeaponState = codeB.itemDataList[0];//currentWeaponState(現在の装備)を素手にして、装備を外す
            i=0;//装備状況の変更。武器を所持していない状態
        }
        else if (i == 0 && Itemdata != codeB.CurrentWeaponState)//選択したアイテムがまだ現在装備していない場合

        {
            codeB.CurrentWeaponState = Itemdata; //currentweaponに選択したアイテムを装備する
            i=1;//装備状況の変更。武器を所持している状態

        }
        else if (i == 1 && Itemdata != codeB.CurrentWeaponState)//選択したアイテムがまだ現在装備していないが、他の武器を装備している場合

        {
            codeB.CurrentWeaponState = codeB.itemDataList[0];//一度装備を素手にする。
            codeB.CurrentWeaponState = Itemdata; //currentweaponに選択したアイテムを装備する

        }

    }
    //インベントリから盾を使用した時の動作内容。CurrentWeaponがCurrentsheildになっただけで、武器と処理内容は全く同じ。
    public void tate()
    {

        if (t == 1 && Itemdata == codeB.CurrentshieldState)
        {
            codeB.CurrentshieldState = codeB.itemDataList[0];
            t--;
        }
        else if (t == 0 && Itemdata != codeB.CurrentshieldState)

        {
            codeB.CurrentshieldState = Itemdata;
            t++;

        }
        else if (t == 1 && Itemdata != codeB.CurrentshieldState)

        {
            codeB.CurrentshieldState = codeB.itemDataList[0];
            codeB.CurrentshieldState = Itemdata;

        }

    }

    //インベントリから消費アイテム(HP回復ポーションのみ)を使用した時の動作内容
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
    //ほかの消費アイテムの動作はこの続きに...

}

