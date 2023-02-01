using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverseCondition : Condition {
    
    //Condition to inverse
    public Condition condition;

    public override void ResetCondition() {
        condition.ResetCondition();
    }

    public override bool GetCompleted() {
        //Get inverse value
        bool value = !condition.GetCompleted();
        //Set value
        SetCompleted(value);
        //Return value
        return value;
    }
}