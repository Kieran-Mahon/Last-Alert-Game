public class QRPanel : Panel {
    
    //Reset the panel
    public override void ResetResult() {
        //Reset all conditions
        foreach (Condition condition in conditions) {
            condition.ResetCondition();
        }

        //Change to the uncompleted screen
        HideAllUI();
        uncompletedUI.SetActive(true);
        
        SetCompleted(false);
    }

    //Check if the panel is correct on each frame
    void Update() {
        bool allCorrect = true;

        //Check if any is not complete
        foreach (Condition condition in conditions) {
            if (condition.GetCompleted() == false) {
                allCorrect = false;
            }
        }

        //Panel completed
        if (allCorrect == true) {
            SetCompleted(true);

            //Switch to the completed screen
            HideAllUI();
            completedUI.SetActive(true);
        }
    }
}