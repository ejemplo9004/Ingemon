using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanciaMenu : MonoBehaviour
{
    public GameObject[] fondos;

    void Start()
    {
        int f = Random.Range(0, fondos.Length);
        for (int i = 0; i < fondos.Length; i++)
        {
            fondos[i].SetActive(i == f);
        }
    }

    void Update()
    {
        
    }
}
