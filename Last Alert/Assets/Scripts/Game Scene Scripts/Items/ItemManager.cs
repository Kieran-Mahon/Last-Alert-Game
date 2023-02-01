using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {
    private Item[] pickUpObjects;

    public void GetAllPickUps() {
        pickUpObjects = FindObjectsOfType<Item>();
    }

    //Pause all pickups
    public void PauseAll() {
        foreach (Item pickUp in pickUpObjects) {
            pickUp.Pause();
        }
    }

    //Unpause all pickups
    public void UnpauseAll() {
        foreach (Item pickUp in pickUpObjects) {
            pickUp.Unpause();
        }
    }
}