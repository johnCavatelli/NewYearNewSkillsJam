using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string tipText;
    public virtual void Interact() {
        print("ERROR, CALLING INTERACT FROM INTERACTABLE CLASS");
    }

    public string getTipText()
    {
        return tipText;
    }

    public void setTipText(string s)
    {
        tipText = s;
    }
}
