using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpaceZone : MonoBehaviour {
    public UnityEvent entered, exited;

    void OnTriggerEnter(Collider other) {
        //check if player has entered
        if (other.name == "Player") {
            print("entered");
        }
    }

    void OnTriggerExit(Collider other) {
        //checks if player has exited
        if (other.name == "Player") {
            print("exited");
        }
    }
}
