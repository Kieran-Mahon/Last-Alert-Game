using UnityEngine.Audio;
using System;
using UnityEngine;

/*
To add a sound:
1. Add it to the AudioManager game object found in the scene using the inspector window.
2. Configure the sound you have added.

To play a sound either:
1. Set the sound's playOnAwake to true 
2. Use this line 
    FindObjectOfType<AudioManager>().Play("name");
*/


//uses singleton pattern
public class AudioManager : MonoBehaviour {
    //singleton instance
    public static AudioManager instance;

    //array of sounds
    public Sound[] sounds;

    public static float volumeSetting = 0.5f;
    
    // Awake is called before the first frame update
    void Awake() {

        //ensure only one game object exists
        if (instance == null) {
            instance = this;
        } else {
            print("Destroying extra AudioManager");
            Destroy(gameObject);
        }

        //allows for music to flawlessly transition accross scenes
        DontDestroyOnLoad(gameObject);

        //setup sounds
        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

            //plays sound if playOnAwake is true
            if (s.playOnAwake) {
                Play(s.name);
            }
        }
    }

    public void UpdateVolume() {
        foreach (Sound s in sounds) {
            s.source.volume = s.volume * volumeSetting;
        }
    }

    //function to find a sound by name and then play it
    public void Play(string name) {
        //search for sound
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            //no sound by this name is found
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        //play sound
        s.source.Play();
    }

    public void Pause(string name) {
        //search for sound
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            //no sound by this name is found
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        //play sound
        s.source.Pause();
    }

    public void PauseAll() {
        foreach (Sound s in sounds) {
            s.source.Pause();
        }
    }
}
