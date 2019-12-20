using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class EquipSlot : MonoBehaviour

{
    public GameObject Equip_Text;
    public void OnSortiePointClick(BaseEventData data)
    {
        var eventData = (PointerEventData)data;
        //処理...
        if (Equip_Text.activeSelf == true)
        {

            Equip_Text.SetActive(false);
        }else if (Equip_Text.activeSelf == false)
        {

            Equip_Text.SetActive(true);
        }
    }
    
    
}
