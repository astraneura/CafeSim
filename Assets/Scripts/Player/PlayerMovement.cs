using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

// this script handles player movement and camera look functionality with the new Input System

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    private PlayerInputActions inputActions;
    private Rigidbody rb;
    private void Awake()
    {
        inputActions = new PlayerInputActions();
        rb = GetComponent<Rigidbody>();

        //Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;


    }

    private void OnEnable()
    {
        inputActions.PlayerMap.Enable();
    }

    private void OnDisable()
    {
        inputActions.PlayerMap.Disable();
    }

    private void FixedUpdate()
    {
        Vector2 moveInput = inputActions.PlayerMap.Move.ReadValue<Vector2>();
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        Vector3 velocity = move * moveSpeed;

        rb.linearVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);
    }
}
