using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Outro: MonoBehaviour
{
    public TextMeshProUGUI noteText;
    public Animator doorAnim;
    private bool playerEnteredFlag = false;

    private void Start() {
        noteText.text = System.Environment.UserName + ",\n I should not be writing this. Enclosed are 'YOUR' new pants. You can take these pants, and move on with your life. But the 'RIFT' calls you. You know it does. If you want to be 'TRUE' to yourself and the 'RIFT', take up your lens once more and join us. You can find our ship with your lens. The decision is yours.";
    }

    private void OnTriggerEnter(Collider other) {
        if(!playerEnteredFlag && other.CompareTag("Player")){
            doorAnim.SetTrigger("open");
            gameObject.GetComponent<AudioSource>().Play();
            playerEnteredFlag = true;
        }
    }




}