using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoyTest : MonoBehaviour
{
    void Start()
    {
        Ingemonster ing = new IngemonBuilder().WithName("Geronimo").WithMaxHealth(300);
        Debug.Log(ing.id);
        Debug.Log(ing.name);
        Debug.Log(ing.maxHealth.ToString());
        Debug.Log(ing.phenotype);
    }


}
