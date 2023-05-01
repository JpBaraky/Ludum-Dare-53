using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class playerEvaluator: MonoBehaviour {
    public int maxStars = 5;
    public int minPackagesForOneStar = 5;
    public float minAveragePackageHealthForOneStar = 2f;
    public float maxTimeForOneStar = 60f;
    public int stars;
    public List<GameObject> starIcons;
    
    public Sprite[] starImage;

    public int Evaluate(List<PackageHealth> packages,float totalTime) {
        // Calculate the number of packages delivered
        int deliveredCount = 0;
        foreach(PackageHealth package in packages) {
            if(package.isDelivered) {
                deliveredCount++;
            }
        }

        // Calculate the average health of the packages
        float totalHealth = 0f;
        foreach(PackageHealth package in packages) {
            totalHealth += package.currentBoxHealth;
        }
        float averageHealth = totalHealth / packages.Count;

        // Calculate the number of stars
        stars = maxStars;
        if(deliveredCount < minPackagesForOneStar) {
            stars--;
        }
        if(averageHealth < minAveragePackageHealthForOneStar) {
            stars--;
        }
        if(totalTime > maxTimeForOneStar) {
            stars--;
        }

        // Turn on/off stars based on score
        for(int i = 0; i < starIcons.Count; i++) {
            
            if(i < stars) {
                starIcons[i].GetComponent<Image>().sprite = starImage[0];
              
            } else {
                starIcons[i].GetComponent<Image>().sprite = starImage[1];
                
            }
        }

        // Return the number of stars
        return stars;
    }
}

