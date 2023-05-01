using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public enum enemyState {
    PASSIVE,
    PATROLING,
    ALERT,
    COMBAT,
    STOP,
    DYING,

}

public class enemyController: MonoBehaviour {
    
    public Collider2D enemyCollider;
    [Header("Animation System")]
    private Animator enemyAnimator;
    public enemyState currentState;
   
    private GameObject alertSign;
    
    [Header("Movement System")]
    private Vector3 enemyDirection = Vector3.right;
    private bool endOfPatrol;
    Rigidbody2D enemyRigidBody2D;
    public bool lookRight = true;
    public float EnemySpeed;
    private int idAnimation;
    public float waitToTurn = 0.5f;
    public bool beenHit;
    public bool isOldMan;
    public float rockX, rockY;
    private bool stoneThrown = false;
    public Transform handPosition;
    public GameObject rockObject;


    [Header("RayCast System")]
    public LayerMask patrolTiles;
    public GameObject enemyEyes;
    public GameObject enemyEyesBellow;
    public float rayCastPatrolLength = 0.15f;
    public float rayCastAttackLength = 0.5f;
    public LayerMask whatIsPlayer;
    [Header("Damage System")]
    
    private bool interactableRayCast = true;
   





    // Use this for initialization
    public void Awake() {

        enemyCollider = GetComponent<Collider2D>();
        enemyRigidBody2D = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();      
        whatIsPlayer = LayerMask.GetMask("Player");
        

        alertSign = this.transform.Find("ExclamationSign").gameObject;
    }


    // Update is called once per frame
    public void Update() {

        if(currentState == enemyState.PATROLING ) {
            PatrolWalk();
            StartCoroutine(PatrolTurn());
        }
      
        RayCastInteraction();
        //Animation();
       

    }

    void Flip() {
        lookRight = !lookRight; //invers�o de valor booleano.
        enemyDirection.x = transform.localScale.x;
        enemyDirection.x *= -1; //inverter sinal int ou float;
        transform.localScale = new Vector3(enemyDirection.x,transform.localScale.y,transform.localScale.z); //passa valor do X novo para o scale.
        
    }
    void RayCastInteraction() {
        if(currentState == enemyState.PATROLING) {

            if(interactableRayCast) {

                RaycastHit2D rayCastPatrol = Physics2D.Raycast(enemyEyes.transform.position,enemyDirection,-rayCastPatrolLength,patrolTiles);

                RaycastHit2D rayCastPatrolBellow = Physics2D.Raycast(enemyEyesBellow.transform.position,enemyDirection,-rayCastPatrolLength,patrolTiles);


                if(rayCastPatrol || rayCastPatrolBellow) {
                    endOfPatrol = true;
                    StartCoroutine(PatrolTurn());
                    interactableRayCast = false;
                }
            }
        }
        if(isOldMan) {
            OldManStoner();
        }
           

        
    }
    void PatrolWalk() {
       
            enemyRigidBody2D.velocity = (Vector2.left * EnemySpeed);
        
    }
    IEnumerator PatrolTurn() {
                
        if(endOfPatrol) {
           endOfPatrol = false;
            float tempSpeed = EnemySpeed;
            EnemySpeed = 0;
            Flip();
            yield return new WaitForSeconds(waitToTurn);
            interactableRayCast = true;
            EnemySpeed = tempSpeed;
            EnemySpeed = -EnemySpeed;
         
        }
            
    }
    void Animation() {

      
        enemyAnimator.SetInteger("idAnimation",idAnimation);
        if(enemyRigidBody2D.velocity != Vector2.zero) {
            idAnimation = 1;
        } else {
            idAnimation = 0;
        }
       
            
            
        }
    void OldManStoner() {
        if(lookRight) {
            Vector3 tilted = Quaternion.Euler(0,0,-45) * Vector3.up;
            RaycastHit2D rayCastAttack = Physics2D.Raycast(enemyEyes.transform.position,tilted,-rayCastAttackLength,whatIsPlayer);
            Debug.DrawRay(enemyEyes.transform.position,tilted * -rayCastAttackLength,Color.red);
            if(rayCastAttack) {
                if(!stoneThrown) {
                    stoneThrown = true;
                    Debug.Log("I see you");
                    StartCoroutine(ThrowStone());
                }
            }
        } else {
            Vector3 tilted = Quaternion.Euler(0,0,45) * Vector3.up;
            RaycastHit2D rayCastAttack = Physics2D.Raycast(enemyEyes.transform.position,tilted,-rayCastAttackLength,whatIsPlayer);
            Debug.DrawRay(enemyEyes.transform.position,tilted * -rayCastAttackLength,Color.red);
            if(rayCastAttack) {
                if(!stoneThrown) {
                    stoneThrown = true;                    
                    Debug.Log("I see you");
                    StartCoroutine(ThrowStone());
                }
            }
        }
       

    }
    IEnumerator ThrowStone() {
        if(stoneThrown) {
            float tempSpeed = EnemySpeed;
            EnemySpeed = 0;
            yield return new WaitForSeconds(0.5f);
            GameObject RockTemp = Instantiate(rockObject,handPosition.transform.position,transform.localRotation);
           

            RockTemp.GetComponent<Rigidbody2D>().AddForce(new Vector2(this.transform.localScale.x * rockX,rockY + 100));
            RockTemp.GetComponent<Rigidbody2D>().AddTorque(45,ForceMode2D.Impulse);
            stoneThrown = false;
            
            EnemySpeed = tempSpeed;
            
        }
    }
    

    }
    
    

