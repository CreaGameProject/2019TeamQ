using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//インベントリの表示・非表示のスクリプト
public class PauseScript : MonoBehaviour
{
    EquipSlot codeES; //EquipSlotスクリプトの変数

    //　ポーズした時に表示するUI
    [SerializeField]
    private GameObject pauseUI;

    void Start()
    {
        codeES = GetComponent<EquipSlot>();//EquipSlotを取得(以下codeES)
    }

    void Update()
    {
         //キーボードのQを押したとき
        if (Input.GetKeyDown("q"))
        {



            //　インベントリの表示・非表示を切り替え
            pauseUI.SetActive(!pauseUI.activeSelf);

            //　インベントリが表示されてる時は停止
            if (pauseUI.activeSelf)
            {
                Time.timeScale = 0f;

            }
            else
            {

                //　インベントリが表示されてなければtimeScale1(通常)
                Time.timeScale = 1f;

                codeES.b=0;//アイテムスロットのボタンの表示状態を非表示に設定

                //codeES.clickedGameObject.transform.GetChild(3).gameObject.SetActive(false);//多分いらないかも?
            }
        }
    }
}

