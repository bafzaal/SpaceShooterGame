using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : Enemy
{
    protected float enemyTwoTime = 0;
    private bool _turnedTwoBlue = false;
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
        if (Input.GetKeyDown(KeyCode.B) && FreezeSlider.slide.value >= 100f)
        {
            _turnedTwoBlue = true;
            speed = 0;
            foreach (Material m in materials) // For every m in "materials" the following happens
            {
                m.color = Color.blue; // The color is set to white to show the damage
            }

        }
    }
    public override void UnShowDamage()
    {
        if (_turnedTwoBlue)
        {
            for (int i = 0; i < materials.Length; i++) // loop continues from i = 0 until it reaches the size of "materials"
            {
                materials[i].color = Color.blue; // material color is set to the color that is in originalColors array
            }
        }
        else if (_turnedTwoBlue == false)
        {
            for (int i = 0; i < materials.Length; i++) // loop continues from i = 0 until it reaches the size of "materials"
            {
                materials[i].color = originalColors[i]; // material color is set to the color that is in originalColors array
            }
        }
        showingDamage = false; // showing damage is set to false. 
    }

}

