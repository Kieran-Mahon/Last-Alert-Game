using UnityEngine;

public class PlayerInAreaCondition : Condition {

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            SetCompleted(true);
        }
    }

    public override void ResetCondition() {
        SetCompleted(false);
    }
}