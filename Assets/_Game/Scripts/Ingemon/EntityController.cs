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
    private BuffUIController protectIcon;
    private List<Poison> poisons = new();
    private List<Bleed> bleeds = new();
    private GameObject buffUI;
    public CombatIngemonEnum position;
    public float entityPosOffset = 9.2f;

    private Animator animator;

    public void SetUI(CombatIngemonEnum position)
    {
        currentHealth = ingemonInfo.maxHealth;
        this.position = position;
    }

    public void Spawn(Vector3 pos, Ingemonster ingemon, int room)
    {
        transform.position = pos + room * new Vector3(0, entityPosOffset, 0);
        ingemonInfo = ingemon;
        currentHealth = ingemon.maxHealth;
        Generate(ingemon.phenotype);
        animator = gameObject.GetComponentInChildren<Animator>();
        animator?.SetBool(Parameters.COMBATE, true);
    }

    public void DestroyIngemon()
    {
        ingemonMesh.SetActive(false);
    }

    //Aqui usariamos el fenotipo para generar el ingemon.
    public void Generate(string phenotype)
    {
        ingemonMesh.SetActive(true);
        if (phenotype != null)
        {
            string[] feat = phenotype.Split("-");
            ingemonMesh.GetComponent<MorionCambioPartes>()
                .TransformarIngemon(Int32.Parse(feat[0]), Int32.Parse(feat[1]), Int32.Parse(feat[2]));
            ingemonMesh.GetComponent<MorionCambioMascaras>()
                .CambiarTexturas(Int32.Parse(feat[3]), Int32.Parse(feat[4]));
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
            UpdateProtection();
        }
        else
        {
            protection -= health;
            UpdateProtection();
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
            CleanBuffs();
        }
    }

    public void GetHealed(int health)
    {
        currentHealth = Mathf.Clamp(currentHealth + health, 0, currentHealth);
    }

    public void SetState(IngemonState state)
    {
        switch (state.buffType)
        {
            case BuffsEnum.WEAK:
                break;
            case BuffsEnum.BUFFED:
                break;
            case BuffsEnum.POISON:
                poisons.Add((Poison)state);
                break;
            case BuffsEnum.BLEED:
                bleeds.Add((Bleed)state);
                break;
            case BuffsEnum.PROTECT:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        state.buffIcon = CombatSingletonManager.Instance.uiManager.ShowBuff(position, state.buffType);
        state.SetBuffIcon();
    }

    public void TickPoison()
    {
        for (int i = poisons.Count - 1; i >= 0; i--)
        {
            if (poisons[i].Tick(this) == 0)
            {
                if (poisons.Count <= 0)
                    return;
                poisons.RemoveAt(i);
            }
        }
    }

    public void TickBleed()
    {
        for (int i = bleeds.Count - 1; i >= 0; i--)
        {
            if (bleeds[i].Tick(this) == 0)
            {
                if (bleeds.Count <= 0)
                    return;
                bleeds.RemoveAt(i);
            }
        }
    }

    public void HealBleed()
    {
        for (int i = bleeds.Count - 1; i >= 0; i--)
        {
            if (bleeds[i].DeniedTick() == 0)
            {
                if (bleeds.Count <= 0)
                    return;
                bleeds.RemoveAt(i);
            }
        }
    }

    private void CleanBuffs()
    {
        poisons = new List<Poison>();
        bleeds = new List<Bleed>();
        CombatSingletonManager.Instance.uiManager.CleanBuffs(position);
    }


    public void GetProtection(int protection)
    {
        this.protection += protection;
        UpdateProtection();
    }

    public void LoseProtection()
    {
        protection = 0;
        UpdateProtection();
    }

    private void UpdateProtection()
    {
        if (protectIcon == null)
        {
            if (protection > 0)
            {
                protectIcon = CombatSingletonManager.Instance.uiManager
                    .ShowBuff(position, BuffsEnum.PROTECT);
            }
            else
            {
                return;
            }
        }
        protectIcon.UpdateValue(protection);
    }

    public bool CheckDead() => currentHealth <= 0;

    public void DeadAnimation()
    {
        animator?.SetBool(Parameters.VIVO, false);
    }

    public void BattlePosition(bool state)
    {
        animator?.SetBool(Parameters.COMBATE, true);
    }

    public void AttackAnimation()
    {
        animator?.SetTrigger(Parameters.ATACANDO);
    }

    public void DamageAnimation()
    {
        animator?.SetTrigger(Parameters.DANO);
    }
}