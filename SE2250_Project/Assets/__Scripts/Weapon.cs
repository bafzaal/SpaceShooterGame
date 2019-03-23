using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is an enum for various possible weapon types
    public enum WeaponType
{
    none, //default, no weapon
    blaster, //A simple blaster
    spread, //Two simultaneous shots
    shield //Raise shieldLevel
}


/*WeaponDefinition class allows to set properties of specific weapon
in the inspector. The main class will have an array of these*/
[System.Serializable]
public class WeaponDefinition
{
    public WeaponType type = WeaponType.none;
    public string letter; //Letter to show on power-up
    public Color color = Color.white; //Color of Collar & power-up
    public GameObject projectilePrefab; //Prefab for projectiles
    public Color projectileColor = Color.white; 
    public float damageOnHit = 0; //Amount of damage caused
    public float delayBetweenShots = 0; // DelaryBetweenShots float variable is initialized to zero
    public float velocity = 20; //speed of projectiles
}
public class Weapon : MonoBehaviour
{
    static public Transform PROJECTILE_ANCHOR;

    [Header("Set Dynamically")]
    [SerializeField]
    private WeaponType _weaponType = WeaponType.none; // _WeaponType is initialized to the enum none
    public WeaponDefinition def;
    public GameObject collar; // Gameobject called collar
    public float lastShotTime; //time last shot was fired
    private Renderer _collarRend;

    private int _currWeaponNumber = 0; // New private int _currWeaponNumber is initialized to zero, this varibale will help switch weapons.

    void Start()
    {
        collar = transform.Find("Collar").gameObject;
        _collarRend = collar.GetComponent<Renderer>();

        //call SetWeaponType() for the default _weapontype of WeaponType.none
        SetWeaponType(_weaponType);

        //Dynamically create an anchor for all Projectiles
        if (PROJECTILE_ANCHOR == null) //static Transform to act as parent of projectiles
        {
            GameObject gameobject = new GameObject("_ProjectileAnchor");
            PROJECTILE_ANCHOR = gameobject.transform;
        }
        //Find the fireDelegate of the root GameObject
        GameObject rootGameObject = transform.root.gameObject;
        if (rootGameObject.GetComponent<Hero>() != null)
        {//if a gameobject has a hero scropt, fire() is added to delegate
            rootGameObject.GetComponent<Hero>().fireDelegate += Fire;
        }
    }

    public WeaponType type
    {
        get
        {
            return (_weaponType); // Returns the current weaponType that is in use
        }
        set
        {
            SetWeaponType(value); // Sets the weapon type to the value
        }
    }

    public void SetWeaponType(WeaponType wepType)
    {
        _weaponType = wepType; // _weaponType is set to the passed parameter wepType

        if (type == WeaponType.none) //if weapontype is none, gameobject is disabled
        {
            this.gameObject.SetActive(false);
            return;
        }
        else
        {
            this.gameObject.SetActive(true);
        }
        def = Main.GetWeaponDefinition(_weaponType); //pulls proper WeaponDefinition
        _collarRend.material.color = def.color; //sets color of Collar
        lastShotTime = 0;//You can fire immediately after _weaponType is set
    }

    public void Fire()
    {
        //if this.gameObject is inactive, return
        if (!gameObject.activeInHierarchy) return;

        //if it hasn't been enough time between shots, return
        if (Time.time - lastShotTime < def.delayBetweenShots) 
        {
            return;
        }

        Projectile weaponProjectile; // New Projectile called weaponProjectile is declared

        Vector3 velocity = Vector3.up * def.velocity; //initial velocity is set to up
        if (transform.up.y < 0)
        {
            velocity.y = -velocity.y; //y component set to down if weapons face down
        }

        switch (type) //cases for the blast and spread WeaponType
        {
            case WeaponType.spread: //single projectile
                weaponProjectile = MakeProjectile(); //calls function
                weaponProjectile.rigidBody.velocity = velocity; //initialize velocity of projectile
                break;

            case WeaponType.blaster: //3 types of proejctiles created if it is a spread
                weaponProjectile = MakeProjectile(); //Make middle projectile
                weaponProjectile.rigidBody.velocity = velocity; //initialize velocity
                weaponProjectile = MakeProjectile(); //Make right projectile
                weaponProjectile.transform.rotation = Quaternion.AngleAxis(30, Vector3.back);
                weaponProjectile.rigidBody.velocity = weaponProjectile.transform.rotation * velocity;
                weaponProjectile = MakeProjectile(); //Make left projectile
                weaponProjectile.transform.rotation = Quaternion.AngleAxis(-30, Vector3.back);
                weaponProjectile.rigidBody.velocity = weaponProjectile.transform.rotation * velocity;
                break;
        }
    }

    /*this method instantiates a clone of the prefab in WeaponDefinition
     and returns a Projectile object reference*/  
    public Projectile MakeProjectile()
    {

        GameObject gObject = Instantiate<GameObject>(def.projectilePrefab);

        //proper tag is given depending on who fired the projectile:
        if (transform.parent.gameObject.tag == "Hero") 
        {
            gObject.tag = "ProjectileHero"; // gObject tag is set to "ProjectileHero"
            gObject.layer = LayerMask.NameToLayer("ProjectileHero");
        }
        else
        {
            gObject.tag = "ProjectileEnemy"; // gObject tag is set to "ProjectileEnemy"
            gObject.layer = LayerMask.NameToLayer("ProjectileEnemy");
        }
        gObject.transform.position = collar.transform.position;
        //places it under the _ProjectileAnchor in hierarchy pane to keep it clean:
        gObject.transform.SetParent(PROJECTILE_ANCHOR, true);
        Projectile projectile = gObject.GetComponent<Projectile>();
        projectile.type = type;
        //lastShotTime is set to current time it follows the delayBetweenShots:
        lastShotTime = Time.time; 
        return (projectile); //return object reference of Projectile
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C)) // If C is pressed then the following happens
        {
            _currWeaponNumber++; // Current weapon number is increased by 1
            if (_currWeaponNumber > 1) // If it is greater than one it is reset since there is only 2 weapons right now (weapon 0 and weapon 1)
                _currWeaponNumber = 0; // Resets the currWeaponNumber variable
            if (_currWeaponNumber == 0) // If the currWeaponVariable is 0
                SetWeaponType(WeaponType.blaster); // WeaponType is set to the blaster weapon
            else if (_currWeaponNumber == 1) // If the currWeaponNumber is 1
                SetWeaponType(WeaponType.spread); // WeaponType is set to the spread weapon
        }
    }

}
