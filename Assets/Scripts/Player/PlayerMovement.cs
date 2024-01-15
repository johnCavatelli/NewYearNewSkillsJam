using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Public References")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Transform orientation;
    [SerializeField] private Vector3 startingRotation;
    [SerializeField] private float playerHeight;
    [SerializeField] private float stamina;

    [Header("Movement Parameters")]
    [SerializeField] private float walkSpeed = 10.0f;
    [SerializeField] private float slideSpeed = 7.0f;
    [SerializeField] private float airbornSpeed = 7.0f;
    [SerializeField] private float airAccelerationMultiplier = 0.8f;
    // [SerializeField] private float crouchSpeed = 1.5f;
    [SerializeField] private float cyoteTime = 0.1f;

    [Header("Dashing Parameters")]
    [SerializeField] private float dashSpeed = 30.0f;
    // [SerializeField] private float dashLength = 30.0f;
    public float dashForce;
    public float dashUpwardForce;
    public float dashCooldown;
    [Header("Physics Parameters")]
    public float groundDrag;
    public float jumpForce;
    public float jumpCooldown;
    public float terminalVelocity = 10f;
    public LayerMask whatIsGround;
    public float decelleration;//how fast the player transitions between different movement velocities

    [Header("Look Parameters")]
    [SerializeField, Range(1, 10)] private float lookSpeedX = 2.0f;
    [SerializeField, Range(1, 10)] private float lookSpeedY = 2.0f;

    //private fields
    private float rotationX = 0;
    private float rotationY = 0;
    private Rigidbody rb;
    private bool readyToJump;
    private bool readyToDash;
    private bool grounded;
    private Vector3 moveDirection;
    private float moveSpeed => rb.velocity.magnitude;
    private float desiredMoveSpeed => MoveStateToMoveSpeed(moveState);
    private bool hasStamina => stamina >= 0f;
    private bool moveAnimMutex = false;
    private movementState moveState;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveState = movementState.walking;
        rb.freezeRotation = true;
        readyToJump = true;
        readyToDash = true;
        rotationX = startingRotation.x;//set the initial rotation of the player rather than defaulting to 0
        rotationY = startingRotation.y;
    }

    private void Update()
    {
        CheckGrounded();
        HandleMouseLook();
        HandleInput();
        MovePlayer();
        SpeedControl();
        // print(moveState + " moveDir" + moveDirection);
        ApplyDrag();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void ApplyDrag()
    {
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void CheckGrounded()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);
    }

    private void HandleInput()
    {
        moveDirection = orientation.forward * IM.current.verticalInput + orientation.right * IM.current.horizontalInput;
        if (moveAnimMutex)
        {
            return;
        }
        else if (IM.current.isJumping && readyToJump && grounded)
        {
            // calculate movement direction            
            moveState = movementState.jumping;
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
        else
        {
            if (grounded)
            {
                moveState = movementState.walking;
            }
            else
            {
                moveState = movementState.air;
            }
        }


    }

    private void ResetAnimMutex()
    {
        moveAnimMutex = false;
    }

    private void Dash()
    {
        rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
        moveDirection = moveDirection == Vector3.zero ? orientation.forward : moveDirection;
        Vector3 dashDir = moveDirection * dashForce;
        dashDir += transform.up * dashUpwardForce;
        rb.AddForce(dashDir, ForceMode.Impulse);
    }
    private void Jump()
    {
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
    private void ResetDash() { readyToDash = true; }
    private void MovePlayer()
    {
        rb.AddForce(moveDirection.normalized * desiredMoveSpeed * 10f * (!grounded ? airAccelerationMultiplier : 1f), ForceMode.Force);
        // if(moveState == movementState.dashing)
        //     rb.AddForce(moveDirection.normalized * desiredMoveSpeed * 10f, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        // limit velocity if needed
        if (flatVel.magnitude > desiredMoveSpeed)
        {
            float calculatedMoveSpeed = Mathf.Lerp(moveSpeed, desiredMoveSpeed, decelleration * Time.deltaTime);
            Vector3 limitedVel = flatVel.normalized * calculatedMoveSpeed;

            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y > terminalVelocity ? terminalVelocity : rb.velocity.y, limitedVel.z);
        }
    }

    private float MoveStateToMoveSpeed(movementState ms)
    {
        switch (ms)
        {
            case movementState.walking:
                return walkSpeed;
            case movementState.dashing:
                return dashSpeed;
            case movementState.sliding:
                return slideSpeed;
            case movementState.air:
                return airbornSpeed;
            default:
                return 0f;
        }
    }

    private void HandleMouseLook()
    {
        rotationX -= IM.current.mouseAxisY * lookSpeedY * IM.current.ySens;
        rotationX = Mathf.Clamp(rotationX, -90, 90);
        rotationY += IM.current.mouseAxisX * lookSpeedX * IM.current.xSens;
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0);
        orientation.rotation = Quaternion.Euler(0, rotationY, 0);
    }

    private enum movementState
    {
        walking,
        sliding,
        jumping,
        air,
        wallSliding,
        dashing,
        stomping
    }
}