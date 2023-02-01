using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwitchMovement : MonoBehaviour {
    public UnityEvent entered, exited;

    void OnTriggerEnter(Collider other) {
        Rigidbody rb = null;

        //check if player has entered
        if (other.name == "Player") {
            PlayerController.inSpace = true;
        } else if ((rb = other.GetComponent<Rigidbody>()) != null){
            rb.useGravity = false;
        }
    }

    void OnTriggerExit(Collider other) {
        Rigidbody rb = null;
        
        //checks if player has exited
        if (other.name == "Player") {
            PlayerController.inSpace = false;
        } else if ((rb = other.GetComponent<Rigidbody>()) != null) {
            rb.useGravity = true;
        }
    }
}