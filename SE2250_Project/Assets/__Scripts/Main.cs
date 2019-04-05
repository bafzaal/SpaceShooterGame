using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    static public Main Singleton; // New Singleton is created
    static Dictionary<WeaponType, WeaponDefinition> WEAP_DICT; //has enum as key and class as the value
    [Header("Set in Inspector")]
    public GameObject[] prefabEnemies; // And array for all the enemies
    public float enemySpawnPerSecond = 0.5f; // 0.5 enemies spawn per second
    public float levelStartDelay = 1.5f; // 2 seconds between level start
    private Text _levelText; // The text that is shown between levels
    private GameObject _levelImage; // The image for the background of the text
    private bool _doingSetUp; // bool to identify if setup is in progress or not
    static public int LEVEL = 1; // Current level indicator
    public bool isLevel2 = false;
    public float enemyDefaultPadding = 1.5f; // The enemy default badding is 1.5
    public WeaponDefinition[] weaponDefinitions; //uses enum for properties
    public GameObject prefabPowerUp; //holds prefabs for powerups
    //determines how often each powerup will be created:
    public PowerUpType[] powerUpFrequency = new PowerUpType[] { PowerUpType.speed, PowerUpType.invincible };
    private BoundsCheck _bndCheck; // Private variable for bounds check is declared


    public void ShipDestroyed(Enemy e) //called when enemy is destroyed
    {   //potentially generate a powerup

        if (Random.value <= e.powerUpDropChance)//random value compared to see if powerup should be dropped
        {
            int ndx = Random.Range(0, powerUpFrequency.Length);//generates an index array number to randomly spawn powerup
            PowerUpType puType = powerUpFrequency[ndx];

            //spawn a powerup
            GameObject go = Instantiate(prefabPowerUp) as GameObject;
            PowerUp pu = go.GetComponent<PowerUp>();

            //set it to the proper PowerUpType
            pu.setType(puType); //setType handles color, text, and type of cube


            //set it to the position of the destoryed ship
            pu.transform.position = e.transform.position;

        }
    }

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

        InitGame();

    }

    void InitGame()
    {
       

        _doingSetUp = true;
        if(LEVEL == 1)
        {
            _levelImage = GameObject.Find("LevelImage");
            _levelText = GameObject.Find("LevelText").GetComponent<Text>();
        }
        _levelText.text = "LEVEL: " + LEVEL;
        _levelImage.SetActive(true);
        _levelText.gameObject.SetActive(true);
        Invoke("HideLevelImage", levelStartDelay);
    }

   
    private void HideLevelImage()
    {
        _levelImage.SetActive(false);
        _levelText.gameObject.SetActive(false);
        _doingSetUp = false;
    }

    public void SpawnEnemy() // SpawnEnemy function is public and returns void (returns nothing)
    {
        int ndx;
        if(LEVEL == 1)
            ndx = Random.Range(0, prefabEnemies.Length - 1); // ndx variable holds a number from 0 to the amount of prefabEnemies
        else
            ndx = Random.Range(0, prefabEnemies.Length);

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
        ScoreCounter.CURR_SCORE = 0;
    }

    public void Restart() // The Restart function is used in this class
    {
        //Reload _Scene_0 to restart the game
        LEVEL = 1;
        SceneManager.LoadScene("Menu");
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

    private void Update()
    {
        if (_doingSetUp)
            return;
        if(LEVEL == 2 && isLevel2 != true)
        {
            Awake();
            isLevel2 = true;
        }
    }
}
