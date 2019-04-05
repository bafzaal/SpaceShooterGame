using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    public Text scoreText; 
    public Text highScoreText;
    public static int CURR_SCORE = 0; // Keeping track of the current round's score
    public static int HIGH_SCORE; // Keeping track of high score
  
    

    void Start()
    {

        HIGH_SCORE = PlayerPrefs.GetInt("HighScore", 0); //  //Fetch the score from the PlayerPrefs (set these Playerprefs in another script). If no Int of this name exists, the default is 0.
        scoreText.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(2, 1.4f, 0)); //adjusts score when screen size is changed
        highScoreText.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(2, 1, 0)); //adjusts highscore when screen size is changed
    }   

    void Update()
    {
        scoreText.text = "Score: " + CURR_SCORE; // Updating the UI text for the score
         highScoreText.text = "High Score: " + HIGH_SCORE; // Updating the UI text for the high score
        if (CURR_SCORE > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", CURR_SCORE); //Give the PlayerPrefs some values to send over to the next Scene

        }
        scoreText.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(2, 1.4f,0)); //adjusts score when screen size is changed
        highScoreText.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(2, 1, 0)); //adjusts highscore when screen size is change
    
        if(CURR_SCORE >= 1500 && Main.LEVEL != 0)
        {
            Main.LEVEL = 2;
        }


    }
  
}
