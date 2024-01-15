using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager current;
    public SceneLoader sceneLoader;
    public Animator doorAnimator;
    public Money money;
    public GameObject gotMoneyText;

    private void Awake()
    {
        current = this;
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
}
