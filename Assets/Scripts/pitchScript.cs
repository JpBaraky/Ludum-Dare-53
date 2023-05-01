using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pitchScript: MonoBehaviour {
    public AudioClip soundEffect;
    public float pitchRange = 0.3f;

    public AudioSource audioSource;
    private Rigidbody2D rb;

    private void Start() {

        rb = GetComponent<Rigidbody2D>();
       
    }

    private void FixedUpdate() {
        if(rb.velocity.y <= -0.1f) {
            if(!audioSource.isPlaying) {
                audioSource.clip = soundEffect;
                audioSource.pitch = 1f;
                audioSource.loop = true;
                audioSource.Play();
            }

            float pitch = Mathf.Clamp(Mathf.Abs(rb.velocity.y) / 10f,0f,1f) * pitchRange;
            audioSource.pitch = 1 - pitch;
        } else {
            audioSource.Stop();
        }
    }
   
}