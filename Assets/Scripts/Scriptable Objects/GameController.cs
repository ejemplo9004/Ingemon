using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Controller", menuName = "Ingemon/Game Controller")]
public class GameController : ScriptableObject
{
    [SerializeField] private Run currentRun;

    [SerializeField] private bool lastRunPassed;

    public void SetRun(Run run){
        currentRun = run;
    }

    public Run CurrentRun { get => currentRun; }
    public bool LastRunPassed { get => lastRunPassed; set => lastRunPassed = value; }
}
