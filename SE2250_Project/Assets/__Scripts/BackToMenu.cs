using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    public void MenuStart() // MenuStart Function is called in order to load the menu scene
    {
        SceneManager.LoadScene(0); // Using UnityEngine.SceneManager the Menu scene is loaded
    }
}
