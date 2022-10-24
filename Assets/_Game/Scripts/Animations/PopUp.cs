using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    public float time = .13f;
    void OnEnable()
    {
        LeanTween.scale(gameObject, Vector3.zero, time).setEasePunch(); 
    }

}
