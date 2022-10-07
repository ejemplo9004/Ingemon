using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private GameObject runsPanel;
    [SerializeField] private GameObject rewardPanel;
    [SerializeField] private Text rewardText;
    [SerializeField] private float fadeRewardTime;

    public void ToggleRunsPanel(){
        if(runsPanel.activeInHierarchy){
            runsPanel.SetActive(false);
        }
        else{
            runsPanel.SetActive(true);
        }
    }

    public void ShowRewardPanel(string text){
        rewardPanel.SetActive(true);
        rewardText.text = text;
        StartCoroutine(FadeRewardPanel());
    }

    private IEnumerator FadeRewardPanel()
    {
        Image panel = rewardPanel.GetComponent<Image>();
        Color panelStartColor = panel.color;
        Color panelEndColor = new Color(panelStartColor.r, panelStartColor.g, panelStartColor.b, 0f);
        Color textStartColor = rewardText.color;
        Color textEndColor = new Color(textStartColor.r, textStartColor.g, textStartColor.b, 0f);
        for (float i = 0f; i < fadeRewardTime; i+= Time.deltaTime)
        {
            float normalizedTime = i / fadeRewardTime;
            panel.color = Color.Lerp(panelStartColor, panelEndColor, normalizedTime);
            rewardText.color = Color.Lerp(textStartColor, textEndColor, normalizedTime);
            yield return null;
        }
        rewardPanel.SetActive(false);
        panel.color = panelStartColor;
        rewardText.color = textStartColor;
    }
}
