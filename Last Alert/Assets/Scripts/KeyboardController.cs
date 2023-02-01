using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : MonoBehaviour {

    public static KeyboardController instance;

    //Movement keys
    public static KeyCode runKey = KeyCode.LeftShift;
    public static KeyCode jumpKey = KeyCode.Space;
    public static KeyCode crouchKey = KeyCode.LeftControl;
    //Pick up key
    public static KeyCode itemPickUpKey = KeyCode.Mouse0;
    public static KeyCode itemRotateLeftKey = KeyCode.Q;
    public static KeyCode itemRotateRightKey = KeyCode.E;
    //Pause key
    public static KeyCode pauseKey = KeyCode.Escape;

    private void Start() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(this);
        }
    }

    //Add a key to an action
    public static bool SetKey(Action action, KeyCode newKeyCode) {
        //Check if the new key is already in use
        if (CheckForKeyInUse(newKeyCode) == true) {
            //Return false to say the key was NOT set
            return false;
        }

        //Call the AddToArray function depending on what action was called
        switch (action) {
            case Action.RUN:
                runKey = newKeyCode;
                break;
            case Action.JUMP:
                jumpKey = newKeyCode;
                break;
            case Action.CROUCH:
                crouchKey = newKeyCode;
                break;
            case Action.ITEMPICKUP:
                itemPickUpKey = newKeyCode;
                break;
            case Action.ITEMROTATELEFT:
                itemRotateLeftKey = newKeyCode;
                break;
            case Action.ITEMROTATERIGHT:
                itemRotateRightKey = newKeyCode;
                break;
            case Action.PAUSE:
                pauseKey = newKeyCode;
                break;
            default:
                break;
        }

        //Return true as the key was set
        return true;
    }

    //Returns true if the key is in use
    private static bool CheckForKeyInUse(KeyCode key) {
        bool used = false;
        if (runKey == key) {
            used = true;
        }
        if (jumpKey == key) {
            used = true;
        }
        if (crouchKey == key) {
            used = true;
        }
        if (itemPickUpKey == key) {
            used = true;
        }
        if (itemRotateLeftKey == key) {
            used = true;
        }
        if (itemRotateRightKey == key) {
            used = true;
        }
        if (pauseKey == key) {
            used = true;
        }
        return used;
    }

    public enum Action {
        RUN,
        JUMP,
        CROUCH,
        ITEMPICKUP,
        ITEMROTATELEFT,
        ITEMROTATERIGHT,
        PAUSE
    }
}