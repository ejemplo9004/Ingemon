using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EntityController : MonoBehaviour
{
    public Ingemonster ingemonInfo { get; set; }
    [SerializeField] private GameObject ingemonMesh;
    public int currentHealth { get; private set;  }
    private List<Modifiers> status;

    public void SetHealth()
    {
        currentHealth = ingemonInfo.maxHealth;
    }

    public void Spawn(Vector3 pos, string phenotype)
    {
        transform.position = pos;
        Generate(phenotype);
    }

    //Aqui usariamos el fenotipo para generar el ingemon.
    public void Generate(string phenotype)
    {
        ingemonMesh.SetActive(true);
        if(phenotype != ""){
            string[] feat = phenotype.Split("-");
            ingemonMesh.GetComponent<MorionCambioPartes>().TransformarIngemon(Int32.Parse(feat[0]), Int32.Parse(feat[1]), Int32.Parse(feat[2]));
            ingemonMesh.GetComponent<MorionCambioMascaras>().CambiarTexturas(Int32.Parse(feat[3]), Int32.Parse(feat[4]));
            ingemonMesh.GetComponent<MorionCambioColores>().EstablecerColores(feat[5]);
        }
    }

    public void GetDamaged(int health)
    {
        currentHealth = Mathf.Clamp(currentHealth - health, 0, currentHealth);
        Debug.Log($"{ingemonInfo.name} has lost {health} health, now he has {currentHealth} health points");
    }

    public void GetHealed(int health)
    {
        currentHealth = Mathf.Clamp(currentHealth + health, 0, currentHealth);
    }

    public void GetStatus(Modifiers mod)
    {
        status.Add(mod);
    }
}
