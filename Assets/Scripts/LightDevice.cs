using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class LightDevice : Device
{
    public GameObject bulb;

    public override void UpdateState(bool newState)
    {
        base.UpdateState(newState);
        bulb.SetActive(newState);
    }
}
