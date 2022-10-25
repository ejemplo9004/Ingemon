using System;
using System.Collections.Generic;
using UnityEngine;

public class IngemonSpawner : MonoBehaviour
{
    [SerializeField] private Inventory ingemonInventory;
    [SerializeField] private List<GameObject> ingemonPrefabs;
    [SerializeField] private RenderCreator renderCreator;
    public Vector3 startPosition;
    public float positionOffset;
    private float lastPosition;
    public bool inRun;

    private void Start()
    {
        lastPosition = startPosition.x;
        Spawn();
    }

    private void Spawn()
    {
        List<Camera> cameras = new List<Camera>();
        for (int i = 0; i < ingemonInventory.Ingemones.Count; i++)
        {
            Vector3 pos = new Vector3(lastPosition + positionOffset, startPosition.y, startPosition.z);
            Ingemonster ingemon = ingemonInventory.Ingemones[i];
            int race = Int32.Parse(ingemon.phenotype.Split('-')[6]);
            GameObject copy = Instantiate(ingemonPrefabs[race], pos, Quaternion.identity);
            cameras.Add(copy.GetComponentInChildren<Camera>());
            string[] feat = ingemon.phenotype.Split('-');
            Generate(copy, feat);
            lastPosition += positionOffset;
        }
        renderCreator.Cameras = cameras;
        if (inRun)
        {
            renderCreator.gameObject.SetActive(true);
        }
    }

    private void Generate(GameObject copy, string[] feat)
    {
        copy.GetComponent<MorionCambioPartes>().TransformarIngemon(Int32.Parse(feat[0]), Int32.Parse(feat[1]), Int32.Parse(feat[2]));
        copy.GetComponent<MorionCambioMascaras>().CambiarTexturas(Int32.Parse(feat[3]), Int32.Parse(feat[4]));
        copy.GetComponent<MorionCambioColores>().EstablecerColores(feat[5]);
    }
}
