using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : Enemy
{
    int numberOfTicks = 0;

    public override void Move()
    {
        Vector3 tempPos = pos;
        tempPos.y -= (speed - 5f) * Time.deltaTime;
        tempPos.x += Mathf.Sin(tempPos.y);

        pos = tempPos;
    }
}
