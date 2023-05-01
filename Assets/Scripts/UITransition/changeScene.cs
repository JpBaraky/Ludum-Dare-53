using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class changeScene : MonoBehaviour
{
    public string targetScene;
    public bool isChangeScene;
    public float secondsToWait;
    private fadeBackground fadeBackground;
    
    // Start is called before the first frame update
    void Start()
    {
       fadeBackground = FindObjectOfType(typeof(fadeBackground)) as fadeBackground;
        if(isChangeScene) {
            StartCoroutine("changeSceneFadeOut");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(catOutOfBox.catIsOut) {
            catOutOfBox.catIsOut = false;
         StartCoroutine("changeSceneFadeOut");
        }
        if(isChangeScene) {
            isChangeScene = false;
         StartCoroutine("changeSceneFadeOut");
        }
        
    }
     IEnumerator changeSceneFadeOut() {
        yield return new WaitForSecondsRealtime(secondsToWait);
        fadeBackground.fadeIn();
        yield return new WaitWhile(() => fadeBackground.fume.color.a < 0.9f);
        yield return new WaitForEndOfFrame();        
         SceneManager.LoadScene(targetScene);
    }
    
}