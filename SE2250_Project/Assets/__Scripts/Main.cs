using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    static public Main Singleton; // New Singleton is created
    static Dictionary<WeaponType, WeaponDefinition> WEAP_DICT; //has enum as key and class as the value
    [Header("Set in Inspector")]
    public GameObject[] prefabEnemies; // And array for all the enemies
    public float enemySpawnPerSecond = 0.5f; // 0.5 enemies spawn per second
    public float enemyDefaultPadding = 1.5f; // The enemy default badding is 1.5
    public WeaponDefinition[] weaponDefinitions; //uses enum for properties

    private BoundsCheck _bndCheck; // Private variable for bounds check is declared

    void Awake()
    {
        Singleton = this; // Singleton set to the current instance (this)
       _bndCheck = GetComponent<BoundsCheck>();
        Invoke("SpawnEnemy", 1.5f / enemySpawnPerSecond); // Spawns the Enemy based on the spawnPerSecond

        //A generic Dictonary with WeaponType as the key
        WEAP_DICT = new Dictionary<WeaponType, WeaponDefinition>(); // New Dictionary is declared
        foreach (WeaponDefinition def in weaponDefinitions)
        {
            WEAP_DICT[def.type] = def;
        }
    }

    public void SpawnEnemy() // SpawnEnemy function is public and returns void (returns nothing)
    {
        int ndx = Random.Range(0, prefabEnemies.Length); // ndx variable holds a number from 0 to the amount of prefabEnemies
        GameObject go = Instantiate<GameObject>(prefabEnemies[ndx]); // Instantiates a enemy based on the random number

        float enemyPadding = enemyDefaultPadding; // enemyPadding is set to the Default enemy padding
        if (go.GetComponent<BoundsCheck>() != null)
            enemyPadding = Mathf.Abs(go.GetComponent<BoundsCheck>().radius);

        Vector3 pos = Vector3.zero; // The vector position is set to a zero vector
        float xMin = -_bndCheck.camWidth + enemyPadding; // xMin is the camWidth and also takes the enemyPadding into account
        float xMax =_bndCheck.camWidth - enemyPadding; // xMax is the camWidth and also takes the enemyPadding into account
        pos.x = Random.Range(xMin, xMax);
        pos.y = _bndCheck.camHeight + enemyPadding;
        go.transform.position = pos; // go.transform.position is set to the previously altered Vector called pos

        Invoke("SpawnEnemy", 1.5f / enemySpawnPerSecond); // Spawns the Enemy based on the spawnPerSecond
    }

    public void DelayedRestart(float delay)
    {
        //Invoke the Restart() method in delay seconds
        Invoke("Restart", delay);
    }

    public void Restart() // The Restart function is used in this class
    {
        //Reload _Scene_0 to restart the game
        SceneManager.LoadScene("_Scene_0");
    }

    static public WeaponDefinition GetWeaponDefinition(WeaponType weaponType)
    {
       //check to make sure key exists in dictionary
        if (WEAP_DICT.ContainsKey(weaponType))
        {
            return (WEAP_DICT[weaponType]);
        }
        //failed to find the right weapon so it returns WeaponType.none
        return (new WeaponDefinition());
    }

}
