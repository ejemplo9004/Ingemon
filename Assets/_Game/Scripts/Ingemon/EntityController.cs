using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EntityController : MonoBehaviour
{
    public Ingemonster ingemonInfo { get; set; }
    [SerializeField] private GameObject ingemonMesh;
    public int currentHealth { get; private set; }
    public int protection { get; set; }
    private List<Poison> poisons = new();
    private List<Bleed> bleeds = new();

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
        health *= 2;
        if (health > protection)
        {
            health -= protection;
            protection = 0;
        }
        else
        {
            protection -= health;
            health = 0;
        }
        
        GetDamageNoProtection(health);
    }

    public void GetDamageNoProtection(int health)
    {
        currentHealth = Mathf.Clamp(currentHealth - health, 0, currentHealth);
        if (CheckDead())
        {
            CombatSingletonManager.Instance.eventManager.DeadIngemon(this);
            CombatSingletonManager.Instance.turnManager.info.PurgeCardsFromDeckAfterAnIngemonDie(this);
            DeadAnimation();
        }
    }

    public void GetHealed(int health)
    {
        currentHealth = Mathf.Clamp(currentHealth + health, 0, currentHealth);
    }

    public void SetState(IngemonState state)
    {
        if(state.GetType() == typeof(Poison))
            poisons.Add((Poison)state);
        if(state.GetType() == typeof(Bleed))
            bleeds.Add((Bleed)state);
    }

    public void TickPoison()
    {
        for (int i = poisons.Count -1; i >= 0; i--)
        {
            if (poisons[i].Tick(this) == 0)
            {
                poisons.RemoveAt(i);
            }
        }
    }
    
    public void TickBleed()
    {
        for (int i = bleeds.Count -1; i >= 0; i--)
        {
            if (bleeds[i].Tick(this) == 0)
            {
                bleeds.RemoveAt(i);
            }
        }
    }
    public void HealBleed()
    {
        for (int i = bleeds.Count -1; i >= 0; i--)
        {
            if (bleeds[i].DeniedTick() == 0)
            {
                bleeds.RemoveAt(i);
            }
        }
    }

    public void GetProtection(int protection) => this.protection += protection;
    public void LoseProtection() => protection = 0;

    public bool CheckDead() => currentHealth <= 0;

    public void DeadAnimation()
    {
        transform.Rotate(new Vector3(90,0,0), Space.Self);
    }
    
}
