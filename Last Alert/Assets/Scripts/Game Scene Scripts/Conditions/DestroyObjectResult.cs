using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectResult : Result {
    //Script to all objects if all conditions are completed

    
    //Objects to destroy
    public GameObject[] objects;
    private bool destroyed = false;

    //Reset condition
    public override void ResetResult() {
        foreach (Condition condition in conditions) {
            condition.ResetCondition();
        }

        SetCompleted(false);
    }

    void Update() {
        bool allCompleted = true;

        //Check if any are false
        foreach (Condition condition in conditions) {
            if (condition.GetCompleted() == false) {
                allCompleted = false;
            }
        }

        //Destory if all are completed
        if (allCompleted == true) {
            if (destroyed == false) {
                DestroyAll();
            }
            destroyed = true;
        }

        SetCompleted(allCompleted);
    }

    private void DestroyAll() {
        foreach (GameObject GO in objects) {
            PickUp pickUpRef = GO.GetComponent<PickUp>();
            if (pickUpRef != null) {
                if (pickUpRef.held == true) {
                    GameReferenceGetter.pickUpControllerRef.DropItem(false);
                }
            }
            Destroy(GO);
        }
    }
}