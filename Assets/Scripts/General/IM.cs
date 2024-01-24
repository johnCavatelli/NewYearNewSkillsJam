using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IM : MonoBehaviour
{
    public static IM current;

    [Header("Camera Settings")]
    public float xSens;
    public float ySens;
    // [SerializeField] private float ySens {get;}


    [Header("Movement Input")]
    [SerializeField] private  KeyCode jump;
    [SerializeField] private  KeyCode dash;
    [SerializeField] private KeyCode slide;
    
    //calculated fields
    public float verticalInput => Input.GetAxisRaw("Vertical");
    public float horizontalInput => Input.GetAxisRaw("Horizontal");
    public float mouseAxisX => Input.GetAxis("Mouse X");
    public float mouseAxisY => Input.GetAxis("Mouse Y");
    public bool isJumping =>Input.GetKey(jump);
    public bool isShooting =>Input.GetMouseButton(0);
    public bool isDashKeyDown =>Input.GetKey(dash);


    //private fields
    private bool lockedMouse;
    

    private void Awake() {
        if(current == null)
            current = this;
    }
     
    private void Start() {
        SetMouseLock(true);
    }

    public void SetMouseLock(bool l){
        lockedMouse = l;
        Cursor.lockState = l ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !l;
        // print("setting lock " + l);
    }
}
