using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetConditionResult : Result {

    public Condition[] conditionsToReset;

    public bool resetConditionsOnReset = true;

    //Reset all conditions when this is reset
    public override void ResetResult() {
        foreach (Condition condition in conditions) {
            condition.ResetCondition();
        }

        //Reset the conditions to be reset on reset
        if (resetConditionsOnReset == true) {
            ResetConditions();
        }

        SetCompleted(false);
    }

    void Update() {
        //Check if all conditions are completed
        bool allCompleted = true;
        foreach (Condition condition in conditions) {
            if (condition.GetCompleted() == false) {
                allCompleted = false;
            }
        }

        //If all completed then reset the conditions which need to be reset
        if (allCompleted == true) {
            ResetConditions();
        }

        SetCompleted(allCompleted);
    }

    //Reset the conditions within the conditionsToReset array
    private void ResetConditions() {
        foreach (Condition condition in conditionsToReset) {
            condition.ResetCondition();
        }
    }
}