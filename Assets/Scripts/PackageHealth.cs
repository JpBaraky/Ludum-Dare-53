using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageHealth : MonoBehaviour
{
    
    public int currentBoxHealth;
    public Sprite[] boxStages;
    private SpriteRenderer boxSpriteRenderer;
    public bool isPlayerBox;
    private Rigidbody2D boxRigidBody;
    public BoxCollider2D boxColliderPickUp;
    public BoxCollider2D bigCollider;
    public BoxCollider2D smallCollider;
    // Start is called before the first frame update
    void Start()
    {
        if(!isPlayerBox) {

            boxRigidBody = GetComponent<Rigidbody2D>();
        }
        boxSpriteRenderer= GetComponent<SpriteRenderer>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
        switch (currentBoxHealth) {
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

            }
            break;

        }
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Package") {

            if(boxRigidBody != null) {

                if(boxRigidBody.velocity.y <= 0.0000000001) {

                    currentBoxHealth -= 1;
                }
            }
        }
    }
}
