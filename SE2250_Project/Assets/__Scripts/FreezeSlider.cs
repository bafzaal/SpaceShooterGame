using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FreezeSlider : MonoBehaviour
{
    private float _bWasPressedTime = 0;
    private bool _bWasPressed = false;
    private bool _playAudio = true;
    public Image Fill;
    public AudioClip freezingClip;
    public AudioClip freezeReady;
    static public Slider slide;

    // Start is called before the first frame update
    void Start()
    {
        slide = GameObject.Find("Slider").GetComponent<Slider>();
        slide.maxValue = 100f;
        slide.minValue = 0f;
        slide.value = 0;
    }

    // Update is called once per frame
    void Update()
    {

        slide.value += 0.04f;


        if (slide.value >= 100f)
        {
            Fill.color = Color.blue;

            if (_playAudio)
            {
                AudioSource.PlayClipAtPoint(freezeReady, new Vector3(5, 1, 2));
                _playAudio = false;
            }

        }

        if (Input.GetKeyDown(KeyCode.B) && slide.value >= 100f)
        {
            AudioSource.PlayClipAtPoint(freezingClip, new Vector3(5, 1, 2));
            _bWasPressed = true;
        }

        if (_bWasPressed)
        {
            _bWasPressedTime += Time.deltaTime;
            if (_bWasPressedTime > 0.1f)
            {
                _bWasPressedTime = 0f;
                _bWasPressed = false;
                _playAudio = true;
                slide.value = 0f;
            }

        }


        if (slide.value < 100)
            Fill.color = Color.white;
    }
}
