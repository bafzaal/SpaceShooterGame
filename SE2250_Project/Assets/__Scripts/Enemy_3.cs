using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_3 : Enemy
{
    private bool turnedThreeBlue = false;
   
    public override void Move() // This Move function overrides the one in the Enemy class since it was virtual
    {
        if (GameObject.FindGameObjectWithTag("Hero") != null)
        {
            if (this.gameObject.transform.position.y > GameObject.FindGameObjectWithTag("Hero").transform.position.y)
            {

                float step = speed * Time.deltaTime;
                gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, GameObject.FindGameObjectWithTag("Hero").transform.position, step);
            }
            if (gameObject.transform.position.y < GameObject.FindGameObjectWithTag("Hero").transform.position.y)
            {
                Vector3 temp = pos;
                temp.y -= speed * Time.deltaTime;
                pos = temp;
            }
        }

        if (Input.GetKeyDown(KeyCode.B) && FreezeSlider.slide.value >= 100f)
        {
         
            turnedThreeBlue = true;
            speed = 4;
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

    }
}