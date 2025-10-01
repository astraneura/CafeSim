using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook_Default : MouseLook
{
    Vector2 MousePosition() => Mouse.current.delta.ReadValue();

    void Update()
    {
        DoLookMouse(MousePosition());
    }    
}
