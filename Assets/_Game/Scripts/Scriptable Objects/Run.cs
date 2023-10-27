using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Run", menuName = "Ingemon/RunSO")]
public class Run : ScriptableObject
{
    [SerializeField] private List<Room> rooms;
    public Room currentRoom;
    public bool lastFightPassed;
    public bool runCompleted;
    public int currentRoomNumber;
    public EnemiesInventory enemiesInventory;
    [SerializeField] private int reward;
    [SerializeField] private GameObject boss;
    [SerializeField] private Sprite background;
    [SerializeField] private bool ingemonsWereSelected;

    public Sprite Background { get => background; }
    public bool IngemonsWereSelected { get => ingemonsWereSelected; set => ingemonsWereSelected = value; }

    public void SetRoom(Room room){
        currentRoom = room;
    }

    public void UnlockNextRoom(){
        if(currentRoomNumber >= rooms.Count - 1){
            runCompleted = true;
            return;
        }
        currentRoomNumber++;
    }

    public void SetLastFightStatus(bool status){
        lastFightPassed = status;
    }

    public void RestartValues(){
        ingemonsWereSelected = false;
        currentRoomNumber = 0;
        //currentRoom = null;
    }

    public int Reward => reward;
}
