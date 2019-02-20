using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Header("Set In Inspector: Enemy")]
    public float speed = 10f; // Speed in m/s
    public float fireRate = 0.3f; // Seconds/shto (Unused)
    public float health = 10;
    public int score = 100; // Points earned for destroying this

    private BoundsCheck bndCheck;

    void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if(bndCheck != null && bndCheck.offDown)
        {
            // Enemy has gone off the bottom of the screen so destroy it
            Destroy(gameObject);
        }
    }
}
