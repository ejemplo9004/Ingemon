using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Room", menuName = "Ingemon/RoomSO")]
public class Room : ScriptableObject
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private Sprite background;
    

    /// <summary>
    /// Metodo para obtener un enemigo del pool de enemigos
    /// </summary>

    public void GetRoomEnemy(){
        
    }

    public Sprite Background { get => background; set => background = value; }
}
