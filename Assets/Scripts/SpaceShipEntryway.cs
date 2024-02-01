using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SpaceShipEntryway : MonoBehaviour
{
    AudioSource assss;

    private void Start() {
        assss = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other) {
        if( other.CompareTag("Player")){
            assss.Play();
            FindObjectOfType<Outro>().ShipEntered();
        }
    }
}
