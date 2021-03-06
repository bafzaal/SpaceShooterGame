﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private BoundsCheck _bndCheck;
    private Renderer _rend; 

    [Header("Set Dynamically")]
    public Rigidbody rigidBody; // Declare new rigidbody which will be set dynamically

    [SerializeField] //allows it to be visible and settable in inspector
    private WeaponType _weaponType;

    public WeaponType type //property with get and set
    {
        get
        {
            return (_weaponType); // Returns the weapon type that is in use
        }
        set
        {
            SetType(value); //calls function to set field and color
        }
    }

    void Awake()
    { //initialize components
        _bndCheck = GetComponent<BoundsCheck>();
        _rend = GetComponent<Renderer>();
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (_bndCheck.offUp) //boundscheck
        {
            Destroy(gameObject); // Destroys the gameObject
        }
    }

    //sets the _weaponType field and colors this projectile to match WeaponDefinition
       
    public void SetType(WeaponType eType)
    {
        _weaponType = eType;
        WeaponDefinition def = Main.GetWeaponDefinition(_weaponType);
        _rend.material.color = def.projectileColor;
    }
}
