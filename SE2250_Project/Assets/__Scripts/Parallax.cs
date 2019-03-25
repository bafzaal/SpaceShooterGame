using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public GameObject playerShip; //The Player ship
    public GameObject[] panels; //The scrolling foregrounds
    public float scrollSpeed = -30f; //How fast it scrolls
    public float motionMult = 0.25f; //How much panels react to player movement
    private float panel_Height; //Height of the panel
    private float depth; //Depth of Panels

    
    // Start is called before the first frame update
    void Start()
    {
        panel_Height = panels[0].transform.localScale.y;
        depth = panels[0].transform.position.z;
        panels[0].transform.position = new Vector3(0, 0, depth);
        panels[1].transform.position = new Vector3(0, panel_Height, depth);
    }

    // Update is called once per frame
    void Update()
    {
        float timeY, timeX = 0;
        timeY = Time.time * scrollSpeed % panel_Height + (panel_Height * 0.5f);

        if(playerShip != null)
        {
            timeX = -playerShip.transform.position.x * motionMult;
        }
        panels[0].transform.position = new Vector3(timeX, timeY, depth);
        if(timeY >= 0)
        {
            panels[1].transform.position = new Vector3(timeX, timeY - panel_Height, depth);
        }
        else
        {
            panels[1].transform.position = new Vector3(timeX, timeY + panel_Height, depth);
        }
    }
}
