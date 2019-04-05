using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : Enemy
{
    protected float enemyThreeTime = 0;
    private float holdX = 1;
    private bool turnedThreeBlue = false;
    public override void Move() // This Move function overrides the one in the Enemy class since it was virtual
    {

        Vector3 tempPos = pos; // New vector called tempPos is set to pos
        tempPos.y -= speed * Time.deltaTime; // tempPos.y is altered based on the speed and time
        tempPos.x += Mathf.Sin(Time.time) * Time.deltaTime * 4 * holdX; // tempPos.x is altered based on the speed and time
        pos = tempPos; // pos is set to the tempPos vector that holds the new positions
<<<<<<< HEAD
        if (Input.GetKeyDown(KeyCode.B) && FreezeSlider.slide.value >= 100f)
        {
            speed = 0;
            holdX = 0;
            foreach (Material m in materials) // For every m in "materials" the following happens
            {
                m.color = Color.blue; // The color is set to white to show the damage
            }
        }
    }
    public override void UnShowDamage()
    {
        if (turnedThreeBlue)
        {
            for (int i = 0; i < materials.Length; i++) // loop continues from i = 0 until it reaches the size of "materials"
            {
                materials[i].color = Color.blue; // material color is set to the color that is in originalColors array
            }
        }
        else if (turnedThreeBlue == false)
        {
            for (int i = 0; i < materials.Length; i++) // loop continues from i = 0 until it reaches the size of "materials"
            {
                materials[i].color = originalColors[i]; // material color is set to the color that is in originalColors array
            }
        }
        showingDamage = false; // showing damage is set to false. 
=======
        
>>>>>>> origin/newLevelBranch
    }
}
