using System;
using System.Collections.Generic;
using UnityEngine;
using Cards;
using Random = UnityEngine.Random;

public class RunSingleton : MonoBehaviour
{
    #region Singleton
    public static RunSingleton Instance;

    private void Awake() {
        if(Instance != null && Instance != this){
            DestroyImmediate(this.gameObject);
        }
        Instance = this;

        DontDestroyOnLoad(this.gameObject);
    }
    #endregion

    public RunDeck runDeck;
    public Run run;
    public bool enoughIngemones;
    [SerializeField] private Inventory runInventory;
    [SerializeField] private List<ScriptableCard> rewardCardPool;

    private void OnEnable()
    {
        run = GameController.gameController.CurrentRun;
        enoughIngemones = true;
    }
    private void Start() {
        runDeck = new RunDeck(GameController.gameController.CardInventory.BaseCollection);
    }
    public ScriptableCard AddCardReward(){
        int index = Random.Range(0, rewardCardPool.Count - 1);
        runDeck.AddCard(rewardCardPool[index]);
        return rewardCardPool[index];
    }

    public void DeleteIngemonFromRun(string ingemonId)
    {
        runInventory.DeleteIngemon(ingemonId);
        if (runInventory.Ingemones.Count < 2)
        {
            enoughIngemones = false;
        }
    }

    public Inventory RunInventory => runInventory;
}
