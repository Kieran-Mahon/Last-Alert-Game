using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour {

    [Header("Pick Up Information")]
    public float maxPickUpDistance = 5;
    public float minPickUpDistance = 1;
    public float rotateSpeed = 25;
    public int ignoreLayer = 2;

    [Header("References")]
    public GameObject itemHolderRef;
    public GameObject cameraRef;

    private bool holdingItem = false;
    private PickUp itemRef;
    private float pickUpDistance;

    void Start() {
        GameReferenceGetter.pickUpControllerRef = this;
    }

    public void TryMoveItem() {
        //Check if no items are held
        if (holdingItem == false) {
            //Check if the players wants to pick up an item
            PickUpItem();
        } else {
            //Move item
            MoveItem();
            //Check if the player wants to drop the item
            PutDownItem();
        }
    }

    private void PickUpItem() {
        //Check if key is pressed
        if (Input.GetKeyDown(KeyboardController.itemPickUpKey)) {
            //See if there is an item in front of the player
            if (Physics.Raycast(cameraRef.transform.position, cameraRef.transform.TransformDirection(Vector3.forward), out RaycastHit hit, maxPickUpDistance, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore)) {
                //Make sure the item is not too close to the player
                if (hit.distance <= minPickUpDistance) {
                    return;
                }
                //Check if the item has the pick up tag
                PickUp tempPickUp = hit.collider.GetComponent<PickUp>();
                if (tempPickUp != null) {
                    holdingItem = true;
                    itemRef = tempPickUp;
                    itemRef.held = true;
                    //Set the items parent to the holder object
                    SetItemParent();
                    //Set the pick up distance
                    pickUpDistance = itemHolderRef.transform.localPosition.z;
                    //Give the item reference the ignore raycast layer
                    itemRef.gameObject.layer = ignoreLayer;
                    //Disable the physics on the item if any
                    Rigidbody rigidbody = itemRef.rigidbodyRef;
                    if (rigidbody != null) {
                        rigidbody.isKinematic = true;
                    }
                }
            }
        }
    }

    private void SetItemParent() {
        itemRef.transform.parent = itemHolderRef.transform;
        itemHolderRef.transform.localPosition = new Vector3(0, 0, itemRef.transform.localPosition.z);
        itemRef.transform.localPosition = Vector3.zero;
    }

    private void RemoveItemParent() {
        itemRef.transform.parent = null;
        itemHolderRef.transform.localPosition = Vector3.zero;
    }

    //Code which is ran when the item is being moved
    private void MoveItem() {
        KeepLevel();
        RotateItem();
        ChangeDistance();
    }

    //Keep item level
    private void KeepLevel() {
        itemRef.transform.eulerAngles = new Vector3(0, itemRef.transform.eulerAngles.y, 0);
    }

    //Rotate the item
    private void RotateItem() {
        float rotateAmount = 0;
        //Rotate the item left
        if (Input.GetKey(KeyboardController.itemRotateLeftKey)) {
            rotateAmount += rotateSpeed;
        }
        //Rotate the item right
        if (Input.GetKey(KeyboardController.itemRotateRightKey)) {
            rotateAmount -= rotateSpeed;
        }
        //Apply new rotation
        itemRef.transform.Rotate(new Vector3(0, rotateAmount * Time.deltaTime, 0));
    }

    //Change item distance
    private void ChangeDistance() {
        float shortestDistance = pickUpDistance;
        if (Physics.Raycast(cameraRef.transform.position, cameraRef.transform.TransformDirection(Vector3.forward), out RaycastHit hit, maxPickUpDistance, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore)) {
            //Move the item closer to the player
            float newDistance = hit.distance - itemRef.pickUpDistanceOffset;

            //If the new distance is shorter than the current distance then change to the new distance
            if (newDistance < shortestDistance) {
                shortestDistance = newDistance;
            }
        }

        //Move object
        itemHolderRef.transform.localPosition = new Vector3(0, 0, shortestDistance);

        //Release item if too close to the player
        //if (shortestDistance <= (minPickUpDistance * 1)) {
        //    ReleaseItem();
        //}
    }

    //Drop the item
    private void PutDownItem() {
        if (holdingItem == true) {
            //Check if key is pressed
            if (Input.GetKeyDown(KeyboardController.itemPickUpKey)) {
                ReleaseItem();
            }
        }
    }

    //Disconnect the item from the player
    private void ReleaseItem() {
        //Release item from holder
        RemoveItemParent();
        //Give the item reference the default layer (0)
        itemRef.gameObject.layer = 0;
        //Enable the physics on the item if any
        Rigidbody rigidbody = itemRef.rigidbodyRef;
        if (rigidbody != null) {
            rigidbody.isKinematic = false;
        }
        itemRef.held = false;
        itemRef = null;
        holdingItem = false;
    }

    public void DropItem(bool dropAtStart) {
        //Check if holding an item
        if (holdingItem == true) {
            //Save reference of item for reseting to its start location
            PickUp tempItem = itemRef;
            //Release item
            ReleaseItem();
            //Drop at start location
            if (dropAtStart == true) {
                //Set to start location
                tempItem.ResetPickUp();
            }
        }
    }
}