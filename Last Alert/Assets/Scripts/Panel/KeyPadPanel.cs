using UnityEngine;
using TMPro;

public class KeyPadPanel : Panel {

    [Header("Keypad Info")]
    public string correctCode;
    public string enteredCode;

    [Header("Keypad UI")]
    public TextMeshProUGUI codeLabel;

    [Header("Animations")]
    public Animator wrongCodeAnimator;

    //Reset the panel
    public override void ResetResult() {
        SetCompleted(false);
        //Reset the code and update it on the screen
        enteredCode = "";
        UpdateCodeLabel();
        //Change to the uncompleted screen
        HideAllUI();
        uncompletedUI.SetActive(true);
    }

    //Function called when key button is pressed
    public void KeyEntered(int num) {
        //Make sure the max num of keys is not pressed
        if (enteredCode.Length < correctCode.Length) {
            //Add key pressed to the entered code and update the label
            enteredCode += num;
            UpdateCodeLabel();
        }
    }

    //Function used to submit the code
    public void SubmitCode() {
        //Check if the code is correct
        if (enteredCode == correctCode) {
            //Code is correct
            SetCompleted(true);

            //Switch screens
            HideAllUI();
            completedUI.SetActive(true);
        } else {
            //Code is wrong
            enteredCode = "";
            UpdateCodeLabel();

            //Display animation and hide it after 1.5 seconds
            wrongCodeAnimator.SetBool("Show", true);
            Invoke("HideAnimation", 1.5f);
        }
    }

    private void HideAnimation() {
        wrongCodeAnimator.SetBool("Show", false);
    }

    //Write code to label
    private void UpdateCodeLabel() {
        string newText = "";
        char[] codeArray = enteredCode.ToCharArray();

        //Loop through each index of the code and add it to the text
        for (int i = 0; i < correctCode.Length; i++) {
            //Add left border
            newText += "[";

            //Add either spacing or the character
            if (i < codeArray.Length) {
                newText += codeArray[i];
            } else {
                newText += "  ";
            }

            //Add right border
            newText += "]";
        }

        //Set the label's text to the new text
        codeLabel.text = newText;
    }
}