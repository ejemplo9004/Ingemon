using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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
    public dbJugador jugadorActual;
    [SerializeField] private PlayerEconomy playerEconomy;
    [SerializeField] private Run currentRun;

    [SerializeField] private bool lastRunPassed;
    [SerializeField] private Inventory inventory;
    [SerializeField] private CardInventory cardInventory;
    private UnityEvent onWin = new UnityEvent();
    private UnityEvent onFail = new UnityEvent();

    private void Start()
    {
        inventory.Ingemones.Clear();
    }

    public void SetRun(Run run){
        currentRun = run;
    }
    public void AsignarJugador(dbJugador jugador)
    {
        jugadorActual = jugador;
        playerEconomy.SetPlayerMoney(jugadorActual.oro);
    }
    public void AsignarIngemones(List<string> ingemones)
    {
        List<Ingemonster> ingemonsters = new List<Ingemonster>();
        for (int i = 0; i < ingemones.Count; i++)
        {
            ingemonsters.Add(JsonUtility.FromJson<Ingemonster>(ingemones[i]));
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
}
