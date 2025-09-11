using UnityEngine;
using UnityEngine.InputSystem;

// this script handles player movement and camera look functionality with the new Input System

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    [Header("Look Settings")]
    public Transform cameraTransform;
    public Transform cameraPivot;
    public float mouseSensitivity = 2f;
    public float pitchClamp = 90f;

    private PlayerInputActions inputActions;
    private Rigidbody rb;

    private float cameraPitch = 0f;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = true;

       
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

    private void LateUpdate()
    {
        Vector2 lookInput = inputActions.PlayerMap.Look.ReadValue<Vector2>();

        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime * 200f;
        Debug.Log("Mouse X: " + mouseX);
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime * 200f;
        Debug.Log("Mouse Y: " + mouseY);

        // Rotate player (yaw)
        transform.Rotate(Vector3.up * mouseX);

        // Rotate camera (pitch)
        cameraPitch -= mouseY;
        cameraPitch = Mathf.Clamp(cameraPitch, -pitchClamp, pitchClamp);

        if (cameraTransform != null)
        {
            cameraTransform.localRotation = Quaternion.Euler(cameraPitch, 0f, 0f);
            cameraPivot.localRotation = Quaternion.Euler(cameraPitch, 0f, 0f);
        }
    }
}
