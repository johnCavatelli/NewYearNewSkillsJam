using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entryway : MonoBehaviour
{
    private bool playerEnteredFlag = false;
    private void OnTriggerEnter(Collider other) {
        if(!playerEnteredFlag && other.CompareTag("Player")){
            LevelManager.current.CloseDoor();
            gameObject.GetComponent<AudioSource>().Play();
            playerEnteredFlag = true;
        }
    }
}
