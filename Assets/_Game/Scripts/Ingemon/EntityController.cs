using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public class EntityController : MonoBehaviour
{
    [SerializeField] private List<GameObject> ingemonPrefabs;
    public Ingemonster ingemonInfo { get; set; }
    [SerializeField] private GameObject ingemonMesh;
    public int currentHealth { get; private set; }
    public int protection { get; set; }
    private BuffUIController protectIcon;
    private List<Poison> poisons = new();
    private List<Bleed> bleeds = new();
    private List<IngemonState> otherStates = new();
    private GameObject buffUI;
    public CombatIngemonPosition position;
    public float entityPosOffset = 9.2f;

    private Animator animator;

    [Header("Acciones")]
    public UnityEvent eCurado;
    public UnityEvent eSpawn;
    public UnityEvent eDañado;
    public UnityEvent eEnvenenado;
    public UnityEvent eEscudo;

    public void SetUI(CombatIngemonPosition position)
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
        eSpawn.Invoke();
    }

    public void DestroyIngemon()
    {
        ingemonMesh.SetActive(false);
    }

    //Aqui usariamos el fenotipo para generar el ingemon.
    public void Generate(string phenotype)
    {
        if (phenotype != null)
        {
            string[] feat = phenotype.Split("-");
            ingemonMesh = feat.Length == 7 ? ingemonPrefabs[Int32.Parse(feat[6])] : ingemonPrefabs[0];
            ingemonMesh.SetActive(true);
            ingemonMesh.GetComponent<MorionCambioPartes>()
                .TransformarIngemon(Int32.Parse(feat[0]), Int32.Parse(feat[1]), Int32.Parse(feat[2]));
            ingemonMesh.GetComponent<MorionCambioMascaras>()
                .CambiarTexturas(Int32.Parse(feat[3]), Int32.Parse(feat[4]));
            ingemonMesh.GetComponent<MorionCambioColores>().EstablecerColores(feat[5]);
        }
    }

    public void GetDamaged(int health)
    {
        health *= CombatSingletonManager.Instance.damageMultiplier;
        if (health > protection)
        {
            health -= protection;
            protection = 0;
            UpdateProtection();
            eDañado.Invoke();
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
        else
        {
            eDañado.Invoke();
        }
    }

    public void GetHealed(int health)
    {
        currentHealth = Mathf.Clamp(currentHealth + health, 0, ingemonInfo.maxHealth);
        eCurado.Invoke();
    }

    public void SetState(IngemonState state)
    {
        switch (state.buffType)
        {
            case BuffsEnum.Weak:
                break;
            case BuffsEnum.Buffed:
                break;
            case BuffsEnum.Poison:
                poisons.Add((Poison)state);
                eEnvenenado.Invoke();
                break;
            case BuffsEnum.Bleed:
                bleeds.Add((Bleed)state);
                break;
            case BuffsEnum.Protect:
                break;
            case BuffsEnum.PermanentProtection:
                otherStates.Add((PermanentProtection) state);
                eEscudo.Invoke();
                break;
            case BuffsEnum.StartProtection:
                otherStates.Add((StartProtection) state);
                eEscudo.Invoke();
                break;
            case BuffsEnum.PartnerProtection:
                otherStates.Add((PartnerProtection) state);
                eEscudo.Invoke();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        state.buffIcon = CombatSingletonManager.Instance.uiManager.ShowBuff(position, state.buffType);
        state.SetBuffIcon();
    }

    public bool IsPoisoned()
    {
        return poisons.Count > 0;
    }

    public bool IsBleeding()
    {
        return bleeds.Count > 0;
    }

    public void TickPoison()
    {
        for (int i = poisons.Count - 1; i >= 0; i--)
        {
            if(currentHealth <= 0 ) return;
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

    public void HealBleedTick()
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

    public void CleanPoison()
    {
        poisons.Clear();
        CombatSingletonManager.Instance.uiManager.CleanBuffsOfType(position, BuffsEnum.Poison);
    }

    public void CleanBleed()
    {
        bleeds.Clear();
        CombatSingletonManager.Instance.uiManager.CleanBuffsOfType(position, BuffsEnum.Bleed);
    }

    public void CleanBuffs()
    {
        poisons.ForEach(p => p.Clear());
        bleeds.ForEach(b => b.Clear());
        otherStates.ForEach(s => s.Clear());
        poisons.Clear();
        bleeds.Clear();
        otherStates.Clear();
        CombatSingletonManager.Instance.uiManager.CleanBuffs(position);
    }


    public void GetProtection(int protection)
    {
        this.protection += protection;
        UpdateProtection();

    }

    public void EndTurnClearProtection()
    {
        for (int i = 0; i < otherStates.Count; i++)
        {
            if (otherStates[i].GetType() == typeof(PermanentProtection))
            {
                if (otherStates[i].Tick(this) == 0)
                {
                    if (otherStates.Count > 0)
                    {
                        otherStates.RemoveAt(i);
                    }
                }
                return;
            }
        }
        ClearProtection();
    }

    public void ClearProtection()
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
                    .ShowBuff(position, BuffsEnum.Protect);
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

    public void TickStates(BuffTimings timings)
    {
        for (int i = 0; i < otherStates.Count; i++)
        {
            if (!otherStates[i].timings.Contains(timings)) continue;
            if (otherStates[i].Tick(this) != 0) continue;
            if (otherStates.Count > 0)
            {
                otherStates.RemoveAt(i);
            }
        }
    }

    public bool IsBuffedWith(BuffsEnum buff)
    {
        foreach (IngemonState state in otherStates)
        {
            if (state.buffType == buff)
            {
                return true;
            }
        }

        return false;
    }

    public void UpdateState(BuffsEnum state, int value)
    {
        IngemonState s = otherStates.Find(n => n.buffType == state);
        if (s is IUpdatableState)
        {
            IUpdatableState u = (IUpdatableState)s;
            u.UpdateState(value);
        }
    }
}