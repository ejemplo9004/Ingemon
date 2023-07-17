using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    #region Singleton
    public static GameController gameController;

    private void Awake() {
        if(gameController != null && gameController != this){
            DestroyImmediate(this.gameObject);
            return;
        }
        gameController = this;
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion
    public dbUsuario usuarioActual;
    [SerializeField] private PlayerEconomy playerEconomy;
    [SerializeField] private Run currentRun;

    [SerializeField] private bool lastRunPassed;
    [SerializeField] private Inventory inventory;
    [SerializeField] private CardInventory cardInventory;
    [SerializeField] private List<CardSet> cardSet1;
    [SerializeField] private List<CardSet> cardSet2;
    [SerializeField] private List<CardSet> cardSet3;
    [SerializeField] private List<CardSet> cardSet4;
    
    public bool firstTime = false;
    private UnityEvent onWin = new UnityEvent();
    private UnityEvent onFail = new UnityEvent();

    private void Start()
    {
        inventory.Ingemones.Clear();
    }

    public void SetRun(Run run){
        currentRun = run;
    }
    public void AsignarJugador(dbUsuario usuario)
    {
        usuarioActual = usuario;
        playerEconomy.SetPlayerMoney(usuarioActual.gold);
    }

    public void AsignarIngemones(List<string> ingemones)
    {

        List<Ingemonster> ingemonsters = new List<Ingemonster>();
        for (int i = 0; i < ingemones.Count; i++)
        {
            Ingemonster inge = JsonUtility.FromJson<Ingemonster>(ingemones[i]);
            // if (inge.id_card1 != null) return;
            // string[] feat = inge.phenotype.Split("-");
            // int race = Int32.Parse(feat[6]);
            // if (feat.Length < 8)
            // {
            //     int index = 0;
            //     switch (race)
            //     {
            //         case 0:
            //             inge.deck = CardSet1[0].Get();
            //             break;
            //         case 1:
            //             inge.deck = CardSet2[0].Get();
            //             break;
            //         case 2:
            //             inge.deck = CardSet3[0].Get();
            //             break;
            //         case 3:
            //             inge.deck = CardSet4[0].Get();
            //             break;
            //         default:
            //             break;
            //     }
            //
            //     string newPhenotype = inge.phenotype + "-" + index;
            //     
            // }
            // else
            // {
            //     int deck = Int32.Parse(feat[7]);
            //     switch (race)
            //     {
            //         case 0:
            //             inge.deck = CardSet1[0].Get();
            //             break;
            //         case 1:
            //             inge.deck = CardSet2[0].Get();
            //             break;
            //         case 2:
            //             inge.deck = CardSet3[0].Get();
            //             break;
            //         case 3:
            //             inge.deck = CardSet4[0].Get();
            //             break;
            //         default:
            //             break;
            //     }
            // }
            ingemonsters.Add(inge);
            Debug.Log(ingemones[i]);
        }

        inventory.Ingemones = ingemonsters;
    }

    public void WinBattle()
    {
        currentRun.lastFightPassed = true;
        onWin.Invoke();
    }

    public void FailBattle()
    {
        onFail.Invoke();
    }
    public Run CurrentRun { get => currentRun; }
    public bool LastRunPassed { get => lastRunPassed; set => lastRunPassed = value; }
    public CardInventory CardInventory { get => cardInventory; }
    public Inventory Inventory => inventory;
    public UnityEvent OnWin => onWin;
    public UnityEvent OnFail => onFail;

    public List<CardSet> CardSet1 => cardSet1;

    public List<CardSet> CardSet2 => cardSet2;

    public List<CardSet> CardSet3 => cardSet3;

    public List<CardSet> CardSet4 => cardSet4;

}
