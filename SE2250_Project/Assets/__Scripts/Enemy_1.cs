using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : Enemy
{
    private int _randomDirection; // Private integer called _randomDirection will help the enemy move in a random direction

    void Start()
    {
        _randomDirection = Random.Range(0, 2); // _randomDirection is randomly initialized to a number using the Random function in C#
    }

    public override void Move() // This Move function overrides the one that is in the Enemy class since it was a virtual function
    {
        Vector3 tempPos = pos; // Temporary function called tempPos is set to pos
        tempPos.y -= Mathf.Sin(45f) * (speed * Time.deltaTime); // tempPos.y is altered based on the speed and time

        if (_randomDirection == 0) // If the _randomDirection is zero
        {
            tempPos.x += Mathf.Cos(45f) * (speed * Time.deltaTime); // tempPos.x is altered based on the speed and time and the enemy moves to the right
        }
        else
        {
            tempPos.x -= Mathf.Cos(45f) * (speed * Time.deltaTime); // tempPos.x is altered based on the speed and time and the entmy moves to the left
        }
        if (tempPos.x >= 27f) // If the pos reaches the very right side of the pane
            _randomDirection = 1; // _randomDirection is set to 1 and then the enemy moves in the left direction 
        if (tempPos.x <= -27) // If the pos reaches the very left side of the pane
            _randomDirection = 0; // _randomDirection is set to 0 and then the enemy moves in the right direction 

        pos = tempPos; // pos is set to the tempPos vector that holds the new positions 
    }

 }

