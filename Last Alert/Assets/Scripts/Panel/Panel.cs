using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Panel : Result {
    
    [Header("Panel UI")]
    public GameObject lockedUI;
    public GameObject hintUI;
    public GameObject uncompletedUI;
    public GameObject completedUI;

    //Hide all UI
    //(Hides the UI if it is not null)
    public void HideAllUI() {
        if (lockedUI != null) {
            lockedUI.SetActive(false);
        }
        if (hintUI != null) {
            hintUI.SetActive(false);
        }
        if (uncompletedUI != null) {
            uncompletedUI.SetActive(false);
        }
        if (completedUI != null) {
            completedUI.SetActive(false);
        }
    }
}