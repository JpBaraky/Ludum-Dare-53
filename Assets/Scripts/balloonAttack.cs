using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balloonAttack : MonoBehaviour
{
    public GameObject[] balloonPrefab;
    public float balloonReleaseInterval;
    public Transform handPosition;

    private float timeSinceLastBalloon;
    // Start is called before the first frame update
    void Start()
    {
        timeSinceLastBalloon = balloonReleaseInterval;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timeSinceLastBalloon += Time.deltaTime;

        if(timeSinceLastBalloon >= balloonReleaseInterval) {
            if(balloonPrefab.Length > 0) {
                int rand = Random.Range(0,balloonPrefab.Length);
                Instantiate(balloonPrefab[rand],handPosition.position,Quaternion.identity);
            }
            timeSinceLastBalloon = 0;
        }
    }
}
