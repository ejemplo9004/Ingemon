using System;
using UnityEngine;

public class TutorialActivator: MonoBehaviour
{
    [SerializeField] private GameObject tutorialObject;
    private void Start()
    {
        if (PlayerPrefs.GetInt("FirstTime", 1) == 1)
        {
            tutorialObject.SetActive(true);
            PlayerPrefs.SetInt("FirstTime", 0);
        }
    }
}