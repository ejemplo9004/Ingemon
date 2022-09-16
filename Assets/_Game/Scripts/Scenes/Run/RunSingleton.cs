using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cards;

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
    [SerializeField] private List<ScriptableCard> rewardCardPool;

    private void Start() {
        runDeck = new RunDeck(GameController.gameController.CardInventory.BaseCollection);
    }

    public ScriptableCard AddCardReward(){
        int index = Random.Range(0, rewardCardPool.Count - 1);
        runDeck.AddCard(rewardCardPool[index]);
        return rewardCardPool[index];
    }
}
