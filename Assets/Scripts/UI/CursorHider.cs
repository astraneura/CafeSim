using UnityEngine;
using UnityEngine.InputSystem;

public class CursorHider : MonoBehaviour
{
    void Update()
    {
        // Hide cursor when using controller sticks or dpad
        if (Gamepad.current != null && 
            (Gamepad.current.leftStick.ReadValue().magnitude > 0.1f ||
             Gamepad.current.dpad.ReadValue().magnitude > 0.1f))
        {
            Cursor.visible = false;
        }
        else if (Mouse.current != null && Mouse.current.delta.ReadValue().magnitude > 0.1f)
        {
            Cursor.visible = true;
        }
    }
}
