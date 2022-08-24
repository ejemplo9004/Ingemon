using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Controller", menuName = "Ingemon/Game Controller")]
public class GameController : MonoBehaviour
{
    #region Singleton
    public static GameController gameController;

    private void Awake() {
        if(gameController != null){
            DestroyImmediate(this.gameObject);
            return;
        }
        gameController = this;
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion
    [SerializeField] private Run currentRun;

    [SerializeField] private bool lastRunPassed;
    [SerializeField] private Inventory inventory;

    private void Start() {
        if(Persistence.persistence != null){
            inventory.Ingemones = Persistence.persistence.LoadIngemon();
        }
    }

    public void SetRun(Run run){
        currentRun = run;
    }

    public Run CurrentRun { get => currentRun; }
    public bool LastRunPassed { get => lastRunPassed; set => lastRunPassed = value; }
}
