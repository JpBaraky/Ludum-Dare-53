using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class titleScreen: MonoBehaviour {
    public GameObject drone;
    public float hoverAmplitude = 0.5f;
    public float hoverSpeed = 1f;
    public TextMeshProUGUI pressStartText;
    public float blinkInterval = 0.5f;

    private Vector3 startPos;
    private float timeElapsed = 0f;
    private bool textVisible = true;

    private changeScene changeScene;
    public AudioSource sceneSound;

    void Start() {
        startPos = drone.transform.position;
        changeScene = GetComponent<changeScene>();
    }

    void Update() {
        // Hover the drone up and down
        Vector3 newPos = startPos;
        newPos.y += Mathf.Sin(Time.time * hoverSpeed) * hoverAmplitude;
        drone.transform.position = newPos;

        // Blink the "Press Start" text
        timeElapsed += Time.deltaTime;
        if(timeElapsed >= blinkInterval) {
            timeElapsed = 0f;
            textVisible = !textVisible;
            pressStartText.enabled = textVisible;
        }

        if(Input.anyKey) {
            sceneSound.Play();
            changeScene.isChangeScene= true;
        }
    }

}
