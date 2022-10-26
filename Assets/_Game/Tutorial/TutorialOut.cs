using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialOut : MonoBehaviour
{
    [SerializeField] private GameObject obj;

    private void OnDisable()
    {
        obj.SetActive(true);
    }
}
