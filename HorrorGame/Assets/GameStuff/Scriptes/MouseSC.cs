using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSC : MonoBehaviour
{
    bool cursorLocked = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            cursorLocked = !cursorLocked;
        }
        if (cursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
            cursorLocked = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            cursorLocked = true;
        }
    }

   
}
