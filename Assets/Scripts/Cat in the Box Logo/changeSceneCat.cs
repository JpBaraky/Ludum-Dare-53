using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class changeSceneCat : MonoBehaviour
{
    public string targetScene;
    public bool isChangeScene;
    public float secondsToWait;

     
    private fadeBackgroundCat fadeBackgroundCat;
    
    // Start is called before the first frame update
    void Start()
    {
       fadeBackgroundCat = FindObjectOfType(typeof(fadeBackgroundCat)) as fadeBackgroundCat;
    }

    // Update is called once per frame
    void Update()
    {
        if(catOutOfBox.catIsOut) {
            catOutOfBox.catIsOut = false;
         StartCoroutine("changeSceneFadeOut");
        }
        
    }
     IEnumerator changeSceneFadeOut() {
        yield return new WaitForSecondsRealtime(secondsToWait);
        fadeBackgroundCat.fadeIn();
        yield return new WaitWhile(() => fadeBackgroundCat.fume.color.a < 0.9f);

        yield return new WaitForEndOfFrame();        
         SceneManager.LoadScene(targetScene);
    }
       public void ChangeScene(string sceneToLoad){
        targetScene = sceneToLoad;
        StartCoroutine("changeSceneFadeOut");       
        
        }
    
}