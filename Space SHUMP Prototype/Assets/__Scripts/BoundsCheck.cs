using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsCheck : MonoBehaviour
{
    /// <summary>
    /// Keeps a GameObject on screen.
    /// Note that this ONLY works for an orthographic Main Camera at [0,0,0].
    /// </summary>
    [Header("Set in Inspector")]
    public float radius = 1f;
    public bool keepOnScreen = true;

    [Header("Set Dynamically")]
    public bool isOnScreen = true;
    public float camWidth;
    public float camHeight;


    [HideInInspector]
    public bool offRight, offLeft, offUp, offDown;


    void Awake() { 

        camHeight = Camera.main.orthographicSize; /*accesses the MainCamera tag,
                                                   makes camHeight the distance from 
                                                   the origin of the world*/
      
        camWidth = camHeight * Camera.main.aspect; /*computes distance from origin
                                                    to edge of screen */   
    }

    void LateUpdate() /*(race condition)-called after ever frame after Update() 
                        to ensure the ship moves to a new position each frame 
                        before Hero is bounded to screen */       
    {
        Vector3 pos = transform.position;
        isOnScreen = true;
        offRight = offLeft = offUp = offDown = false;

        if (pos.x > camWidth - radius)
        {
            pos.x = camWidth - radius;
            offRight = true;
        }
        if (pos.x< -camWidth + radius)
        {
            pos.x = -camWidth + radius;
            offLeft = true;
        }
        if (pos.y > camHeight - radius)
        {
            pos.y = camHeight - radius;
            offUp = true;
        }
        if (pos.y < -camHeight + radius)
        {
            pos.y = -camHeight + radius;
            offDown = true;
        }

       isOnScreen = !(offRight || offLeft || offUp || offDown);

        if (keepOnScreen && !isOnScreen)
        {
            transform.position = pos;
            isOnScreen = true;
            offRight = offLeft = offUp = offDown = false;
        }

        transform.position = pos;
    }

    //Draw the bounds in the Scene pane using OnDrawGizmos()
   void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        Vector3 boundSize = new Vector3(camWidth * 2, camHeight * 2, 0.1f);
        Gizmos.DrawWireCube(Vector3.zero, boundSize);
    }

}
