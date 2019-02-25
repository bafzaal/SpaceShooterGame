using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Header("Set In Inspector: Enemy")]
    public float speed = 10f; // Speed in m/s
    public float fireRate = 0.3f; // Seconds/shto (Unused)
    public float health = 10; // (Unused Currently)
    public int score = 100; // Points earned for destroying this (Unused Currently)

    private BoundsCheck _bndCheck;

    void Awake()
    {
        _bndCheck = GetComponent<BoundsCheck>();
    }

    // This is a Property: A method that acts like a field
    public Vector3 pos
    {
        get
        {
            return (this.transform.position);
        }
        set
        {
            this.transform.position = value;
        }
    }

    public virtual void Move()
    {
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        pos = tempPos;
    }


    // Update is called once per frame
    void Update()
    {
        Move();

        if(_bndCheck != null && _bndCheck.offDown)
        {
            // Enemy has gone off the bottom of the screen so destroy it
            Destroy(gameObject);
        }
    }
}
