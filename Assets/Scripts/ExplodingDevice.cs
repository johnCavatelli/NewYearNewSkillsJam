using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class ExplodingDevice : Device
{
    public GameObject bulb;
    public AudioSource deviceChangedStateSound;
    [Tooltip("The state the light will explode on")]
    public bool looseState;

    public override void UpdateState(bool newState)
    {
        base.UpdateState(newState);
        bulb.SetActive(newState);

        if(newState == looseState){
            deviceChangedStateSound.Play();
            LevelManager.current.DeviceExploded(2f);
        }
    }
}
