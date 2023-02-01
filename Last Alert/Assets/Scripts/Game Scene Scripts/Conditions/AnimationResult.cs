using UnityEngine;

public class AnimationResult : Result {

    private bool changed = false; //Variable used to make sure the animations are only called once
    public Animator[] animators;
    public string[] animatorBools;
    public bool[] animatorCompletedValues;

    public override void ResetResult() {
        //Reset the result and change the animators to false
        SetCompleted(false);
        ChangeAnimatorState(false);
        changed = false;
    }

    void Update() {
        //Check if all conditions are completed
        bool allCompleted = true;
        foreach (Condition condition in conditions) {
            if (condition.GetCompleted() == false) {
                allCompleted = false;
            }
        }

        //If completed then change all animators to true
        if (allCompleted == true) {
            //Make sure the change animation is only called once 
            if (changed == false) {
                ChangeAnimatorState(true);
                changed = true;
            }
        } else {
            if (GetCompleted() == true) {
                ResetResult();
            }
        }
        //Change completed
        SetCompleted(allCompleted);
    }

    //Change the state which the animators are in
    private void ChangeAnimatorState(bool changeToCompletedValue) {
        for (int i = 0; i < animators.Length; i++) {
            //Make sure the animator bool index actually exist
            if ((i < animatorBools.Length) && (i < animatorCompletedValues.Length)) {
                bool newValue;
                if (changeToCompletedValue == true) {
                    newValue = animatorCompletedValues[i]; //Make new value equal to the completed value
                } else {
                    newValue = !animatorCompletedValues[i]; //Make new value opposite of the completed value
                }

                //Set the value of the animator
                animators[i].SetBool(animatorBools[i], newValue);
            }
        }
    }
}
