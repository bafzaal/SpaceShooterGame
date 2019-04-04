using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum PowerUpType
{
    speed, //speed ship
    invincible //slow enemies
  
}

public class PowerUp : MonoBehaviour
{
    [Header("Set in Inspector")]
    // x holds a min value and y a max for a Random.Range() that will be called later
    public Vector2 rotMinMax = new Vector2(15, 90);
    public Vector2 driftMinMax = new Vector2(.25f, 2);
    public float lifeTime = 6f;//Seconds the PowerUp exists
    public float fadeTime = 4f; //Seconds it will then fade

    [Header("Set Dynamically")]
    public PowerUpType type; //The type of the PowerUp
    public GameObject cube; //Reference to the Cube child
    public TextMesh letter; //Reference to the TextMesh
    public Vector3 rotPerSecond; //Euler rotation speed
    public float birthTime; 

    private Rigidbody _rigid; //private vars
    private BoundsCheck _bndCheck;
    private Renderer _cubeRend;

    void Awake()
    {
        //Find the Cube reference
        cube = transform.Find("Cube").gameObject;
        //Find the TextMesh and other components
        letter = GetComponent<TextMesh>();
        _rigid = GetComponent<Rigidbody>();
        _bndCheck = GetComponent<BoundsCheck>();
        _cubeRend = GetComponent<Renderer>();

        //Set a random velocity
        Vector3 vel = Random.onUnitSphere; //Get Random XYZ velocity
        /*Random.onUnitSphere gives you a vector point that is somewhere on 
         the surface of the sphere with a radius of 1m around the origin*/
        vel.z = 0; //Flatten the vel to the XY plane
        vel.Normalize(); //Normalizing a Vector3 makes its length 1m

        vel *= Random.Range(driftMinMax.x, driftMinMax.y);
        _rigid.velocity = vel;

        //Set the rotation of this GameObject to R:[0,0,0]
        transform.rotation = Quaternion.identity;
        //Quaternion.identity is equal to no rotation

        //Set up the rotPerSecond for the Cube child using rotMinMax x & y
        rotPerSecond = new Vector3(Random.Range(rotMinMax.x, rotMinMax.y),
            Random.Range(rotMinMax.x, rotMinMax.y),
            Random.Range(rotMinMax.x, rotMinMax.y));


        birthTime = Time.time;

    }

    // Update is called once per frame
    void Update()
    {
        cube.transform.rotation = Quaternion.Euler(rotPerSecond * Time.time);//manually rotates the cube

        //Fade out the PowerUp over time
        //Given the default values, a PowerUp will exist for 10 seconds
        //and fade out over 4 seconds
        float u = (Time.time - (birthTime + lifeTime)) / fadeTime;
        /*For lifeTime seconds, u will be <=0. Then it will transition to 
          1 over the course of fadeTime seconds*/

        //if u>=1, Destroy this PowerUp
        if (u >= 1)
        {
            Destroy(this.gameObject);
            return;
        }

        //Use u to determine the alpha value of the Cube & Letter
        if (u > 0)
        {
            Color c = _cubeRend.material.color;
            c.a = 1f - u;
            _cubeRend.material.color = c;

            //Fade the Letter too, just not as much
            c = letter.color;
            c.a = 1f - (u * 0.5f);
            letter.color = c;
        }

        if (!_bndCheck.isOnScreen)
        {
            //If the PowerUp has drifted entirely off the screen, destroy it
            Destroy(gameObject);
        }
    }

    public void setType(PowerUpType puT)
    {
        if (puT ==PowerUpType.speed)
       {
            _cubeRend.material.color = Color.red;
            letter.text = "SPEED";
            type = puT;

        }

        if (puT == PowerUpType.invincible)
        {
            _cubeRend.material.color = Color.blue;
            letter.text = "INVINCIBLE";
            type = puT;
        }

    }

    public void AbsorbedBy(GameObject target)
    {
        //This function is called by the Hero Class when a powerup is collected
        //Destroys the object
        Destroy(this.gameObject);
    }
}
