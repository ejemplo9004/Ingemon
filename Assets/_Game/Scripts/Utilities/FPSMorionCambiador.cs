using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMorionCambiador : MonoBehaviour
{
    public int FPSEntrada   = 60;
    public int FPSSalida    = 30;
    void Start()
    {
        Application.targetFrameRate = FPSEntrada;
    }

    private void OnDestroy()
    {
        Application.targetFrameRate = FPSSalida;
    }
}
