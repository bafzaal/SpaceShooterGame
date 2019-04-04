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
    // Start is called before the first frame update
    public GameObject progressBar;
    public Slider slider;
    private float tTime = 0;
    public static float SLIDER_VAL = 0;
    private float bWasPressedTime = 0;
    private bool bWasPressed = false;
    public Image Fill;
    public AudioClip freezingClip;
    public AudioClip freezeReady;


    void Start()
    {
        HIGH_SCORE = PlayerPrefs.GetInt("HighScore", 0); //  //Fetch the score from the PlayerPrefs (set these Playerprefs in another script). If no Int of this name exists, the default is 0.
        scoreText.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(2, 1.4f, 0)); //adjusts score when screen size is changed
        highScoreText.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(2, 1, 0)); //adjusts highscore when screen size is changed
    }   

    void FixedUpdate()
    {
        scoreText.text = "Score: " + CURR_SCORE; // Updating the UI text for the score
         highScoreText.text = "High Score: " + HIGH_SCORE; // Updating the UI text for the high score
        if (CURR_SCORE > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", CURR_SCORE); //Give the PlayerPrefs some values to send over to the next Scene

        }
        scoreText.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(2, 1.4f,0)); //adjusts score when screen size is changed
        highScoreText.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(2, 1, 0)); //adjusts highscore when screen size is change
        SliderChanger();
        SLIDER_VAL = slider.value;
        if(slider.value == 1)
        {
            Fill.color = Color.blue; 
        }
        if (slider.value != 1)
        {
            Fill.color = Color.white;
        }

    }
    void SliderChanger()
    {
        tTime += Time.deltaTime;

        if ( tTime > 3.0f)
        {
            tTime = 0f;
            slider.value += 0.1f;
        }
        if (SLIDER_VAL == 1)
        {
            AudioSource.PlayClipAtPoint(freezeReady, new Vector3(5, 1, 2));
        }
        if (Input.GetKeyDown(KeyCode.B) && SLIDER_VAL == 1)
        {
            AudioSource.PlayClipAtPoint(freezingClip, new Vector3(5, 1, 2));
            bWasPressed = true;
        }
        if (bWasPressed)
        {
            bWasPressedTime += Time.deltaTime;
            if (bWasPressedTime > 0.1f)
            {
                bWasPressedTime = 0f;
                slider.value = 0;
                bWasPressed = false;
            }
        }

    }
}
