using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomButtonsController : MonoBehaviour
{
    [SerializeField] private List<Button> roomButtons;

    public void ToggleButton(int button, bool state){
        if(button >= roomButtons.Count){
            return;
        }
        roomButtons[button].interactable = state;
    }
}
