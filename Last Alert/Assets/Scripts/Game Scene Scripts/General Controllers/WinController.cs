using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinController : MonoBehaviour {

    public Condition[] conditions;

    public bool CheckForWin() {
        bool allCompleted = true;
        //Loop through all conditions and make sure none are not completed
        foreach (Condition condition in conditions) {
            if (condition.GetCompleted() == false) {
                allCompleted = false;
            }
        }
        return allCompleted;
    }

    public void ResetConditions() {
        foreach (Condition condition in conditions) {
            condition.ResetCondition();
        }
    }
}