using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

//static class to controll saving player state
public static class SaveSystem {

    /*
        To save the players state:
         1. First get reference to the players transform, and the timer.
         2. call SaveSystem.save(transform, timer);
    */
    public static void Save(Transform player) {
 
        //settup saving
        string path = Application.persistentDataPath + "/data.abc";
        BinaryFormatter formatter = new BinaryFormatter();

        //settup writing stream
        FileStream stream = new FileStream(path, FileMode.Create);
        PlayerData data = new PlayerData(player);

        //save data
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void Save(PlayerData player) {

        //settup saving
        string path = Application.persistentDataPath + "/data.abc";
        BinaryFormatter formatter = new BinaryFormatter();

        //settup writing stream
        FileStream stream = new FileStream(path, FileMode.Create);

        //save data
        formatter.Serialize(stream, player);
        stream.Close();
    }

    //only update the save settings by calling: SaveSystem.saveSettings();
    public static void SaveSettings() {

        //get path
        string path = Application.persistentDataPath + "/data.abc";
        BinaryFormatter formatter = new BinaryFormatter();

        if (!IsSaved()) {
            Save(new PlayerData());
        }

        //save to stream
        PlayerData data = Load();
        FileStream stream = new FileStream(path, FileMode.Create);

        data.invertX = PlayerController.mouseXInverted;
        data.invertY = PlayerController.mouseYInverted;

        data.volume = AudioManager.volumeSetting;
        data.sensitivity = PlayerController.mouseXSensitivity;

        data.runKey = (int)KeyboardController.runKey;
        data.jumpKey = (int)KeyboardController.jumpKey;
        data.crouchKey = (int)KeyboardController.crouchKey;
        data.itemPickUpKey = (int)KeyboardController.itemPickUpKey;
        data.itemRotateLeftKey = (int)KeyboardController.itemRotateLeftKey;
        data.itemRotateRightKey = (int)KeyboardController.itemRotateRightKey;
        data.pauseKey = (int)KeyboardController.pauseKey;

        //finilize save
        formatter.Serialize(stream, data);
        stream.Close();
    }

    //clears all data by saving null to the location
    public static void ClearSave() {

        //get path
        string path = Application.persistentDataPath + "/data.abc";
        BinaryFormatter formatter = new BinaryFormatter();

        //write null using stream
        FileStream stream = new FileStream(path, FileMode.Create);
        PlayerData data = new PlayerData(null);

        //finalize
        formatter.Serialize(stream, data);
        stream.Close();
    }

    //method to get all data
    public static PlayerData Load() {
        string path = Application.persistentDataPath + "/data.abc";

        //check if the file exists
        if (File.Exists(path)) {

            //get the stream of data
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data;

            //if not empty
            if (stream.Length != 0) {
                data = formatter.Deserialize(stream) as PlayerData;
            } else {
                data = new PlayerData();
            }

            stream.Close();

            return data;
        } else {
            return null;
        }
    }

    //to get player location in aa Vector3 call: SaveSystem.getPlayerLocation();
    public static Vector3 GetPlayerLocation() {
        PlayerData pd = Load();
        return new Vector3(pd.position[0], pd.position[1], pd.position[2]);
    }

    //to get the timer as an float, call: SaveSystem.getTimer();
    public static float GetTimer() {
        PlayerData pd = Load();
        return pd.timer;
    }

    //to automatically update the keybinds call: SaveSystem.loadSettings();
    public static void LoadSettings() {
        PlayerData pd = Load();

        PlayerController.mouseXInverted = pd.invertX;
        PlayerController.mouseYInverted = pd.invertY;

        AudioManager.volumeSetting = pd.volume;
        PlayerController.mouseXSensitivity = pd.sensitivity;
        PlayerController.mouseYSensitivity = pd.sensitivity;

        KeyboardController.runKey = (KeyCode)pd.runKey;
        KeyboardController.jumpKey = (KeyCode)pd.jumpKey;
        KeyboardController.crouchKey = (KeyCode)pd.crouchKey;
        KeyboardController.itemPickUpKey = (KeyCode)pd.itemPickUpKey;
        KeyboardController.itemRotateLeftKey = (KeyCode)pd.itemRotateLeftKey;
        KeyboardController.itemRotateRightKey = (KeyCode)pd.itemRotateRightKey;
        KeyboardController.pauseKey = (KeyCode)pd.pauseKey;
    }

    //to check if there is a current save call: SaveSystem.isSaved();
    public static bool IsSaved() {
        PlayerData pd = Load();
        return (pd != null);
    }

    public static bool IsContinue() {
        PlayerData pd = Load();
        return (pd != null && pd.position[0] != 0.0f && pd.position[1] != 0.0f && pd.position[2] != 0.0f);
    }
}