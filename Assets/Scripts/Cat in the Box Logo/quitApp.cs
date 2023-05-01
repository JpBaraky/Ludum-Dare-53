using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quitApp : MonoBehaviour
{
    private bool isPaused = false;
    public AudioSource pauseSound;

    // Start is called before the first frame update
   
    

    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if(isPaused) {
                pauseSound.Play();
                ResumeGame();
            } else {
                pauseSound.Play();
                Pause();
            }
        }
    }

    void Pause() {
        Time.timeScale = 0;
        isPaused = true;
    }

    void ResumeGame() {
        Time.timeScale = 1;
        isPaused = false;
    }
}

