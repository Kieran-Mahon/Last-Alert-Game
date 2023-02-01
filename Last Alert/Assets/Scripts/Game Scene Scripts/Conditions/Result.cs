using UnityEngine;

public abstract class Result : MonoBehaviour {
    private bool completed = false;

    public Condition[] conditions;

    public abstract void ResetResult();

    public bool GetCompleted() {
        return completed;
    }

    public void SetCompleted(bool newValue) {
        completed = newValue;
    }
}