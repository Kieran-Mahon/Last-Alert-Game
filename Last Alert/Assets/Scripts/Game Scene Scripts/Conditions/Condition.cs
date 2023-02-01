using UnityEngine;

public abstract class Condition : MonoBehaviour {
    private bool completed = false;

    public abstract void ResetCondition();

    public virtual bool GetCompleted() {
        return completed;
    }

    public virtual void SetCompleted(bool newValue) {
        completed = newValue;
    }
}