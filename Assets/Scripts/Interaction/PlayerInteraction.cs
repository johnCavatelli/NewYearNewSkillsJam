using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public static interactionStates interactionState;
    public float interactionDistance;
    public LayerMask interactionMask;
    public Transform playerViewCamera;
    public TextManager textManager;
    public Image crosshair;
    public Animator LensAnimator;
    public Animator TutorialAnimator;
    private GameObject hoveredInteractable;

    private void Start()
    {
        interactionState = interactionStates.freeRoam;
    }
    private void Update()
    {
        switch (interactionState)
        {
            case interactionStates.freeRoam:
                if (IsHoveringOverInteractable())
                {
                    UpdateTip();
                    if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
                    {
                        Interact(hoveredInteractable.GetComponent<Interactable>());
                    }
                }
                else
                {
                    ClearTip();
                }
                if (Input.GetKeyDown(KeyCode.Q) || Input.GetMouseButtonDown(1))
                {
                    ZoomLens();
                }
                if (Input.GetKey(KeyCode.Tab))
                {
                    if (TutorialAnimator != null)
                    {
                        TutorialAnimator.SetBool("tutorialButton", true);
                    }
                }
                else
                {
                    if (TutorialAnimator != null)
                    {
                        TutorialAnimator.SetBool("tutorialButton", false);
                    }
                }
                break;
            case interactionStates.paused:
                break;
            default:
                break;
        }
    }

    private void Interact(Interactable interactable)
    {
        interactable.Interact();
    }

    private void ZoomLens()
    {
        LensAnimator.SetTrigger("zoom");
    }

    public void Imobilize()
    {//imobilize player for cutscene
        interactionState = interactionStates.paused;
        ClearTip();
    }

    private void UpdateTip()
    {
        if (hoveredInteractable != null)
        {
            textManager.UpdateTipText(hoveredInteractable.GetComponent<Interactable>().getTipText());
            crosshair.color = new Color(255, 0, 0);
        }
        else
        {
            Debug.LogWarning("Trying to update tip of something empty");
        }
    }

    private void ClearTip()
    {
        textManager.ClearTipText();
        crosshair.color = new Color(255, 10, 30);
    }

    private bool IsHoveringOverInteractable()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerViewCamera.position, playerViewCamera.forward, out hit, interactionDistance, interactionMask))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Interaction"))
            {
                hoveredInteractable = hit.transform.gameObject;
                //print("Hovering Over Interactable");
                return true;
            }
        }
        return false;
    }
}

public enum interactionStates
{
    freeRoam, inInteraction, paused
}
