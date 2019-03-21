using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is an enum for various possible weapon types
    public enum WeaponType
{
    none, //default, mo weapon
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
    public float delayBetweenShots = 0;
    public float velocity = 20; //speed of projectiles
}
public class Weapon : MonoBehaviour
{
    static public Transform PROJECTILE_ANCHOR;

    [Header("Set Dynamically")]
    [SerializeField]
    private WeaponType _weaponType = WeaponType.none;
    public WeaponDefinition def;
    public GameObject collar;
    public float lastShotTime; //time last shot was fired
    private Renderer _collarRend;

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
            return (_weaponType);
        }
        set
        {
            SetWeaponType(value);
        }
    }

    public void SetWeaponType(WeaponType wepType)
    {
        _weaponType = wepType;

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

        Projectile weaponProjectile;

        Vector3 velocity = Vector3.up * def.velocity; //initial velocity is set to up
        if (transform.up.y < 0)
        {
            velocity.y = -velocity.y; //y component set to down if weapons face down
        }

        switch (type) //cases for the blaset and spread WeaponType
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
            gObject.tag = "ProjectileHero";
            gObject.layer = LayerMask.NameToLayer("ProjectileHero");
        }
        else
        {
            gObject.tag = "ProjectileEnemy";
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


}
