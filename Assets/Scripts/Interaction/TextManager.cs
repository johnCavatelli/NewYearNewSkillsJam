using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TextManager : MonoBehaviour
{
    public TextMeshProUGUI toolTipText;
    // public Animator animator;
    public static TextManager current;

    // private bool typing = false;
    private string currentTextBeingTyped;


    private void Awake()
    {
        current = this;
    }

    public void UpdateTipText(string v)
    {
        toolTipText.text = v;
    }

    public void ClearTipText()
    {
        if(toolTipText.text != null)
        {
            toolTipText.text = null;
        }
    }

    // public void DisplayMessage(string message)
    // {
    //     messageText.text = "";
    //     StopCoroutine("WriteMessage");

    //     StartCoroutine("WriteMessage", message);
    // }

    // IEnumerator WriteMessage(string message)
    // {
    //     messageText.text = message;
    //     yield return new WaitForSeconds(1.2f);
    //     while(messageText.text.Length > 0)
    //     {
    //         messageText.text = messageText.text.Remove(messageText.text.Length - 1);
    //         yield return new WaitForSeconds(0.04f);
    //     }
    // }
}
