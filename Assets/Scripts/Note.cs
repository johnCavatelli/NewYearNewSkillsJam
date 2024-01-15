using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : Interactable
{
    public GameObject model;
    public GameObject text;
    public BoxCollider bc;
    private bool isNoteActive;
    public AudioSource doorAudio;


    private void Start() {
        isNoteActive = false;
        model.SetActive(true);
        text.SetActive(false); 
        bc.enabled = true;
    }

    public void PickupNote(){
        Invoke("canClose",0.5f);
        model.SetActive(false);
        text.SetActive(true);
        bc.enabled = false;
        gameObject.GetComponent<AudioSource>().Play();
    }

    private void canClose(){
        isNoteActive = true;
    }
    public override void Interact()
    {
        // print("Interacting with note " + isNoteActive);
        if(!isNoteActive){
            PickupNote();
        }
    }

    private void Update() {
        if(isNoteActive && (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))){
            isNoteActive = false;
            text.SetActive(false);
            Destroy(gameObject);
            LevelManager.current.OpenDoorStartLevel();
            doorAudio.Play();
        }
    }
}
