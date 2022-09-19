using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private GameObject runsPanel;
    [SerializeField] private GameObject rewardPanel;

    public void ToggleRunsPanel(){
        if(runsPanel.activeInHierarchy){
            runsPanel.SetActive(false);
        }
        else{
            runsPanel.SetActive(true);
        }
    }

    public void ShowRewardPanel(){
        rewardPanel.SetActive(true);
    }
}
