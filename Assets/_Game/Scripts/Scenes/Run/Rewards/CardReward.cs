using System.Collections;
using UnityEngine;
using Cards;

public class CardReward : MonoBehaviour, IReward
{
    [SerializeField] private GameObject cardSprite;
    [SerializeField] private GameObject parent;
    public void AddReward()
    {
        ScriptableCard card = RunSingleton.Instance.AddCardReward();
        ShowCardEarned(card);
    }

    public void ShowCardEarned(ScriptableCard cardInfo){
        Card card = new Card(cardInfo);
        GameObject cardCopy = Instantiate(cardSprite, parent.transform);
        cardCopy.GetComponent<CardSpriteController>().InitCardSprite(card);
        StartCoroutine(HideCard(cardCopy));
    }

    private IEnumerator HideCard(GameObject card){
        yield return new WaitForSeconds(2f);
        Destroy(card);
    }

}
