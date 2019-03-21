using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{

    static public Hero S;

    [Header("Set in Inspector")]
    // These fields control the movement of the ship
    public float speed = 30;
    public float rollMult = -45;
    public float pitchMult = 30;
    public float gameRestartDelay = 2f;

    [Header("Set Dynamically")]
    [SerializeField]
    private float _shieldLevel = 1;
    //holds a reference to the last triggering GameObject:
    private GameObject _lastTriggerGo = null;
 

    void Awake()
    {
        if (S == null)
        {
            S = this;
        }
        else
        {
            Debug.LogError("Hero.Awake() - Attempted to assign second Hero.S!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Pull in information from the Input class
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        //Change transform.position based on the axes
        Vector3 pos = transform.position;
        pos.x += xAxis * speed * Time.deltaTime;
        pos.y += yAxis * speed * Time.deltaTime;
        transform.position = pos;

        //Rotate the ship to make it feel more dynamic
        transform.rotation = Quaternion.Euler((yAxis * pitchMult) - 90f, xAxis * rollMult, 0);
    }


    void OnTriggerEnter(Collider other)
    {
        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;
        //print("Triggered : " + go.name); 

        //Make sure it's not the same triggering go as last time 
        if (go == _lastTriggerGo) /*this can happen if the same enemy triggers 
                                    the hero in the same single frame*/
        {
            return;
        }
        _lastTriggerGo = go; //so that it updates the next time it is called


        if (go.tag == "Enemy") //If the shield was triggered by an enemy
        {
            shieldLevel--; //Decrease the level of the shield by 1
            Destroy(go); //..and destroy the enemy
        }
        else
        {
            print("Triggered by non-enemy: " + go.name);
        }
    }

    public float shieldLevel
    {
        get
        {
            return (_shieldLevel);
        }
        set
        {   //ensure that _shieldLevel is never higher than 4
            _shieldLevel = Mathf.Min(value, 4);

            if (value < 0)
            {   
            //if the value is less than zero, hero is destroyed
                Destroy(this.gameObject);

             //Tell Main.S to restart the game after a delay
                Main.S.DelayedRestart(gameRestartDelay);
            }
        }
    }
}
