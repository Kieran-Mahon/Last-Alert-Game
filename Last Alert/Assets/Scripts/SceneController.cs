using UnityEngine.SceneManagement;

public class SceneController {
    //Scene controller class which allows you to switch between scenes
    //MAKE SURE to have a scene called "GameScene" and "StartScene"
    //The functions are static so you can call them in any scene

    public static void SwitchToStartScene() {
        //Make sure not already on the start scene
        if (SceneManager.GetActiveScene().name != "StartScene") {
            //Switch to the start scene
            SceneManager.LoadScene("StartScene");
        }
    }

    public static void SwitchToGameScene() {
        //Make sure not already on the game scene
        if (SceneManager.GetActiveScene().name != "GameScene") {
            //Switch to the game scene
            SceneManager.LoadScene("GameScene");
        }
    }

    public static void SwitchToTutorialScene() {
        //Make sure not already on the game scene
        if (SceneManager.GetActiveScene().name != "TutorialScene") {
            //Switch to the game scene
            SceneManager.LoadScene("TutorialScene");
        }
    }
}