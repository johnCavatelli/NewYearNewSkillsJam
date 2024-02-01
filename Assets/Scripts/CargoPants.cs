using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoPants : Interactable
{

    public override void Interact()
    {
        FindObjectOfType<Outro>().PantsClicked();
    }
}
