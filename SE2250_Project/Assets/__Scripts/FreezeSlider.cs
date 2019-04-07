using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FreezeSlider : MonoBehaviour
{
    private float _bWasPressedTime = 0; // provides delay when B is pressed
    private bool _bWasPressed = false; // bool to check if B was pressed
    private bool _playAudio = true; // bool to see if audio is played
    public Image Fill; // Image inside the slider
    public AudioClip freezingClip; // Audio clip that is used within the game
    public AudioClip freezeReady; // Audio clip that is used within the game
    static public Slider slide; // a Slider called slide is declared that is static

    // Start is called before the first frame update
    void Start()
    {
        slide = GameObject.Find("Slider").GetComponent<Slider>(); // slide is found in the hierarchy
        slide.maxValue = 100f; // set the max value of the slider to 100
        slide.minValue = 0f; // sets the min value of the slider to 0
        slide.value = 0; // current slide value is initialized to 0
    }

    // Update is called once per frame
    void Update()
    {
        if (slide != null)
        slide.value += 0.04f; // Slide value is increased every frame


        if (slide.value >= 100f) // if slide reaches max value
        {
            Fill.color = Color.blue; // Set slider colour to blue

            if (_playAudio)
            {
                AudioSource.PlayClipAtPoint(freezeReady, new Vector3(5, 1, 2)); // Play audio
                _playAudio = false; // set to false to prevent repeating audio
            }

        }

        if (Input.GetKeyDown(KeyCode.B) && slide.value >= 100f) // if B is pressed and slider is greater than or equal to 100
        {
            AudioSource.PlayClipAtPoint(freezingClip, new Vector3(5, 1, 2)); // plays the audio clip
            _bWasPressed = true; // B was pressed so the bool is adjusted
        }

        if (_bWasPressed)
        {
            _bWasPressedTime += Time.deltaTime; // Allows the delay to reset
            if (_bWasPressedTime > 0.1f)
            {
                // Resets all variables to original values
                _bWasPressedTime = 0f; 
                _bWasPressed = false;
                _playAudio = true;
                slide.value = 0f;
            }

        }


        if (slide.value < 100) 
            Fill.color = Color.white; // Color set to white
    }
}
