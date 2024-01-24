using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager current;
    public SceneLoader sceneLoader;
    public Animator doorAnimator;
    public Money money;
    public GameObject gotMoneyText;

    [Header("Loose State Options")]
    public int switchFlips = -1;//how many flips of switches are allowed in the level, the loose state is when it reaches 0 and the house explodes
    public Camera LooseCamera;
    public Camera FPCamera;
    public GameObject explosionParticleAndNoise;
    public GameObject restartLevelButton;
    public Text countdownText;


    private void Awake()
    {
        current = this;
        countdownText.text = switchFlips.ToString();
    }

    public void CompleteLevel()
    {
        doorAnimator.SetTrigger("open");
        gameObject.GetComponent<AudioSource>().Play();
        money.EnableMoney();

    }

    public void CloseDoor(){
        doorAnimator.SetTrigger("close");
    }

    internal void MoneyPickedUp()
    {
        sceneLoader.Load(2.01f);
        gotMoneyText.SetActive(true);
    }

    internal void OpenDoorStartLevel()
    {
        doorAnimator.SetTrigger("open");
    }

    public void SwitchFliped(){
        switchFlips--;
        countdownText.text = switchFlips.ToString();
        if(switchFlips ==0){
            LooseGame();
        }
    }

    private void LooseGame(){
        explosionParticleAndNoise.SetActive(true);
        FPCamera.enabled = false;
        LooseCamera.enabled=true;
        restartLevelButton.SetActive(true);
        IM.current.SetMouseLock(false);
        Destroy(GameObject.FindGameObjectWithTag("Player"));
    }
}
