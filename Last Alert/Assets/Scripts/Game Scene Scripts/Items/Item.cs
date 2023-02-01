using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    //Start position and rotation
    private Vector3 startPos;
    private Quaternion startRot;
    //Reference to the rigidbody if any
    private Rigidbody rigidbodyRef;
    //Start kinematic value
    private bool kinematic;
    //Pause rigidbody velocity
    private Vector3 velocity;
    //Reference to the pick up script if any
    private PickUp pickUpRef;

    void Start() {
        //Get rigidbody
        rigidbodyRef = GetComponent<Rigidbody>();
        //Get pick up script
        pickUpRef = GetComponent<PickUp>();
        //Save start data
        UpdateStartData();
    }

    public void ResetItem() {
        //Remove item from pick up controller if the player is held onto it
        if (pickUpRef != null) {
            if (pickUpRef.held == true) {
                GameReferenceGetter.pickUpControllerRef.DropItem(false);
            }
        }

        //Pause physics
        if (rigidbodyRef != null) {
            rigidbodyRef.isKinematic = true;
        }

        //Move object to start location and rotation
        transform.position = startPos;
        transform.rotation = startRot;

        //Set physics to start value
        if (rigidbodyRef != null) {
            rigidbodyRef.isKinematic = kinematic;
        }
    }

    public void Pause() {
        //Don't pause if held (if it has a pick up script)
        if (pickUpRef != null) {
            if (pickUpRef.held == true) {
                return;
            }
        }
        if (rigidbodyRef != null) {
            //Save velocity and set kinematic to true
            velocity = rigidbodyRef.velocity;
            rigidbodyRef.isKinematic = true;
        }
    }

    public void Unpause() {
        //Don't unpause if held (if it has a pick up script)
        if (pickUpRef != null) {
            if (pickUpRef.held == true) {
                return;
            }
        }
        if (rigidbodyRef != null) {
            //Set velocity and set kinematic to false
            rigidbodyRef.isKinematic = false;
            rigidbodyRef.velocity = velocity;
        }
    }

    public void UpdateStartData() {
        //Save pos and rot
        startPos = transform.position;
        startRot = transform.rotation;
        if (rigidbodyRef != null) {
            kinematic = rigidbodyRef.isKinematic;
        }
    }
}