using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngemonSelector : MonoBehaviour
{
    [SerializeField] private GameObject ingemon;

    public GameObject Ingemon { get => ingemon; set => ingemon = value; }
}
