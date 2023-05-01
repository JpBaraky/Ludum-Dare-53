using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class droneBatery: MonoBehaviour {
    public float maxBatteryLevel = 100f;
    public float batteryDrainRate = 1f;
    public float rechargeRate = 10f; // units per second
    public float batteryPickupAmount = 25f;
    public float batteryPickupCooldown = 5f;
    public float batteryPickupTimer = 0f;
    public float batteryLevel {
        get; private set;
    }
    public bool isBatteryDepleted {
        get; private set;
    }

    public Image batteryLevelBar;
    public bool isLandedOnRechargeStation {
        get; private set;
    }

    void Start() {
        batteryLevel = maxBatteryLevel;
        UpdateBatteryLevelBar();
    }

    void Update() {
        if(!isBatteryDepleted) {
            if(!isLandedOnRechargeStation) {
                DrainBattery();
            } else {
                RechargeBattery();
            }
        }
        if(batteryPickupTimer > 0f) {
            batteryPickupTimer -= Time.deltaTime;
        }
    }

    void DrainBattery() {
        if(playerScript.verticalInput != 0) {

            batteryLevel -= batteryDrainRate * Time.deltaTime;
        }
        if(batteryLevel <= 0f) {
            batteryLevel = 0f;
            isBatteryDepleted = true;
            Debug.Log("Battery depleted! Drone is unusable.");
        }
        UpdateBatteryLevelBar();
    }

    void RechargeBattery() {
        batteryLevel += rechargeRate * Time.deltaTime;
        if(batteryLevel >= maxBatteryLevel) {
            batteryLevel = maxBatteryLevel;
            isLandedOnRechargeStation = false;
        }
        UpdateBatteryLevelBar();
    }

    void UpdateBatteryLevelBar() {
        if(batteryLevelBar != null) {
            batteryLevelBar.fillAmount = batteryLevel / maxBatteryLevel;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("RechargeStation")) {
            isLandedOnRechargeStation = true;
        } else if(other.gameObject.CompareTag("BatteryPickup")) {
            CollectBatteryPickup(other.gameObject);
        } else if(other.gameObject.CompareTag("Enemies")) {
            DrainBatteryOnCollision();
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("RechargeStation")) {
            isLandedOnRechargeStation = false;
        }
    }

    void CollectBatteryPickup(GameObject pickupObject) {
        if(batteryPickupTimer <= 0f) {
            batteryLevel += batteryPickupAmount;
            if(batteryLevel >= maxBatteryLevel) {
                batteryLevel = maxBatteryLevel;
            }
            UpdateBatteryLevelBar();
            batteryPickupTimer = batteryPickupCooldown;
            Destroy(pickupObject);
        }
    }
    void DrainBatteryOnCollision() {
        batteryLevel -= 1f; // decrease the battery level by 1 units on collision with an enemy
        if(batteryLevel <= 0f) {
            batteryLevel = 0f;
            isBatteryDepleted = true;
            Debug.Log("Battery depleted! Drone is unusable.");
        }
        UpdateBatteryLevelBar();
    }
}