using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
// using UnityEditor.EditorTools;

public class Outro: MonoBehaviour
{
    public TextMeshProUGUI noteText;
    public Animator doorAnim;
    public Animator boxAnim;
    private bool playerEnteredFlag = false;
    public AudioSource selectionNoise;

    public SceneLoader sceneLoader;
    [Tooltip("The scene to be loaded when the pants are selected")]
    public int PanstSelectedScene;

    [Tooltip("The scene to be loaded when the lens is selected and the thing is found")]
    public int LensSelectedScene;
    public GameObject pantsGO;
    public GameObject lensGO;
    public Animator ScreenDimmer;
    private bool clicked = false;

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

    internal void PantsClicked()
    {
        if(!clicked){
        //play sound
        selectionNoise.Play();
        //dim screen
        ScreenDimmer.SetTrigger("dim");
        //send to bad ending in 2 seconds
        boxAnim.SetTrigger("pants");
        sceneLoader.Load(PanstSelectedScene, 6.0f);
        //pantsGO.SetActive(false);
        clicked = true;
        lensGO.SetActive(false);
        //Destroy(GameObject.FindGameObjectWithTag("Player"));
        IM.current.SetMouseLock(false);
        FindObjectOfType<PlayerInteraction>().Imobilize();
        }
    }

    internal void LensSelected()
    {
        //play sound
        selectionNoise.Play();
        clicked = true;
        //send to bad ending in 2 seconds
        pantsGO.SetActive(false);
        lensGO.SetActive(false);
    }

    public void ShipEntered(){
        ScreenDimmer.SetTrigger("dim");
        sceneLoader.Load(LensSelectedScene, 4.0f);
        IM.current.SetMouseLock(false);
        FindObjectOfType<PlayerInteraction>().Imobilize();
    }


}