using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    public KeyCode resetKey;
    public Transform player;

    private Vector3 startPos;
    void Start(){
        startPos = player.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(Input.GetKeyDown(resetKey)){
            print("reset");
            player.position = startPos;
        }
    }
}
