using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballonScript : MonoBehaviour
{
    public float risingSpeed = 1f;
    

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * risingSpeed);
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("BalloonKiller")){
            Destroy(this.gameObject);
        }
    }
}
