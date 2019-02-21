using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : Enemy
{
    public override void Move()
    {
        Vector3 tempPos = pos;
        tempPos.y -= (speed - 7f) * Time.deltaTime;
        tempPos.x += Mathf.Sin(tempPos.y);

        pos = tempPos;
    }
}
