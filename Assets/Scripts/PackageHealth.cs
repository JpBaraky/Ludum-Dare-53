using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageHealth: MonoBehaviour {

    public int currentBoxHealth;
    public Sprite[] boxStages;
    private SpriteRenderer boxSpriteRenderer;
    public bool isPlayerBox;
    private Rigidbody2D boxRigidBody;
    public BoxCollider2D boxColliderPickUp;
    public BoxCollider2D bigCollider;
    public BoxCollider2D smallCollider;
    public float minHeight = 2;
    private Vector2 initialPosition;
    public bool isDelivered;

    // Start is called before the first frame update
    void Start() {
        initialPosition = GameObject.Find("PackageParent").transform.position;
        if(!isPlayerBox) {

            boxRigidBody = GetComponent<Rigidbody2D>();
        }
        boxSpriteRenderer = GetComponent<SpriteRenderer>();


    }

    // Update is called once per frame
    void Update() {
        initialPosition = GameObject.Find("PackageParent").transform.position;
        switch(currentBoxHealth) {
            case 3:
            boxSpriteRenderer.sprite = boxStages[0];
            break;
            case 2:
            boxSpriteRenderer.sprite = boxStages[1];
            break;
            case 1:
            boxSpriteRenderer.sprite = boxStages[2];
            break;
            case 0:
            boxSpriteRenderer.sprite = boxStages[3];

            if(!isPlayerBox) {
                boxColliderPickUp.enabled = false;
                bigCollider.enabled = false;
                smallCollider.enabled = true;
                Invoke("Destroy",3f);


            }
            break;

        }
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Ground") {

            CheckHeight();
            
        }
    }
    void CheckHeight() {
        float dropHeight = initialPosition.y - transform.position.y;
       
        if(dropHeight >= minHeight) {
            currentBoxHealth -= 1;
        }
    }
    void Destroy() {
        gameObject.SetActive(false);
    }
    void DeliveryPackage() {
        isDelivered= true;
    }
}
