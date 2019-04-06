using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{

    static public Hero S;

    [Header("Set in Inspector")]
    // These fields control the movement of the ship
    public float speed = 30; // Speed is set to 30
    public float rollMult = -45; // RollMult is set to -45
    public float pitchMult = 30f; // pitchMult is set to 30
    public float gameRestartDelay = 2f; //The game restart delay is now 2
    public float projectileSpeed = 40f; // The speed of the pojectile is 40
   
    private float _timer = 0f; //timer to reset powerups
    private bool _invincible = true; //checks if it is invincible to disable collisons
    private Material _mat; //retrieve hero material
    private Color[] _colors = new Color[]{ Color.yellow, Color.black }; //changes color of hero for powerup
    private int _index = 0; 
    public RawImage fast, star; //powerup images
    public AudioClip prize; //audio clip tp be played
    public GameObject explosionPrefab,exhaustPrefab,flame1,flame2; //different animation effects to be used

    [Header("Set Dynamically")]
    [SerializeField]
    private float _shieldLevel = 1; // Shield level is initialized to 1
    //holds a reference to the last triggering GameObject:
    private GameObject _lastTriggerGo = null;

    //declare a new delegate type WeaponFireDelegate:
    public delegate void WeaponFireDelegate();
    //create a WesponFireDelegate:
    public WeaponFireDelegate fireDelegate;

    void Awake()
    {
        if (S == null) // If the Singleton is null it is then set to this instance that was called
        {
            S = this;
        }
        else // if S is not null
        {
            Debug.LogError("Hero.Awake() - Attempted to assign second Hero.S!"); // Message is sent to console
        }

        _mat = GetComponent<Renderer>().material;

       

    }
   
    private void Start()
    {
        GameObject gObject = GameObject.Find("Star");
        
        if (gObject != null)
        {
            star = gObject.GetComponent<RawImage>();
            star.gameObject.SetActive(false);
        }

        gObject = GameObject.Find("Fast");
        if (gObject != null)
        {
            fast = gObject.GetComponent<RawImage>();
            fast.gameObject.SetActive(false);
        }
     flame1= GameObject.Find("Flame1"); //finds hero components for exhaust
     flame2 = GameObject.Find("Flame2");

    }

    // Update is called once per frame
    void Update()
    {
        //Pull in information from the Input class
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        //Change transform.position based on the axes
        Vector3 pos = transform.position;
        pos.x += xAxis * speed * Time.deltaTime; // Alters the position in the x direction based on speed and time
        pos.y += yAxis * speed * Time.deltaTime; // Alters the position in the y direction based on speed and time
        transform.position = pos;

        GameObject exhaust1 = Instantiate(exhaustPrefab,flame1.transform.position, Quaternion.identity) as GameObject; //exhaust effect  of ship is displayed
        exhaust1.transform.SetParent(Weapon.EFFECTS_ANCHOR, true); //put effect in the anchor

        GameObject exhaust2 = Instantiate(exhaustPrefab, flame2.transform.position, Quaternion.identity) as GameObject; //exhaust effect  of ship is displayed
        exhaust2.transform.SetParent(Weapon.EFFECTS_ANCHOR, true); //put effect in the anchor

        Destroy(exhaust1, 0.1f); //destroy flame effect 
        Destroy(exhaust2, 0.1f); //destroy flame effect 

        //Rotate the ship to make it feel more dynamic
        transform.rotation = Quaternion.Euler((yAxis * pitchMult) - 90f, xAxis * rollMult, 0);

       //using the fireDelegate to fire Weapons, button must be pressed and delegate isn't null
        if (Input.GetAxis("Jump") == 1 && fireDelegate != null) //jump or space are inputs
        {
            fireDelegate(); //calls all functions added to fireDelegate
        }

        _timer += Time.deltaTime; //update timer

        if (_timer > 12.0f) //if timer is >12, invincible is stopped and power up image is gone
        {
            _timer = 0f;
            speed = 30f;
            _invincible = true;
            fast.gameObject.SetActive(false);

        }

        if (_invincible) //when invincibility is done, hero ship goes back to white color and power up image is gone
        {
            _mat.color = Color.white;
            star.gameObject.SetActive(false);
        }

    }

    private void FixedUpdate()
    {
        _mat.color = _colors[_index % 2]; //allows for flashing of hero ship (yellow and black)
        _index++;

    }
   
    void OnTriggerEnter(Collider other)
    {
        
        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;

        if (_invincible) {  //if it is invincible, avoid all collisions
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
                GameObject explosion=Instantiate(explosionPrefab,other.gameObject.transform.position, Quaternion.identity) as GameObject; //effect when hero loses shield or is destroyed
                explosion.transform.SetParent(Weapon.EFFECTS_ANCHOR, true); //anchor for effect
                Destroy(go); //..and destroy the enemy
                Destroy(explosion,2); //destroy explosion effect
        }
        else
        {
                if((go.tag != "PowerUp"))
            print("Triggered by non-enemy: " + go.name); // Prints message to the console
        }
            }

        if (go.tag == "PowerUp")
        {//if shield was triggered by PowerUp
            AbsorbPowerUp(go); //powerup absorbed by hero
            AudioSource.PlayClipAtPoint(prize, new Vector3(5, 1, 2)); //play audio when powerup absorbed

        }
    }
 
  


    public void AbsorbPowerUp(GameObject go)
    { 

        PowerUp pu = go.GetComponent<PowerUp>(); //creates power up object and retrieves components
        switch (pu.type) //switch statement depending on type of powerup absorbed
        {
            case PowerUpType.speed: // if speed, set timer to zero and speed to 70
                speed = 70f;
                _timer = 0;
                fast.gameObject.SetActive(true); //display speed icon
                break;


            case PowerUpType.invincible: //if invincible, turn it on and set timer to zero
                _invincible = false;
                _timer = 0;
                star.gameObject.SetActive(true); //display star icon
                break;

        }
        pu.AbsorbedBy(this.gameObject);
    }

    public float shieldLevel
    {
        get
        {
            return (_shieldLevel); // Returns the shieldLevel
        }
        set
        {   //ensure that _shieldLevel is never higher than 4
            _shieldLevel = Mathf.Min(value, 4);

            if (value < 0)
            {   
            //if the value is less than zero, hero is destroyed
                Destroy(this.gameObject);

             //Tell Main.S to restart the game after a delay
                Main.Singleton.DelayedRestart(gameRestartDelay);
            }
        }
    }
}
