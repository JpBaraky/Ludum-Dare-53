using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gameController : MonoBehaviour {
    public droneBatery droneBatteryScript;
    public GameObject gameOverScreen;
    public GameObject gameWinScreen;
    public float gameOverDelay = 3f;

    private bool isGameOver = false;
    private float gameOverTimer = 0f;

    public TextMeshProUGUI timerText;
    private float currentTime = 0f;

    public playerEvaluator playerEvaluator;
    public enum GameMode {
        Delivery,
        Destruction
    }

    public GameMode gameMode = GameMode.Delivery;

    private void Start() {
        playerEvaluator = GetComponent<playerEvaluator>();
    }
    void Update() {
        Timer();
        bool arePackagesCompleted = false;

        if(gameMode == GameMode.Delivery) {
            arePackagesCompleted = AreAllPackagesDelivered();
        } else if(gameMode == GameMode.Destruction) {
            arePackagesCompleted = AreAllPackagesDestroyed();
        }

        if(!isGameOver && IsBatteryDepleted()) {
            gameOverTimer += Time.deltaTime;
            if(gameOverTimer >= gameOverDelay) {
                isGameOver = true;
                gameOverScreen.SetActive(true);
                Time.timeScale = 0f; // Pause the game
            }
        } else if(!isGameOver && arePackagesCompleted) {

            isGameOver = true;
            gameWinScreen.SetActive(true);
            List<PackageHealth> packageHealthList = new List<PackageHealth>();
            PackageHealth[] packageHealths = FindObjectsOfType<PackageHealth>();
            foreach(PackageHealth packageHealth in packageHealths) {
                packageHealthList.Add(packageHealth);
            }
            Debug.Log(playerEvaluator.Evaluate(packageHealthList,currentTime));
            Time.timeScale = 0f; // Pause the game
        }
    }

    bool IsBatteryDepleted() {
        return droneBatteryScript.isBatteryDepleted;
    }

    bool AreAllPackagesDelivered() {
        GameObject[] packageObjects = GameObject.FindGameObjectsWithTag("Package");
        foreach(GameObject packageObject in packageObjects) {
            PackageHealth package = packageObject.GetComponent<PackageHealth>();
            if(!package.isDelivered) {
                return false;
            }
        }
        return true;
    }

    bool AreAllPackagesDestroyed() {
        GameObject[] packageObjects = GameObject.FindGameObjectsWithTag("Package");
        foreach(GameObject packageObject in packageObjects) {
            if(packageObject.activeInHierarchy) {
                return false;
            }
        }
        return true;
    }
    void Timer() {
        currentTime += Time.deltaTime;
        timerText.text = "TIME: " + currentTime.ToString("F2");
    }
}
