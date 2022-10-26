using System;
using UnityEngine;

public class TutorialActivator: MonoBehaviour
{
    [SerializeField] private GameObject tutorialObject;
    private void Start()
    {
        if (GameController.gameController.firstTime)
        {
            tutorialObject.SetActive(true);
        }

        GameController.gameController.firstTime = false;
    }
}