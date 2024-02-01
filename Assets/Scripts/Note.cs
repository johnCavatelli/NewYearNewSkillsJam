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
    public GameObject setObjectActiveWithRead = null;


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
        if(setObjectActiveWithRead != null){
            setObjectActiveWithRead.SetActive(true);
        }
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
        if(isNoteActive && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Q))){
            isNoteActive = false;
            text.SetActive(false);
            Destroy(gameObject);
            LevelManager.current.OpenDoorStartLevel();
            doorAudio.Play();
        }
    }
}
