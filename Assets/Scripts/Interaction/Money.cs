using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : Interactable
{
    public GameObject model;
    public BoxCollider bc;
    private bool isMoneyActive;

    private void Start()
    {
        isMoneyActive = false;
        model.SetActive(isMoneyActive);
        bc.enabled = isMoneyActive;
    }

    public void EnableMoney()
    {
        isMoneyActive = true;
        model.SetActive(isMoneyActive);
        bc.enabled = isMoneyActive;
    }

    public override void Interact()
    {
        if (isMoneyActive)
        {
            LevelManager.current.MoneyPickedUp();
            isMoneyActive = false;
            model.SetActive(isMoneyActive);
            bc.enabled = isMoneyActive;
            gameObject.GetComponent<AudioSource>().Play();
        }
    }
}
