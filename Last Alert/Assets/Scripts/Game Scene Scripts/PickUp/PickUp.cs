using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Item))]
public class PickUp : MonoBehaviour {
    //Pickup distance change offset
    [HideInInspector]
    public float pickUpDistanceOffset = 0.2f;
    //Used to stop un/pausing while held by the pick up controller
    [HideInInspector]
    public bool held = false;
    //Reference to the objects rigidbody if any
    [HideInInspector]
    public Rigidbody rigidbodyRef;
    //Reference to the objects item script
    private Item itemRef;

    void Start() {
        //Get rigidbody
        rigidbodyRef = GetComponent<Rigidbody>();
        //Get item script
        itemRef = GetComponent<Item>();
    }

    //Call the item reset function
    public void ResetPickUp() {
        itemRef.ResetItem();
    }
}