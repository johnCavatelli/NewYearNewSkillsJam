using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeviceManager : MonoBehaviour
{
    public static DeviceManager current;
    public GameObject[] devices;
    public List<int> lights;//list of lights because need to check when all are off for wincon

    private void Awake()
    {
        current = this;
    }


    private void Start()
    {
        Invoke("UpdateAllDevices",0.1f);
    }

    public void UpdateAllDevices()
    {
        bool wincon = true;
        for (int i = 0; i < devices.Length; i++)
        {
            bool newSt = UpdateDevice(i);
            // print("Updating device " + i + " which is " + devices[i].name + " with new state " + newSt);
            devices[i].GetComponent<Device>().UpdateState(newSt);
            if (lights.Contains(i))
            {
                if (wincon && newSt == true)//if we expect to win and the light is on then our wincon is false (bc we want lights to be off to win)
                {
                    wincon = false;
                }
            }
        }
        if (wincon) { 
            LevelManager.current.CompleteLevel(); 
            print("all lights were off so level is complete");
        }
    }

    public bool UpdateDevice(int id)
    {
        Gate[] conditions = devices[id].GetComponent<Device>().GetConditions();
        bool curr = false;
        for (int i = 0; i < conditions.Length; i++)
        {
            // print(conditions[i]);
            switch (conditions[i].type)
            {
                case gateTypes.TRUE:
                    curr = true;
                    break;
                case gateTypes.FALSE:
                    curr = false;
                    break;
                case gateTypes.NOP:
                    curr = GetDeviceState(id);
                    // print("Hitting nop for device " + id + " with eval " + curr);
                    break;
                case gateTypes.NOT:
                    curr = !curr;
                    break;
                case gateTypes.AND:
                    curr = curr && GetDeviceState(conditions[i].device);
                    break;
                case gateTypes.OR:
                    curr = curr || GetDeviceState(conditions[i].device);
                    break;
                case gateTypes.GET:
                    curr = GetDeviceState(conditions[i].device);
                    break;
                default:
                    break;
            }
        }
        return curr;
    }

    private bool GetDeviceState(int deviceId)
    {
        if (deviceId == -1)
        {
            throw new SystemException("Trying to read device id -1");
        }
        GameObject obj = devices[deviceId];
        if (obj)
        {
            return obj.GetComponent<Device>().GetDeviceState();
        }
        else
        {
            throw new System.Exception("NOO, idk how you even got here but there's no device with that ID: " + deviceId);
        }
    }
}
