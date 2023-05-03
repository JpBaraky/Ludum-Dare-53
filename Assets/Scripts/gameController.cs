using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gameController : MonoBehaviour {
    public droneBatery droneBatteryScript;
    public GameObject gameOverScreen;
    public GameObject gameWinScreen;
    public playerScript playerScript;
    public float gameOverDelay = 3f;

    private bool isGameOver = false;
    private float gameOverTimer = 0f;

    public TextMeshProUGUI timerText;
    private float currentTime = 0f;

    public playerEvaluator playerEvaluator;
    private changeScene changeScene;
    
    public enum GameMode {
        Delivery,
        Destruction
    }

    public GameMode gameMode = GameMode.Delivery;

    private void Start() {
        playerEvaluator = GetComponent<playerEvaluator>();
        changeScene= GetComponent<changeScene>();
        Application.targetFrameRate = (int)Screen.currentResolution.refreshRate;
        PlayerPrefs.DeleteAll();
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
                changeScene.isChangeScene = true;
                
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
            if(playerEvaluator.Evaluate(packageHealthList,currentTime) == 5) {

                gameWinScreen.GetComponent<AudioSource>().Play();
            }
            
            changeScene.isChangeScene = true;
            //Time.timeScale = 0f; // Pause the game

        }
    }

    bool IsBatteryDepleted() {
        return droneBatteryScript.isBatteryDepleted;
    }

    bool AreAllPackagesDelivered() {
        if(playerScript.hasPackage) {
            return false;
        }
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
        if(playerScript.hasPackage) {
            return false;
        }
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
