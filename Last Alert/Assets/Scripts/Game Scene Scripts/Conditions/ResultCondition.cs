using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultCondition : Condition {

    //Result reference
    public Result result;

    void Update() {
        SetCompleted(result.GetCompleted());
    }

    public override void ResetCondition() {
        result.ResetResult();
    }
}