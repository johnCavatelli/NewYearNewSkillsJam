using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchDevice : Device
{
    public void SwitchState()
    {
        isOn = !isOn;
        // print("Switching state in the switch to " + isOn);
        DeviceManager.current.UpdateAllDevices();
    }

    public override void UpdateState(bool newState)
    {
        bool oldState = isOn;
        base.UpdateState(newState);
        if (newState != oldState)
        {
            gameObject.GetComponent<Switch>().SwitchSwitchBecauseItUpdatedElsewhere();
        }
    }
}
