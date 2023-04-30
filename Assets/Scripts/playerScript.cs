using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript: MonoBehaviour {
    private Rigidbody2D playerRb;
    private SpriteRenderer playerRenderer;
    private float h, v;//Joystic input
    public float speed;
    public GameObject healthBar;
    public bool onMouse;
    public int maxHealth;
    public int currentHealth;
    public bool hasPackage;
    public GameObject package;
    public GameObject packagePrefab;
    private bool Grounded;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public Sprite[] animations;
    public int currentBoxHealth;
    public PackageHealth PackageHealth;
    public PackageHealth tempPackageHealth;
    // Start is called before the first frame update
    void Start() {
        playerRb = GetComponent<Rigidbody2D>();
        playerRenderer = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update() {
        DroneAnimation();
        if(!Grounded) {
            h = Input.GetAxis("Horizontal");
            this.transform.rotation = Quaternion.AngleAxis(-15 * h,new Vector3(0,0,1));
        }
        v = Input.GetAxisRaw("Vertical");
        if(v > 0 ) {
            
            playerRb.velocity = new Vector2(h * Time.fixedDeltaTime * speed,v * Time.fixedDeltaTime * speed);
        }
        if(Grounded && v >0) {
           
            playerRb.velocity = new Vector2(0,v * Time.fixedDeltaTime * speed);

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
       
        
       
        Grounded = Physics2D.OverlapCircle(groundCheck.position,0.02f,whatIsGround);

        HealthBarRotation();
            Health();
        PackageHealth.currentBoxHealth = currentBoxHealth;
        }
        void HealthBarRotation() {
            if(onMouse) {

                if(Input.GetKey(KeyCode.Q)) {
                    healthBar.transform.Rotate(0,0,Time.deltaTime * speed);
                }
                if(Input.GetKey(KeyCode.E)) {
                    healthBar.transform.Rotate(0,0,Time.deltaTime * -speed);
                }
            }
        }
        void Health() {
            if(currentHealth <= 0) {
                Debug.Log("GameOver");
            }
        }
    private void OnTriggerStay2D(Collider2D collision) {
        if(Input.GetButton("Jump") && !hasPackage) {

            collision.gameObject.SendMessage("ColectPackage",SendMessageOptions.DontRequireReceiver);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Enemies" && hasPackage) {
            DropPackage();
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
        tempPackageHealth = Box.GetComponent<PackageHealth>();
        tempPackageHealth.currentBoxHealth = currentBoxHealth;

    }
}

