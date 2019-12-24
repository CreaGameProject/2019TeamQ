using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpContrioller : MonoBehaviour
{
    private PlayerPurameter playerpurameter;
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        playerpurameter = GameObject.Find("GameManager").GetComponent<PlayerPurameter>();
        this.slider = GameObject.Find("HPbar").GetComponent<Slider>();
    }
    
    // Update is called once per frame
    void Update()
    {
        slider.value = (float)playerpurameter.PNowHP / (float)playerpurameter.PMaxHP;     
    }
}

