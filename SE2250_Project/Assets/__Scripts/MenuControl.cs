using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    public void ButtonStart() // Function used for on button press
    {
        SceneManager.LoadScene(1); // Loads the first scene which is the scene in which the game is played
    }

    public void IntructionsStart()// Function used for on button press
    {
        SceneManager.LoadScene(2); // Loads the scene in which the intructions are held
    }

    public void CreditStart() // Function used for on button press
    {
        SceneManager.LoadScene(3); // Loads the scene in which the credits are held
    }
        
}
