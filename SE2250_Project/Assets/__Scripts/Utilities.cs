using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour
{
    /*==================================== Materials Functions ==================================== */

    // Returns a list of all Materials on this GameObject and its children]
    static public Material[] GetAllMaterials(GameObject go)
    {
        Renderer[] rends = go.GetComponentsInChildren<Renderer>(); // Declares a new array of Renderer called rends

        List<Material> mats = new List<Material>(); // List of materials called mats is declared
        foreach(Renderer rand in rends) // For each Renderer in rends
        {
            mats.Add(rand.material); // A new materials is added to the mats list
        }
        return (mats.ToArray()); // returns the mats list as an array
    }
}
