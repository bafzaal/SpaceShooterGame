using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsCheck : MonoBehaviour
{

    [Header("Set in Inspector")]
    public float radius = 1f;
    public bool keepOnScreen = true; //Allows to choose whether it stays on screen or not

    [Header("Set Dynamically")]
    public bool isOnScreen = true;
    public float camWidth;
    public float camHeight;

    [HideInInspector]
    public bool offRight, offLeft, offUp, offDown;

    void Awake()
    {
        camHeight = Camera.main.orthographicSize; //The height of the camera
        camWidth = camHeight * Camera.main.aspect; // The width of the camera
    }

    void LateUpdate()
    {
        Vector3 pos = transform.position;
        isOnScreen = true; // Is set to true until proven false
        offRight = offLeft = offUp = offDown = false;

        if (pos.x > camWidth - radius) // If any of the positional if statements are true, then the game object is off screen 
        {
            pos.x = camWidth - radius;
            offRight = true;
        }

        if (pos.x < -camWidth + radius) // If any of the positional if statements are true, then the game object is off screen 
        {
            pos.x = -camWidth + radius;
            offLeft = true;
        }

        if (pos.y > camHeight - radius) // If any of the positional if statements are true, then the game object is off screen 
        {
            pos.y = camHeight - radius;
            offUp = true;
        }

        if (pos.y < -camHeight + radius) // If any of the positional if statements are true, then the game object is off screen 
        {
            pos.y = -camHeight + radius;
            offDown = true;
        }

        isOnScreen = !(offRight || offLeft || offUp || offDown);

        if(keepOnScreen && !isOnScreen)
        {
            transform.position = pos;
            isOnScreen = true;
            offRight = offLeft = offUp = offDown = false;
        }

        transform.position = pos;

    }

    // Draw the bounds in the Scene pane using OnDrawGizmos()

    void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        Vector3 boundSize = new Vector3(camWidth * 2, camHeight * 2, 0.1f);
        Gizmos.DrawWireCube(Vector3.zero, boundSize);
    }

}
