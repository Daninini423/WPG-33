using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class MovementStateManager : MonoBehaviour
{
    public float currentMoveSpeed;
    public float walkSpeed = 3, walkBackSpeed =2;
    public float runSpeed = 7, runBackSpeed = 5;
    public float crouchSpeed = 2, crouchBackSpeed = 2;

    [HideInInspector] public Vector3 dir;
    [HideInInspector] public float hzInput, vInput;
    public CharacterController controller;

    [SerializeField] private float groundYOffset = 0.1f;
    [SerializeField] private LayerMask Ground;
    public Vector3 spherePosition;

    [SerializeField] private float gravity = -9.81f;
    public Vector3 velocity;




    MovementBaseState currentState;

    public IdleState Idle = new IdleState();
    public WalkingState Walk = new WalkingState();
    public CrouchState Crouch = new CrouchState();
    public RunState Run = new RunState();

    [HideInInspector] public Animator anim;

    
    

    void Start()
    {
        anim = GetComponent<Animator>();
        // Cache the CharacterController component for performance
        controller = GetComponent<CharacterController>();
        if (controller == null)
        {
            Debug.LogError("CharacterController component is missing!");
            enabled = false;
            return;
        }

        SwitchState(Idle);
    }

    private void Update()
    {
        // Calculate movement direction
        GetDirection();

        // Apply gravity
        ApplyGravity();

        anim.SetFloat("hzInput", hzInput);
        anim.SetFloat("vInput", vInput);

        // Combine movement and gravity, then move the character
        Vector3 finalMove = dir * currentMoveSpeed + velocity;
        controller.Move(finalMove * Time.deltaTime);

        currentState.UpdateState(this);
    }

    public void SwitchState(MovementBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    private void GetDirection()
    {
        hzInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");

        dir = transform.forward * vInput + transform.right * hzInput;



    }

    private bool IsGrounded()
    {
        spherePosition = new Vector3(transform.position.x, transform.position.y - groundYOffset, transform.position.z);
        return Physics.CheckSphere(spherePosition, controller.radius - 0.05f, Ground);
    }

    private void ApplyGravity()
    {
        if (IsGrounded())
        {
            // Reset gravity when grounded
            if (velocity.y < 0)
            {
                velocity.y = -2f; // Slight negative value to ensure contact with the ground
            }
        }
        else
        {
            // Apply gravity over time when not grounded
            velocity.y += gravity * Time.deltaTime;
        }
    }

    private void OnDrawGizmos()
    {
        // Visualize the ground check sphere in the Scene view
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(
            new Vector3(transform.position.x, transform.position.y - groundYOffset, transform.position.z),
            controller != null ? controller.radius - 0.05f : 0.5f
        );
    }


}
