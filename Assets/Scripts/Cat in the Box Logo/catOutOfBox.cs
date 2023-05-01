using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catOutOfBox : MonoBehaviour
{
    public AudioClip[] sounds;
    public AudioSource catAudio;
    public static bool catIsOut;

        // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    public void CatIsOut(){
        catAudio.PlayOneShot(sounds[0]); 
        catIsOut = true;
           
    }
    
}
