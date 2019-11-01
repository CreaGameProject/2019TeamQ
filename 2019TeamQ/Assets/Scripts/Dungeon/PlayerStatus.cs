using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    int level;
    int MaxHP;
    int NowHP;
    int ATK;
    int DFE;
    float hungry;
    int EXP_rui;
    int EXP_nextrui;
    
    
    int floorlevel;
    
    //経験値を得たとき
    public void GetExperience(int exp)
    {
        EXP_rui += exp;
        if (EXP_rui >= EXP_nextrui)
        {
            EXP_rui -= EXP_nextrui;
            level += 1;
            EXP_nextrui = Mathf.RoundToInt(EXP_nextrui * 1.2f);
            MaxHP = Mathf.RoundToInt(MaxHP * 1.2f);
            ATK = Mathf.RoundToInt(ATK * 1.2f);
            DFE = Mathf.RoundToInt(DFE * 1.2f);

        }
    }
}
