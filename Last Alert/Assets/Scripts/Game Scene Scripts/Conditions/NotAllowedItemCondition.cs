using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotAllowedItemCondition : Condition {

    public GameObject[] allowedItems;

    public bool resetNotAllowedItems = true;

    public override void ResetCondition() {
        SetCompleted(false);
    }

    private void OnTriggerEnter(Collider other) {
        bool allowed = false;
        //Check if its in the array of allowed items
        foreach (GameObject item in allowedItems) {
            if (item == other.gameObject) {
                allowed = true;
            }
        }

        //If not allowed then say this condition is completed and if the
        //resetting is enabled then reset the not allowed item
        if (allowed == false) {
            if (resetNotAllowedItems == true) {
                Item item = other.GetComponent<Item>();
                if (item != null) {
                    item.ResetItem();
                }
            }
            
            SetCompleted(true);
        }
    }
}