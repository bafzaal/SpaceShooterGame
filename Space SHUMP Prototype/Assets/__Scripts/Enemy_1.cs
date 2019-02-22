using System.Collections;
using System.Collections.Generic;
using UnityEngine;

  public class Enemy_1 : Enemy
    {
        bool hitRight = false;

        public override void Move()
        {
            Vector3 tempPos = pos;
            tempPos.y -= Mathf.Sin(45f) * (speed * Time.deltaTime);
            if (hitRight == false)
            {
                tempPos.x += Mathf.Cos(45f) * (speed * Time.deltaTime);
            }
            else
            {
                tempPos.x -= Mathf.Cos(45f) * (speed * Time.deltaTime);
            }
            if (tempPos.x >= 27f)
                hitRight = true;
            if (tempPos.x <= -27f)
                hitRight = false;

            pos = tempPos;
        }
    }
