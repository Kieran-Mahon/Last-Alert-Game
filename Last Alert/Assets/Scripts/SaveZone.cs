using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Add this script trigger object
//Add functionallity in the inspector window
public class SaveZone : MonoBehaviour {
    public UnityEvent entered, exited;

    void OnTriggerEnter(Collider other) {
        //check if player has entered
        if (other.name == "Player") {
            entered.Invoke();
        }
    }

    void OnTriggerExit(Collider other) {
        //checks if player has exited
        if (other.name == "Player") {
            exited.Invoke();
        }
    }
}