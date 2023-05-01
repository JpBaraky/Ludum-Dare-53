using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class randomPosition: MonoBehaviour {
    public GameObject deliveryLocation;
    public GameObject[] randomLocations;







    public void Start() {
        randomLocations = GameObject.FindGameObjectsWithTag("Random Location");
        RandomPosition();
    }



    public void RandomPosition() {
                
              
                deliveryLocation.transform.position = randomLocations[Random.Range(0,randomLocations.Length)].transform.position;   
                

    }
   

    
}


