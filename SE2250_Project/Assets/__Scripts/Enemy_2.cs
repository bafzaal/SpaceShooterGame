using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : Enemy
{
    private float _holdX = 1; // float used to ensure enemy doesnt move in x direc when frozen
    private bool _turnedThreeBlue = false; // bool to keep track of enemy being turned blue
    public override void Move() // This Move function overrides the one in the Enemy class since it was virtual
    {

        Vector3 tempPos = pos; // New vector called tempPos is set to pos
        tempPos.y -= speed * Time.deltaTime; // tempPos.y is altered based on the speed and time
        tempPos.x += Mathf.Sin(Time.time) * Time.deltaTime * 4 * _holdX; // tempPos.x is altered based on the speed and time
        pos = tempPos; // pos is set to the tempPos vector that holds the new positions

        if (Input.GetKeyDown(KeyCode.B) && FreezeSlider.slide.value >= 100f)
        {
            speed = 0; // speed is set to zero to freeze the enemy
           _holdX = 0; // _holdX is set to 0 to prevent movement in the x direction
           _turnedThreeBlue = true;
            foreach (Material m in materials) // For every m in "materials" the following happens
            {
                m.color = Color.blue; // The color is set to blue
            }
        }
    }
    public override void UnShowDamage()
    {
        if (_turnedThreeBlue)
        {
            for (int i = 0; i < materials.Length; i++) // loop continues from i = 0 until it reaches the size of "materials"
            {
                materials[i].color = Color.blue; // material color is set to blue
            }
        }
        else if (_turnedThreeBlue == false)
        {
            for (int i = 0; i < materials.Length; i++) // loop continues from i = 0 until it reaches the size of "materials"
            {
                materials[i].color = originalColors[i]; // material color is set to the color that is in originalColors array
            }
        }
        showingDamage = false; // showing damage is set to false. 

    }
}
