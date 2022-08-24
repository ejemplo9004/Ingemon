using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngemonSelector : MonoBehaviour
{
    [SerializeField] private Ingemonster ingemon;

    public Ingemonster Ingemon { get => ingemon; set => ingemon = value; }
}
