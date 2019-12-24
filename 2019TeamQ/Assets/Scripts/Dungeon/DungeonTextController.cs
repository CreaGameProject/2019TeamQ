using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonTextController : MonoBehaviour
{
    public GameObject textpanel;
    public Text messagetext;
    public Text logtext;
    public List<string> MessageText = new List<string>();
    public List<string> LogText = new List<string>();

    void Start()
    {
     
    }

    public void ShowMessage(string message)
    {
        textpanel.SetActive(true);
        messagetext.text = "";
        MessageText.Add(message);
        LogText.Add(message);
        if (LogText.Count > 30)
        {
            LogText.RemoveAt(0);
        }
        if (MessageText.Count > 3)
        {
            MessageText.RemoveAt(0);
        }
        for(int i=0;i<MessageText.Count; i++)
        {
            messagetext.text += MessageText[i];
        }
        
    }
   
    public void ShowLog()
    {
        for(int i = 0; i < LogText.Count; i++)
        {
            logtext.text += LogText[i];
        }
    }
}
