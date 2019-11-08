using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPurameter : MonoBehaviour
{
    public int Plevel { get; set; } = 1;            //レベル
    public int PMaxHP { get; set; } = 10;           //最大HP
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
                Plevel += 1;
                PEXP_nextrui = Mathf.RoundToInt(PEXP_nextrui * 1.2f);
                PMaxHP = Mathf.RoundToInt(PMaxHP * 1.2f);
                PAtk = Mathf.RoundToInt(PAtk * 1.2f);
                PDef = Mathf.RoundToInt(PDef * 1.2f);

            }
        }
    }
}
