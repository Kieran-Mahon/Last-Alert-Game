using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour {

    //Game state
    public GameState gameState;

    [Header("References")]
    public PlayerController playerControllerRef;
    public PickUpController pickUpControllerRef;
    public WinController winControllerRef;
    public ItemManager itemManagerRef; //Manages all pickup objects in the scene
    public Settings settingsRef;

    [Header("UI")]
    public TextMeshProUGUI timerRef;
    public GameObject pauseScreen;
    public GameObject settingsUI;
    public GameObject gameWinScreen;
    public GameObject gameOverScreen;

    //Start is called before the first frame update
    void Start() {
        itemManagerRef.GetAllPickUps();
        ChangeGameState(GameState.GAME);
    }

    //Update is called once per frame
    void Update() {
        if (gameState == GameState.PAUSEMENU) {
            //Unpause game
            if (Input.GetKeyDown(KeyboardController.pauseKey)) {
                UnpauseGame();
            }

        } else if (gameState == GameState.SETTINGMENU) {

        } else if (gameState == GameState.CUTSCENE) {

        } else if (gameState == GameState.GAME) {
           
            //Move player
            playerControllerRef.MovePlayer();
            playerControllerRef.MoveCamera();

            //Object pick up
            pickUpControllerRef.TryMoveItem();

            //Pause game
            if (Input.GetKeyDown(KeyboardController.pauseKey)) {
                PauseGame();
            }

            //Update time
            GameTimer.UpdateTime();
            timerRef.text = GameTimer.TimeLeftString();

            //Check for win
            if (winControllerRef.CheckForWin()) {
                GameWon();
            } else {
                //Check for lost
                if (GameTimer.GetTimeLeft() <= 0) {
                    //Game over
                    GameOver();
                }
            }

        } else if (gameState == GameState.GAMEWIN) {

        } else if (gameState == GameState.GAMEOVER) {

        }
    }

    //Actions which need to be done on the change state call
    public void ChangeGameState(GameState newGameState) {
        //BEFORE CHANGE
        if (gameState == GameState.PAUSEMENU) {

        } else if (gameState == GameState.SETTINGMENU) {

        } else if (gameState == GameState.CUTSCENE) {

        } else if (gameState == GameState.GAME) {

        } else if (newGameState == GameState.GAMEWIN) {

        } else if (newGameState == GameState.GAMEOVER) {

        }

        //CHANGE
        gameState = newGameState;

        //AFTER CHANGE
        if (newGameState == GameState.PAUSEMENU) {
            HideAllScreens();
            pauseScreen.SetActive(true);
            settingsRef.enabled = false;
            MouseController.UnlockMouse();
        } else if (newGameState == GameState.SETTINGMENU) {
            HideAllScreens();
            settingsUI.SetActive(true);
            settingsRef.enabled = true;
            MouseController.UnlockMouse();
        } else if (newGameState == GameState.CUTSCENE) {

        } else if (newGameState == GameState.GAME) {
            HideAllScreens();
            settingsRef.enabled = false;
            MouseController.LockMouse();
        } else if (newGameState == GameState.GAMEWIN) {
            HideAllScreens();
            gameWinScreen.SetActive(true);
            MouseController.UnlockMouse();
            pickUpControllerRef.DropItem(true);
        } else if (newGameState == GameState.GAMEOVER) {
            HideAllScreens();
            gameOverScreen.SetActive(true);
            MouseController.UnlockMouse();
            pickUpControllerRef.DropItem(true);
        }
    }

    private void HideAllScreens() {
        pauseScreen.SetActive(false);
        settingsUI.SetActive(false);
        gameWinScreen.SetActive(false);
        gameOverScreen.SetActive(false);
    }

    //Pause functions
    public void PauseGame() {
        ChangeGameState(GameState.PAUSEMENU);
        AudioManager.instance.PauseAll();
        itemManagerRef.PauseAll();
    }

    public void UnpauseGame() {
        ChangeGameState(GameState.GAME);
        AudioManager.instance.Play("gameBackground");
        itemManagerRef.UnpauseAll();
    }

    //Resume Button
    public void ResumeGame() {
        UnpauseGame();
    }

    //Settings Button
    public void OpenSettings() {
        ChangeGameState(GameState.SETTINGMENU);
    }

    //temporary button to return to pause menu for testing
    public void CloseSettings() {
        ChangeGameState(GameState.PAUSEMENU);
    }

    //Exit Button
    public void Exit() {
        AudioManager.instance.Play("homeTheme");
        SceneController.SwitchToStartScene();
    }

    private void GameWon() {
        ChangeGameState(GameState.GAMEWIN);
    }

    public void GameOver() {
        ChangeGameState(GameState.GAMEOVER);
    }

    public void SaveData() {
        playerControllerRef.SavePlayer();
    }

    public void LoadData() {
        if (playerControllerRef != null) {
            playerControllerRef.LoadPlayer();

            if (!SaveSystem.IsSaved()) {
                SaveSystem.Save(playerControllerRef.transform);
                SaveSystem.LoadSettings();
                Debug.Log(SaveSystem.GetTimer());
                GameTimer.SetTimer(SaveSystem.GetTimer());
            }
        }
    }
}

//Game scene states
public enum GameState {
    PAUSEMENU,
    SETTINGMENU,
    CUTSCENE,
    GAME,
    GAMEWIN,
    GAMEOVER
}