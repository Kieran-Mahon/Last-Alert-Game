using UnityEngine;
using UnityEngine.EventSystems;

public class CustomInputModule : StandaloneInputModule {
    protected override MouseState GetMousePointerEventData(int id) {
        CursorLockMode lockState = Cursor.lockState;
        bool cursorVis = Cursor.visible;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
        var mouseState = base.GetMousePointerEventData(id);

        Cursor.lockState = lockState;
        Cursor.visible = cursorVis;
        return mouseState;
    }
}