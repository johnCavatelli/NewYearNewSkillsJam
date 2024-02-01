using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LensSelect : Interactable
{

    public GameObject lens;
    public override void Interact()
    {
        FindObjectOfType<Outro>().LensSelected();
        lens.SetActive(true);
    }
}
