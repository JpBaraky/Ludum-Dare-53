using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomDelivery : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject deliverySpot;
    public GameObject[] randomLocations;
    void Start()
    {
        randomLocations = GameObject.FindGameObjectsWithTag("RandomLocation");
        RandomPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RandomPosition() {
        deliverySpot.transform.position = randomLocations[Random.Range(0,randomLocations.Length)].transform.position;
    }
}
