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

    private void UpdateTip()
    {
        if (hoveredInteractable != null)
        {
            textManager.UpdateTipText(hoveredInteractable.GetComponent<Interactable>().getTipText());
            crosshair.color = new Color(0, 0, 0);
        }
        else
        {
            Debug.LogWarning("Trying to update tip of something empty");
        }
    }

    private void ClearTip()
    {
        textManager.ClearTipText();
        crosshair.color = new Color(250, 10, 30);
    }

    private bool IsHoveringOverInteractable()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerViewCamera.position, playerViewCamera.forward, out hit, interactionDistance, interactionMask))
        {
            hoveredInteractable = hit.transform.gameObject;
            //print("Hovering Over Interactable");
            return true;
        }
        return false;
    }
}

public enum interactionStates
{
    freeRoam, inInteraction, paused
}