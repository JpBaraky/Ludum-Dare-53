using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectPackage : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject playerPackage;
    private playerScript playerScript;
    public PackageHealth health;
    void Start()
    {
       
        playerScript = FindObjectOfType(typeof(playerScript)) as playerScript;
        playerPackage = GameObject.Find("PackageParent");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ColectPackage() {
        playerPackage.transform.GetChild(0).gameObject.SetActive(true);
        playerScript.hasPackage = true;
        playerScript.currentBoxHealth = health.currentBoxHealth;
        Destroy(transform.parent.gameObject);
        
    }
    public void DeliveryPackage() {
        Debug.Log("You Won!!");
    }
}
