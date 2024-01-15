using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Gate", order = 1)]
public class Gate : ScriptableObject{
    public int device;
    public gateTypes type;
}