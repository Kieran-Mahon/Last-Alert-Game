using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour {

    //Tutorial state
    public TutorialState TutorialState;

    //References
    public PlayerController playerControllerRef;
    public PickUpController pickUpControllerRef;
    public WinController winControllerRef;
    public ItemManager itemManagerRef; //Manages all pickup objects in the scene
    public Settings settingsRef;

    [Header("UI")]
    public GameObject pauseScreen;
    public GameObject settingsUI;
    public GameObject tutorialCompletedScreen;

    //Start is called before the first frame update
    void Start() {
        itemManagerRef.GetAllPickUps();
        ChangeTutorialState(TutorialState.TUTORIAL);
    }

    //Update is called once per frame
    void Update() {
        if (TutorialState == TutorialState.PAUSEMENU) {
            //Unpause game
            if (Input.GetKeyDown(KeyboardController.pauseKey)) {
                UnpauseTutorial();
            }

        } else if (TutorialState == TutorialState.SETTINGMENU) {
            //Unpause game
            if (Input.GetKeyDown(KeyboardController.pauseKey)) {
                UnpauseTutorial();
            }

        } else if (TutorialState == TutorialState.CUTSCENE) {

        } else if (TutorialState == TutorialState.TUTORIAL) {
            //Move player
            playerControllerRef.MovePlayer();
            playerControllerRef.MoveCamera();

            //Object pick up
            pickUpControllerRef.TryMoveItem();

            //Pause game
            if (Input.GetKeyDown(KeyboardController.pauseKey)) {
                PauseTutorial();
            }

            //Check for win
            if (winControllerRef.CheckForWin()) {
                TutorialCompleted();
            }
            
        } else if (TutorialState == TutorialState.COMPLETED) {

        }
    }

    //Actions which need to be done on the change state call
    public void ChangeTutorialState(TutorialState newTutorialState) {
        //BEFORE CHANGE
        if (TutorialState == TutorialState.PAUSEMENU) {

        } else if (TutorialState == TutorialState.SETTINGMENU) {

        } else if (TutorialState == TutorialState.CUTSCENE) {

        } else if (TutorialState == TutorialState.TUTORIAL) {

        } else if (newTutorialState == TutorialState.COMPLETED) {

        }

        //CHANGE
        TutorialState = newTutorialState;

        //AFTER CHANGE
        if (newTutorialState == TutorialState.PAUSEMENU) {
            HideAllScreens();
            pauseScreen.SetActive(true);
            settingsRef.enabled = false;
            MouseController.UnlockMouse();
        } else if (newTutorialState == TutorialState.SETTINGMENU) {
            HideAllScreens();
            settingsUI.SetActive(true);
            settingsRef.enabled = true;
            MouseController.UnlockMouse();
        } else if (newTutorialState == TutorialState.CUTSCENE) {

        } else if (newTutorialState == TutorialState.TUTORIAL) {
            HideAllScreens();
            settingsRef.enabled = false;
            MouseController.LockMouse();
        } else if (newTutorialState == TutorialState.COMPLETED) {
            MouseController.UnlockMouse();
            HideAllScreens();
            pickUpControllerRef.DropItem(true);
            tutorialCompletedScreen.SetActive(true);
        }
    }

    private void HideAllScreens() {
        pauseScreen.SetActive(false);
        settingsUI.SetActive(false);
        tutorialCompletedScreen.SetActive(false);
    }

    //Pause functions
    public void PauseTutorial() {
        ChangeTutorialState(TutorialState.PAUSEMENU);
        itemManagerRef.PauseAll();
    }

    public void UnpauseTutorial() {
        ChangeTutorialState(TutorialState.TUTORIAL);
        itemManagerRef.UnpauseAll();
    }

    //Resume Button
    public void ResumeGame() {
        UnpauseTutorial();
    }

    //Settings Button
    public void OpenSettings() {
        ChangeTutorialState(TutorialState.SETTINGMENU);
    }

    //temporary button to return to pause menu for testing
    public void CloseSettings() {
        ChangeTutorialState(TutorialState.PAUSEMENU);
    }

    //Exit Button
    public void Exit() {
        SceneController.SwitchToStartScene();
    }

    //Game Completed
    private void TutorialCompleted() {
        ChangeTutorialState(TutorialState.COMPLETED);
    }
}

//Tutorial Scene States
public enum TutorialState {
    PAUSEMENU,
    SETTINGMENU,
    CUTSCENE,
    TUTORIAL,
    COMPLETED
}