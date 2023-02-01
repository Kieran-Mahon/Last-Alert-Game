using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneController : MonoBehaviour {
    //Start state
    public StartState startState;

    //Home reference
    public GameObject homeMenu;

    //Settings reference
    public GameObject settingsUI;
    public Settings settingRef;

    //Difficulties reference
    public GameObject difficultiesUI;

    //References
    //UI controller reference
    //etc. etc..
    public Button continueBtn;

    //timer variable
    private float startTime;

    // Start is called before the first frame update
    void Start() {
        ChangeStartState(StartState.HOMEMENU);


        if (SaveSystem.IsSaved()) {
            print("data loading...");
            SaveSystem.LoadSettings();
        }
        if (SaveSystem.IsContinue()) {
            continueBtn.interactable = true;
        }
    }

    // Update is called once per frame
    void Update() {
        if (startState == StartState.HOMEMENU) {

        } else if (startState == StartState.SETTINGMENU) {

        } else if (startState == StartState.DIFFICULTIES) {

        } else if (startState == StartState.CUTSCENE) {

        }
    }

    //Actions which need to be done on the change state call
    public void ChangeStartState(StartState newStartState) {
        if (newStartState == StartState.HOMEMENU) {
            MouseController.UnlockMouse();
            homeMenu.SetActive(true);
            settingsUI.SetActive(false);
            settingRef.enabled = false;
            difficultiesUI.SetActive(false);

        } else if (newStartState == StartState.SETTINGMENU) {
            MouseController.UnlockMouse();
            settingsUI.SetActive(true);
            settingRef.enabled = true;
            homeMenu.SetActive(false);

        } else if (newStartState == StartState.DIFFICULTIES) {
            MouseController.UnlockMouse();
            difficultiesUI.SetActive(true);
            homeMenu.SetActive(false);

        } else if (newStartState == StartState.CUTSCENE) {
            MouseController.UnlockMouse();
        }
        //Change state
        startState = newStartState;
    }

    //New Game Button
    public void NewGame() {
        ChangeStartState(StartState.DIFFICULTIES);
    }

    //Continue Button
    public void ContinueGame() {
        //continues game from last checkpoint save (if available)
        GameTimer.SetTimer(SaveSystem.GetTimer());
        AudioManager.instance.Pause("homeTheme");
        AudioManager.instance.Play("gameBackground");
        SceneController.SwitchToGameScene();
    }

    //Settings Button
    public void OpenSettings() {
        ChangeStartState(StartState.SETTINGMENU);
    }

    //temporary button to return to home menu for testing
    public void CloseSettings() {
        ChangeStartState(StartState.HOMEMENU);
    }

    public void StartTutorial(){
        SceneController.SwitchToTutorialScene();
    }

    public void StartGame(){
        GameTimer.SetTimer(startTime);
        SaveSystem.ClearSave();
        AudioManager.instance.Pause("homeTheme");
        AudioManager.instance.Play("gameBackground");
        SceneController.SwitchToGameScene();
    }

    public void SetDifficulty(GameObject difficulty){
        //set timer based on chosen difficulty
        switch(difficulty.name){
            case "btnEasy":
                this.startTime = 15 * 60; //15 minutes
                break;

            case "btnNormal":
                this.startTime = 8 * 60; //8 minutes
                break;

            case "btnHard":
                this.startTime = 3 * 60; //3 minutes
                break;
        }

        //switch to game scene
        StartGame();
    }

    //Quit Button
    public void Quit() {
        Application.Quit();
        print("Quit");
    }
}

//Start scene states
public enum StartState {
    HOMEMENU,
    SETTINGMENU,
    DIFFICULTIES,
    CUTSCENE
}