using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : Enemy
{
    public override void Move()
    {
        Vector3 tempPos = pos;
        tempPos.y -=  Mathf.Sin(45f) * (speed * Time.deltaTime);
        tempPos.x += Mathf.Cos(45f) * (speed * Time.deltaTime);
        pos = tempPos;
    }
}
