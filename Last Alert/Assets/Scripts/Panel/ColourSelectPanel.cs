using System;
using UnityEngine;
using UnityEngine.UI;

public class ColourSelectPanel : Panel {

    [Header("Colour Select Info")]
    [SerializeField]
    private ColourArea[] colourAreas;
    
    [Header("Animations")]
    public Animator wrongColoursAnimator;

    //Reset the panel
    public override void ResetResult() {
        SetCompleted(false);

        //Reset each area
        foreach (ColourArea area in colourAreas) {
            area.ResetArea();
        }

        //Change to the uncompleted screen
        HideAllUI();
        uncompletedUI.SetActive(true);
    }
    
    //Change the area's colour
    //The info variable should have a layout as below
    //1,-1
    //area index,amount
    public void ChangeColour(string info) {
        //No need for error checking as its a dev function
        string[] details = info.Split(",");
        //0=area index, 1=amount
        colourAreas[int.Parse(details[0])].ChangeOption(int.Parse(details[1]));
    }

    //Function used to submit the colours
    public void SubmitColours() {
        bool allCorrect = true;

        //Check if any are wrong
        foreach (ColourArea area in colourAreas) {
            if (area.CheckForCompleted() == false) {
                allCorrect = false;
            }
        }
        
        if (allCorrect == true) {
            //All colours are correct
            SetCompleted(true);

            //Switch screens
            HideAllUI();
            completedUI.SetActive(true);
        } else {
            //Display the wrong colours animation and hide it after 1.5 seconds
            wrongColoursAnimator.SetBool("Show", true);
            Invoke("HideAnimation", 1.5f);
        }
    }

    private void HideAnimation() {
        wrongColoursAnimator.SetBool("Show", false);
    }

    [Serializable]
    private class ColourArea {

        public Image display;
        public Sprite[] options;
        private int currentOption = 0;
        public int correctOption;

        //Change active option
        public void ChangeOption(int changeAmount) {
            int newOption = currentOption + changeAmount;
            //Reset if more
            if (newOption > (options.Length - 1)) {
                currentOption = 0;
            } else if (newOption < 0) { //Set to max
                currentOption = options.Length - 1;
            } else { //Change value
                currentOption = newOption;
            }

            //Change display image
            UpdateDisplay();
        }

        //Update display
        private void UpdateDisplay() {
            //If more than 0 then set it to it's sprite
            if (options.Length > 0) {
                display.sprite = options[currentOption];
            } else {
                //Show no sprite if no options
                display.sprite = null;
            }
        }

        //Return true if the current option is equal to the correct option
        public bool CheckForCompleted() {
            return (currentOption == correctOption);
        }

        //Reset the area
        public void ResetArea() {
            currentOption = 0;
            UpdateDisplay();
        }
    }
}