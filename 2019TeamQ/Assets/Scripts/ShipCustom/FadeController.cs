using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    float fadeSpeed = 0.1f;
    float red, green, blue, alfa;

    Image fadeImage;
    // Start is called before the first frame update
    void Start()
    {
        fadeImage = GetComponent<Image>();
        red = fadeImage.color.r;
        green = fadeImage.color.g;
        blue = fadeImage.color.b;
        alfa = fadeImage.color.a;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator StartFadeOut()
    {
        fadeImage.enabled = true;
        while(alfa < 1)
        {
            alfa += fadeSpeed;
            SetAlfa();
            yield return null;
        }
    }

    public IEnumerator StartFadeIn()
    {
        while(alfa > 0)
        {
            alfa -= fadeSpeed;
            SetAlfa();
            yield return null;
        }
        fadeImage.enabled = false;
    }

    void SetAlfa()
    {
        fadeImage.color = new Color(red, green, blue, alfa);
    }
}
