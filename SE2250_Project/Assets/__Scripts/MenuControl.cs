using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    public void ButtonStart()
    {
        SceneManager.LoadScene(1);
    }

    public void IntructionsStart()
    {
        SceneManager.LoadScene(2);
    }

    public void CreditStart()
    {
        SceneManager.LoadScene(3);
    }
        
}
