using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : Enemy
{
    private int _randomDirection;

    void Start()
    {
    _randomDirection = Random.Range(0, 2);
    }
 
    public override void Move()
    {
        Vector3 tempPos = pos;
        tempPos.y -= Mathf.Sin(45f) * (speed * Time.deltaTime);

        if (_randomDirection == 0)
        {
            tempPos.x += Mathf.Cos(45f) * (speed * Time.deltaTime);
        }
        else
        {
            tempPos.x -= Mathf.Cos(45f) * (speed * Time.deltaTime);

        }
        if (tempPos.x >= 27f) 
           _randomDirection=1;

        if (tempPos.x <= -27f)
            _randomDirection = 0;

            pos = tempPos;
     
           
        }
    }

