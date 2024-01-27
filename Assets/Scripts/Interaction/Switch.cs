using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : Interactable
{
    public Animator animator;
    public bool startOn;
    public SwitchDevice switchDevice;
    public bool locked = false;//can disable a switch from being flipped

    private void Start()
    {
        if (!startOn)
        {
            animator.SetTrigger("switch");
        }
    }

    public override void Interact()
    {
        if (!locked)
        {
            // print("In switch");
            gameObject.GetComponent<AudioSource>().Play();
            animator.SetTrigger("switch");
            if (switchDevice != null)
            {
                switchDevice.SwitchState();
            }
            LevelManager.current.SwitchFliped();
        }
    }

    public void SwitchSwitchBecauseItUpdatedElsewhere()
    {
        animator.SetTrigger("switch");
        gameObject.GetComponent<AudioSource>().Play();
        LevelManager.current.SwitchFliped();
    }
}
