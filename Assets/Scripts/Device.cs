using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



/*
explination of device system:

This is the device class, a device can be either on or off. It has a list of conditions (made of logic gates)
that determine if it's off or on. A device manager calls upon this list every time a device is updated to set its state.
Something like a switch uses a NoOp command because its state doesn't depend on anything else. Something like a lightbulb that is only
dependent on a switch might just use a Get command. A lightbulb that is dependend on the AND of 2 switches would use a GET on switch1 as 
well as AND switch2 to complete this operation. The conditions are evaluated in order. So !(A or B) would be GET A, OR B, NOT. 


*/
public class Device : MonoBehaviour
{
    protected bool isOn;
    public bool initialState;
    public int deviceId;

    [SerializeField] private Gate[] conditions;


    protected void Start() {
        isOn = initialState;
    }

    public virtual void UpdateState(bool newState){
        isOn = newState;
    }
    
    public bool GetDeviceState(){
        return isOn;
    }

    public Gate[] GetConditions(){
        return conditions;
    }


}

public enum gateTypes {
    AND,
    OR,
    NOT,
    TRUE,
    FALSE,
    NOP,
    GET
}