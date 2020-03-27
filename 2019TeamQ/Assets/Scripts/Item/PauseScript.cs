using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PauseScript : MonoBehaviour
{
    EquipSlot codeES; //EquipSlotスクリプトの変数

    //　ポーズした時に表示するUI
    [SerializeField]
    private GameObject pauseUI;

    void Start()
    {
        codeES = GetComponent<EquipSlot>();//EquipSlotを取得
    }

    void Update()
    {
        if (Input.GetKeyDown("q"))
        {



            //　ポーズUIのアクティブ、非アクティブを切り替え
            pauseUI.SetActive(!pauseUI.activeSelf);

            //　ポーズUIが表示されてる時は停止
            if (pauseUI.activeSelf)
            {
                Time.timeScale = 0f;
                //　ポーズUIが表示されてなければ通常通り進行
            }
            else
            {
                codeES.b=0;
                //codeES.clickedGameObject.transform.GetChild(3).gameObject.SetActive(false);
                Time.timeScale = 1f;
            }
        }
    }
}

