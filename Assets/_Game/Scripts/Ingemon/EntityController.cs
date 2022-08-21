using System.Collections;
using System.Collections.Generic;
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

    public void Spawn(Vector3 pos)
    {
        transform.position = pos;
        Generate();
    }

    //Aqui usariamos el fenotipo para generar el ingemon.
    public void Generate()
    {
        ingemonMesh.SetActive(true);
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
