using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : Enemy
{
    public override void Move()
    {
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        tempPos.x += Mathf.Sin(Time.time) * Time.deltaTime * 4;
        pos = tempPos;
    }
}
