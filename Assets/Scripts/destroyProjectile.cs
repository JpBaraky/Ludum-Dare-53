using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision) {





        switch(collision.gameObject.tag) {
            case "Ground": {

                StartCoroutine("Destroy");

                break;
            }
            case "Objetos Plataforma": {

                StartCoroutine("Destroy");

                break;
            }
            case "Enemies": {
                StartCoroutine("Destroy");
                break;
            }
            case "Triggers": {
                StartCoroutine("Destroy");
                break;
            }

        }
    }
    IEnumerator Destroy() {
        
        yield return new WaitForSeconds(0.1f);

        Destroy(this.gameObject);
    }
}
