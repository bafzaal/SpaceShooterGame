using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : Enemy
{
    public override void Move() // This Move function overrides the one in the Enemy class since it was virtual
    {
        Vector3 tempPos = pos; // New vector called tempPos is set to pos
        tempPos.y -= speed * Time.deltaTime; // tempPos.y is altered based on the speed and time
        tempPos.x += Mathf.Sin(Time.time) * Time.deltaTime * 4; // tempPos.x is altered based on the speed and time
        pos = tempPos; // pos is set to the tempPos vector that holds the new positions
    }
}
