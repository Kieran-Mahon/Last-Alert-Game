using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour {
    
    private static float timeLeft;
    
    private static GameTimer instance;

    void Start() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(this);
        }
    }

    public static void SetTimer(float time) {
        print("Time set to: " + timeLeft);
        timeLeft = time;
    }

    public static void UpdateTime() {
        timeLeft -= Time.deltaTime;
    }

    public static float GetTimeLeft() {
        return timeLeft;
    }

    public static string TimeLeftString() {
        //If more than 0 then display the formatted time
        if (timeLeft > 0) {
            TimeSpan ts = TimeSpan.FromSeconds(timeLeft);
            return ts.Minutes + ":" + string.Format("{0:00}", ts.Seconds);
        } else {
            //Equal or less than display 0 time
            return "0:00";
        }
    }
}