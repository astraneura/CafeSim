using System.Collections;
using UnityEngine;

public abstract class MouseLook : MonoBehaviour
{
    [SerializeField] protected float mouseSensitivity = 1f;
    [SerializeField] protected Transform playerCamera;
    [SerializeField] protected float maximumRotationSpeedDegreesPerSecond;
    [SerializeField] protected float maximumPitchSpeedDegreesPerSecond;

    protected float xRotation = 0f;

    protected virtual void Awake()
    {
        Cursor.visible = true;
    }

    protected virtual void DoLookMouse(Vector2 direction)
    {
        direction *= mouseSensitivity;
        // Yaw: rotate player (left/right)
        transform.Rotate(Vector3.up * direction.x, Space.World);

        // Pitch: rotate camera (up/down)
        xRotation -= direction.y;
        xRotation = Mathf.Clamp(xRotation, -45f, 35f);
        if (playerCamera != null)
        {
            playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }
    }
}
