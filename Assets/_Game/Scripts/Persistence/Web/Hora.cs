using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hora : MonoBehaviour
{
    public void obtenerHora()
    {
        DateTime dateTime = DateTime.Now;
        
        Debug.Log(dateTime.ToString("yyyy-M-d"));
    }
}
