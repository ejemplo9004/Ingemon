using System.Collections.Generic;
using UnityEngine;
using Cards;
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

    private void Start() {
        if(Persistence.persistence != null){
            inventory.Ingemones = Persistence.persistence.LoadIngemon();
        }
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
            ingemonsters.Add(JsonUtility.FromJson<Ingemonster>(ingemones[i]));
        }

        inventory.Ingemones = ingemonsters;
    }
    public Run CurrentRun { get => currentRun; }
    public bool LastRunPassed { get => lastRunPassed; set => lastRunPassed = value; }
    public CardInventory CardInventory { get => cardInventory; }
}
