using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_3 : Enemy
{
    private bool _turnedFourBlue = false; // bool to keep track of enemy being turned blue

    public override void Move() // This Move function overrides the one in the Enemy class since it was virtual
    {
        if (GameObject.FindGameObjectWithTag("Hero") != null) // Checks if hero is not destroyed
        {
            if (this.gameObject.transform.position.y > GameObject.FindGameObjectWithTag("Hero").transform.position.y) // Checks if enemy has a higher y value than the ship
            {

                float step = speed * Time.deltaTime; // float variable called step is used to increment position
                gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, GameObject.FindGameObjectWithTag("Hero").transform.position, step); // Moves the enemy towards the hero
            }
            if (gameObject.transform.position.y < GameObject.FindGameObjectWithTag("Hero").transform.position.y) // Checks if y value of enemy is less than the ship
            {
                Vector3 temp = pos; // temp Vector3 to hold value of position
                temp.y -= speed * Time.deltaTime; // The enemy will fall in straight line
                pos = temp; 
            }
        }

        if (Input.GetKeyDown(KeyCode.B) && FreezeSlider.slide.value >= 100f)
        {
         
            _turnedFourBlue = true;
            speed = 4; // Speed of the boss enemy is reduced heavily
            foreach (Material m in materials) // For every m in "materials" the following happens
            {
                m.color = Color.blue; // The color is set to blue
            }

        }
    }
   

    public override void UnShowDamage()
    {
        if (_turnedFourBlue)
        {
            for (int i = 0; i < materials.Length; i++) // loop continues from i = 0 until it reaches the size of "materials"
            {
                materials[i].color = Color.blue; // material color is set to blue
            }
        }
        else if (_turnedFourBlue == false)
        {
            for (int i = 0; i < materials.Length; i++) // loop continues from i = 0 until it reaches the size of "materials"
            {
                materials[i].color = originalColors[i]; // material color is set to the color that is in originalColors array
            }
        }
        showingDamage = false; // showing damage is set to false. 

    }
}