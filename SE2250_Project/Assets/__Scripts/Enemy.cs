using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Header("Set In Inspector: Enemy")]
    public float speed = 10f; // Speed in m/s
    public float fireRate = 0.3f; // Seconds/shot
    public float health = 5; // health of the enemy
    public int score = 100; // Points earned for destroying this
    public float showDamageDuration = 0.1f; // Shows the damage for 0.5 seconds

    [Header("Set Dynamically: Enemy")]
    public Color[] originalColors; // new array of colors
    public Material[] materials; // All the materials of this and its children
    public bool showingDamage = false; // Currently not showing damage
    public float damageDoneTime; // Time to stop showing damage
    public bool notifiedofDestruction = false; // Will be used later
    public ScoreCounter scoreCounter;
    private BoundsCheck _bndCheck; // Private bounds check variable
    void Awake()
    {
        _bndCheck = GetComponent<BoundsCheck>();
        // Get materials and colors for this gameObject and its children
        materials = Utilities.GetAllMaterials(gameObject); // Calls the static function in Utilities class
        originalColors = new Color[materials.Length]; // originalColors array set to size of materials
        for(int i = 0; i < materials.Length; i++) // loop continues until it reaches size of materials
        {
            originalColors[i] = materials[i].color; // Puts material colors into origionalColors array
        }
    }
    
    void Start()
    {
    }

    // This is a Property: A method that acts like a field
    public Vector3 pos
    {
        get
        {
            return (this.transform.position); // returns the current position
        }
        set
        {
            this.transform.position = value; // sets the position to the value
        }
    }

    public virtual void Move() // virtual function called Move
    {

            Vector3 tempPos = pos; // Temporary Vector to hold the position
            tempPos.y -= speed * Time.deltaTime; // temp position is set to a new value based on speed and time
            pos = tempPos; // pos is set to the value of tempPos
        

    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject otherGO = collision.gameObject; // Creates a new GameObject called otherGO and is set to the collision gameObject
        switch(otherGO.tag)
        {
            case "ProjectileHero": // In the case that the tag is ProjectileHero
                Projectile weaponProjectile = otherGO.GetComponent<Projectile>(); // new Projectile called weapon projectile is initialized as the projectile 
                                                                                  // of the otherGO gameobject
                if (!_bndCheck.isOnScreen) // If not on screen the gameobject is destroyed
                {
                    Destroy(otherGO); // Destroy the gameobject
                    break;
                }
                ShowDamage(); // Calls the ShowDamage() function
                health -= Main.GetWeaponDefinition(weaponProjectile.type).damageOnHit; // Health is decreased in case of a collision with projectile
                if (health <= 0)
                {
                    if (this.gameObject.name == "Enemy_0(Clone)")
                    {
                        ScoreCounter.CURR_SCORE += 100;
                        Destroy(this.gameObject);
                    }
                    else if (this.gameObject.name == "Enemy_1(Clone)")
                    {
                        ScoreCounter.CURR_SCORE += 200;
                        Destroy(this.gameObject);
                    }
                    else if (this.gameObject.name == "Enemy_2(Clone)")
                    {
                        ScoreCounter.CURR_SCORE+= 300;
                        Destroy(this.gameObject);
                    }
                }// Destroy this game object
                Destroy(otherGO); // Destroy the otherGO game object
                break;

            default:
                print("Enemy hit by non-ProjectileHero: " + otherGO.name); // In the case that the enemy is not hit by projectileHero
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move(); // calls the move function

        if(showingDamage && Time.time > damageDoneTime)
        {
            UnShowDamage(); // Call the UnShowDamage function
        }

        if(_bndCheck != null && _bndCheck.offDown)
        {
            // Enemy has gone off the bottom of the screen so destroy it
            Destroy(gameObject);
        }
    }

    void ShowDamage()
    {
        foreach(Material m in materials) // For every m in "materials" the following happens
        {
            m.color = Color.white; // The color is set to white to show the damage
        }
        showingDamage = true; // showing damage is set to true since damage is being shown
        damageDoneTime = Time.time + showDamageDuration; // damageDoneTime is then changed
    }

    void UnShowDamage()
    {
        for(int i = 0; i < materials.Length; i++) // loop continues from i = 0 until it reaches the size of "materials"
        {
            materials[i].color = originalColors[i]; // material color is set to the color that is in originalColors array
        }
        showingDamage = false; // showing damage is set to false. 
    }

}
