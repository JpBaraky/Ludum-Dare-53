using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript: MonoBehaviour {

    private Rigidbody2D playerRb;
    private SpriteRenderer playerRenderer;
    public static float horizontalInput, verticalInput;
    public float speed;

    [Header("Battery")]
    private droneBatery droneBatery;

    [Header("Package")]
    public bool hasPackage;
      
    public GameObject package;
    public GameObject packagePrefab;
    public int currentBoxHealth;
    public PackageHealth PackageHealth;
    private PackageHealth tempPackageHealth;

    [Header("Ground Check")]
    public bool grounded;
    public Transform groundCheck;
    public LayerMask whatIsGround;

    [Header("Animations")]
    public Sprite[] animations;

    [Header("Sound")]
    public AudioSource droneSound;
    public AudioSource hitSound;
    public AudioClip hitEffect;
    private bool droneIsOn;
    private bool hasBeenHit;

  
    // Start is called before the first frame update
    void Start() {
        droneBatery= GetComponent<droneBatery>();
        playerRb = GetComponent<Rigidbody2D>();
        playerRenderer = GetComponent<SpriteRenderer>();
      
       
    }

    // Update is called once per frame
    void Update() {
        if(droneBatery.isBatteryDepleted) {
            return;
        }
        DroneAnimation();
        if(!grounded) {
            horizontalInput = Input.GetAxis("Horizontal");
            this.transform.rotation = Quaternion.AngleAxis(-15 * horizontalInput,new Vector3(0,0,1));
        }
        verticalInput = Input.GetAxisRaw("Vertical");
        if(horizontalInput != 0 && verticalInput ==0) {
            droneIsOn = true;
            playerRb.velocity = new Vector2(horizontalInput * Time.fixedDeltaTime * speed * 1.5f ,playerRb.velocity.y);
        } else {
            if(horizontalInput != 0 && verticalInput != 0) {
                playerRb.velocity = new Vector2(horizontalInput * Time.fixedDeltaTime * speed * 1.5f ,verticalInput * Time.fixedDeltaTime * speed);
            } else {


                if(verticalInput > 0) {
                    droneIsOn = true;
                    playerRb.gravityScale = 0.2f;
                    playerRb.velocity = new Vector2(horizontalInput * Time.fixedDeltaTime * speed * 1.5f,verticalInput * Time.fixedDeltaTime * speed);
                } else {
                    if(verticalInput < 0) {
                        droneIsOn = true;
                        playerRb.velocity = new Vector2(horizontalInput * Time.fixedDeltaTime * speed * 1.5f,verticalInput * Time.fixedDeltaTime * speed);

                    } else {
                        droneIsOn = false;
                    }

                }
            }
        }
        if(grounded && verticalInput > 0) {
           
            playerRb.velocity = new Vector2(0,verticalInput * Time.fixedDeltaTime * speed);

        }
        if(hasPackage) {
            groundCheck.transform.localPosition = new Vector3(0,-1.5f,groundCheck.transform.localPosition.z);
        } else {
            groundCheck.transform.localPosition = new Vector3(0,-0.5f,groundCheck.transform.localPosition.z);
        }
        if(Input.GetButtonDown("Jump") && hasPackage){
            hasPackage = false;
            package.transform.GetChild(0).gameObject.SetActive(false);
            GameObject Box = Instantiate(packagePrefab,package.transform.position,transform.localRotation);
            tempPackageHealth = Box.GetComponent<PackageHealth>();
      
            tempPackageHealth.currentBoxHealth = currentBoxHealth;
          
        }
       
        
       
        grounded = Physics2D.OverlapCircle(groundCheck.position,0.02f,whatIsGround);

        
        PackageHealth.currentBoxHealth = currentBoxHealth;
        PlayDroneSound();
        }
        
       
    private void OnTriggerStay2D(Collider2D collision) {
        if(Input.GetButton("Jump") && !hasPackage) {

            collision.gameObject.SendMessage("ColectPackage",SendMessageOptions.DontRequireReceiver);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Enemies") {
            hitSound.PlayOneShot(hitEffect);
            if(hasPackage) {

                DropPackage();
            }
        }
        if(collision.gameObject.tag == "Randomizer") {
            collision.GetComponent<randomDelivery>().RandomPosition();
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Enemies") {
            hitSound.PlayOneShot(hitEffect);
            if(hasPackage) {

                DropPackage();
            }
        }
    }
    void DroneAnimation() {
        if(Input.GetButton("Jump")) {
            playerRenderer.sprite = animations[1];
        } else {
            playerRenderer.sprite = animations[0];
        }
            
    }
    public void DropPackage() {
        hasPackage = false;
        package.transform.GetChild(0).gameObject.SetActive(false);
        GameObject Box = Instantiate(packagePrefab,package.transform.position,transform.localRotation);
        Box.GetComponent<PackageHealth>().initialPosition = GameObject.Find("PackageParent").transform.position;
        tempPackageHealth = Box.GetComponent<PackageHealth>();
        tempPackageHealth.currentBoxHealth = currentBoxHealth;

    }
    private void PlayDroneSound() {
        if(droneIsOn) {

            droneSound.enabled = true;
        } else {
        droneSound.enabled = false;}
    }

}

