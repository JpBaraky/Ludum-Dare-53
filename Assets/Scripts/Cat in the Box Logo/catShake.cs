using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catShake : MonoBehaviour
{


public float duration;

public float counter = 0f;
public float waitBeforeShake = 1;
public Animator BoxAnimator;
public AudioSource CatSound;
public AudioClip[] audios;




void Start()
{    
    StartCoroutine("WaitAndShake");
}
IEnumerator WaitAndShake(){
    yield return new WaitForSeconds(waitBeforeShake);
    PlayCatSound(0);
    //shakeGameObject(GameObjectToShake, duration, decreasePoint, false);
    yield return new WaitForSeconds(duration + 0.1f);
    BoxAnimator.SetTrigger("Shook");
    
}
void PlayCatSound(int wichSound){
  CatSound.PlayOneShot(audios[wichSound]);
}
}
